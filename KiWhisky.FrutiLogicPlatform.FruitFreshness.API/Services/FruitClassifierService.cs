using Microsoft.ML;
using Microsoft.ML.Data;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;

namespace FruitFreshness.Services
{
    [SupportedOSPlatform("windows")]
    public class FruitClassifierService : IDisposable
    {
        private readonly MLContext _ml;
        private ITransformer? _model;
        private readonly string[] _labels;
        private readonly string _modelPath;
        private readonly string _modelVersion;
        private readonly object _loadLock = new();
        private string? _loadError;
        private readonly List<ReferenceProfile> _referenceProfiles = new();

        public FruitClassifierService(string onnxModelPath, string[] labels, string modelVersion)
        {
            _ml = new MLContext();
            _labels = labels;
            _modelPath = onnxModelPath;
            _modelVersion = modelVersion;

            // Intentar cargar el modelo en el arranque, pero sin lanzar excepción.
            TryLoadModel();
            TryLoadReferenceProfiles();
        }

        public (string Label, float Confidence, (string label, float score)[] Scores, string ModelVersion) PredictFromFile(string imagePath)
        {
            EnsureModelLoaded();

            if (TryPredictFromReferenceProfiles(imagePath, out var referenceBad, out var referenceGood))
                return BuildResult(referenceBad, referenceGood);

            var heuristic = AnalyzeFreshnessHeuristic(imagePath);

            // Solo forzar heurística cuando el deterioro es realmente alto.
            if (heuristic.BadConfidence >= 0.60f)
            {
                return BuildResult(heuristic.BadConfidence, 1f - heuristic.BadConfidence);
            }

            try
            {
                var engine = _ml.Model.CreatePredictionEngine<ImageData, ModelOutput>(_model!);
                var output = engine.Predict(new ImageData { ImagePath = imagePath });
                var scores = NormalizeScores(output.Output ?? Array.Empty<float>());
                if (scores.Length >= 2)
                {
                    var indexed = scores.Select((s, i) => (label: i < _labels.Length ? _labels[i] : i.ToString(), score: s))
                        .OrderByDescending(x => x.score)
                        .ToArray();
                    var best = indexed.FirstOrDefault();

                    // Solo sobreescribir al modelo si la evidencia visual de deterioro es fuerte.
                    if (heuristic.BadConfidence >= 0.72f || heuristic.BadConfidence > best.score + 0.12f)
                        return BuildResult(heuristic.BadConfidence, 1f - heuristic.BadConfidence);

                    return (best.label, best.score, indexed, _modelVersion);
                }
            }
            catch
            {
                // Si el modelo falla en tiempo de inferencia, usar la heurística visual.
            }

            return BuildResult(heuristic.BadConfidence, 1f - heuristic.BadConfidence);
        }

        public bool ModelLoaded => _model != null;
        public string? ModelLoadError => _loadError;

        public void Dispose()
        {
            // nothing to dispose explicitly
        }

        public string ModelPath => _modelPath;
        public string ModelVersion => _modelVersion;

        private class ImageData
        {
            public string ImagePath { get; set; } = string.Empty;
        }

        private class ModelOutput
        {
            [ColumnName("output")]
            public float[]? Output { get; set; }
        }

        private sealed class ReferenceProfile
        {
            public bool IsBad { get; set; }
            public float[] Features { get; set; } = Array.Empty<float>();
        }

        private void EnsureModelLoaded()
        {
            if (_model != null)
                return;

            lock (_loadLock)
            {
                if (_model == null)
                    TryLoadModel();
            }

            if (_model == null)
                throw new InvalidOperationException($"Model not loaded: {_loadError ?? "unknown error"}");
        }

        private void TryLoadModel()
        {
            try
            {
                if (!File.Exists(_modelPath))
                {
                    _loadError = $"Model file not found: {_modelPath}";
                    _model = null;
                    return;
                }

                var pipeline = _ml.Transforms.LoadImages(outputColumnName: "input", imageFolder: "", inputColumnName: nameof(ImageData.ImagePath))
                    .Append(_ml.Transforms.ResizeImages(outputColumnName: "input", imageWidth: 224, imageHeight: 224, inputColumnName: "input"))
                    .Append(_ml.Transforms.ExtractPixels(outputColumnName: "input", interleavePixelColors: true, scaleImage: 1f / 255f))
                    .Append(_ml.Transforms.ApplyOnnxModel(modelFile: _modelPath, outputColumnNames: new[] { "output" }, inputColumnNames: new[] { "input" }));

                var empty = _ml.Data.LoadFromEnumerable(new ImageData[] { });
                _model = pipeline.Fit(empty);
                _loadError = null;
            }
            catch (Exception ex)
            {
                _model = null;
                _loadError = ex.Message;
            }
        }

        private void TryLoadReferenceProfiles()
        {
            try
            {
                _referenceProfiles.Clear();
                var modelDir = Path.GetDirectoryName(_modelPath);
                if (string.IsNullOrWhiteSpace(modelDir) || !Directory.Exists(modelDir))
                    return;

                var files = Directory.EnumerateFiles(modelDir)
                    .Where(p => p.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                             || p.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
                             || p.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                    .ToArray();

                foreach (var file in files)
                {
                    var name = Path.GetFileNameWithoutExtension(file).ToLowerInvariant();
                    var isClean = name.Contains("_clean");
                    var isDirt = name.Contains("_dirt") || name.Contains("_bad") || name.Contains("_spoiled");
                    if (!isClean && !isDirt)
                        continue;

                    var features = ExtractVisualFeatures(file);
                    if (features.Length == 0)
                        continue;

                    _referenceProfiles.Add(new ReferenceProfile
                    {
                        IsBad = isDirt,
                        Features = features
                    });
                }
            }
            catch
            {
                // Si falla carga de perfiles, se usa el flujo normal (modelo + heurística).
            }
        }

        private bool TryPredictFromReferenceProfiles(string imagePath, out float badScore, out float goodScore)
        {
            badScore = 0f;
            goodScore = 0f;

            if (_referenceProfiles.Count < 2)
                return false;

            var inputFeatures = ExtractVisualFeatures(imagePath);
            if (inputFeatures.Length == 0)
                return false;

            var neighbors = _referenceProfiles
                .Select(p => new
                {
                    p.IsBad,
                    Distance = EuclideanDistance(inputFeatures, p.Features)
                })
                .OrderBy(x => x.Distance)
                .Take(Math.Min(3, _referenceProfiles.Count))
                .ToArray();

            if (neighbors.Length == 0)
                return false;

            float badWeight = 0f;
            float goodWeight = 0f;

            foreach (var n in neighbors)
            {
                var w = 1f / (n.Distance + 1e-6f);
                if (n.IsBad)
                    badWeight += w;
                else
                    goodWeight += w;
            }

            var sum = badWeight + goodWeight;
            if (sum <= 0f)
                return false;

            badScore = badWeight / sum;
            goodScore = goodWeight / sum;
            return true;
        }

        [SupportedOSPlatform("windows")]
        private static float[] ExtractVisualFeatures(string imagePath)
        {
            try
            {
                using var bitmap = new Bitmap(imagePath);
                var width = bitmap.Width;
                var height = bitmap.Height;
                if (width <= 0 || height <= 0)
                    return Array.Empty<float>();

                var step = Math.Max(1, Math.Min(width, height) / 96);
                var samples = 0;

                float sumR = 0f, sumG = 0f, sumB = 0f;
                float sumR2 = 0f, sumG2 = 0f, sumB2 = 0f;
                float sumSat = 0f;
                float dark = 0f, grayish = 0f, yellow = 0f, red = 0f;

                for (var y = 0; y < height; y += step)
                {
                    for (var x = 0; x < width; x += step)
                    {
                        var c = bitmap.GetPixel(x, y);
                        var r = c.R / 255f;
                        var g = c.G / 255f;
                        var b = c.B / 255f;
                        var max = Math.Max(r, Math.Max(g, b));
                        var min = Math.Min(r, Math.Min(g, b));
                        var sat = max <= 0.0001f ? 0f : (max - min) / max;

                        samples++;
                        sumR += r; sumG += g; sumB += b;
                        sumR2 += r * r; sumG2 += g * g; sumB2 += b * b;
                        sumSat += sat;

                        if (max < 0.18f)
                            dark += 1f;
                        if (sat < 0.18f && max < 0.92f)
                            grayish += 1f;
                        if (r >= 0.45f && g >= 0.40f && b <= 0.45f && r > b * 1.15f && g > b * 1.10f)
                            yellow += 1f;
                        if (r > g * 1.10f && r > b * 1.10f)
                            red += 1f;
                    }
                }

                if (samples == 0)
                    return Array.Empty<float>();

                var meanR = sumR / samples;
                var meanG = sumG / samples;
                var meanB = sumB / samples;
                var stdR = MathF.Sqrt(Math.Max(0f, sumR2 / samples - meanR * meanR));
                var stdG = MathF.Sqrt(Math.Max(0f, sumG2 / samples - meanG * meanG));
                var stdB = MathF.Sqrt(Math.Max(0f, sumB2 / samples - meanB * meanB));

                return new[]
                {
                    meanR, meanG, meanB,
                    stdR, stdG, stdB,
                    sumSat / samples,
                    dark / samples,
                    grayish / samples,
                    yellow / samples,
                    red / samples
                };
            }
            catch
            {
                return Array.Empty<float>();
            }
        }

        private static float EuclideanDistance(float[] a, float[] b)
        {
            if (a.Length == 0 || b.Length == 0 || a.Length != b.Length)
                return float.MaxValue;

            var sum = 0f;
            for (var i = 0; i < a.Length; i++)
            {
                var d = a[i] - b[i];
                sum += d * d;
            }

            return MathF.Sqrt(sum);
        }

        [SupportedOSPlatform("windows")]
        private (float BadConfidence, float GoodConfidence) AnalyzeFreshnessHeuristic(string imagePath)
        {
            try
            {
                using var bitmap = new Bitmap(imagePath);
                var width = bitmap.Width;
                var height = bitmap.Height;
                if (width <= 0 || height <= 0)
                    return (0.5f, 0.5f);

                var step = Math.Max(1, Math.Min(width, height) / 120);
                var sampled = 0;
                var moldLike = 0;
                var darkSpots = 0;
                var vividRed = 0;
                var redPixels = 0;
                var yellowPixels = 0;
                var fruitLikePixels = 0;

                for (var y = 0; y < height; y += step)
                {
                    for (var x = 0; x < width; x += step)
                    {
                        var c = bitmap.GetPixel(x, y);
                        var r = c.R / 255f;
                        var g = c.G / 255f;
                        var b = c.B / 255f;
                        var max = Math.Max(r, Math.Max(g, b));
                        var min = Math.Min(r, Math.Min(g, b));
                        var brightness = max;
                        var saturation = max <= 0.0001f ? 0f : (max - min) / max;

                        sampled++;

                        var redDominant = r > g * 1.08f && r > b * 1.08f && brightness > 0.2f;
                        var yellowDominant = r >= 0.45f && g >= 0.40f && b <= 0.45f && r > b * 1.15f && g > b * 1.10f && saturation > 0.20f;

                        // Evita contar como deterioro el fondo blanco y zonas casi blancas.
                        var fruitLike = saturation > 0.08f || brightness < 0.96f;
                        if (fruitLike)
                            fruitLikePixels++;

                        // Moho/gris solo en zona de fruta y excluyendo amarillos sanos.
                        var grayish = fruitLike && !yellowDominant && saturation < 0.30f && brightness >= 0.14f && brightness <= 0.90f;
                        var dark = fruitLike && !yellowDominant && brightness < 0.18f && saturation < 0.50f;

                        if (grayish)
                            moldLike++;
                        if (dark)
                            darkSpots++;
                        if (redDominant)
                            redPixels++;
                        if (redDominant && saturation > 0.45f)
                            vividRed++;
                        if (yellowDominant)
                            yellowPixels++;
                    }
                }

                if (sampled == 0)
                    return (0.5f, 0.5f);

                var denom = Math.Max(1, fruitLikePixels);
                var moldRatio = (float)moldLike / denom;
                var darkRatio = (float)darkSpots / denom;
                var redRatio = (float)redPixels / sampled;
                var vividRedRatio = (float)vividRed / sampled;
                var yellowRatio = (float)yellowPixels / sampled;

                // Regla fuerte: cualquier señal visible de moho gris/blanco o zonas oscuras amplias
                // debe empujar la salida hacia "por echarse a perder".
                var moldSignal = moldRatio * 7.0f;
                var darkSignal = darkRatio * 2.0f;
                var freshnessSignal = (redRatio * 0.25f) + (vividRedRatio * 0.15f);
                var yellowFreshSignal = yellowRatio * 1.6f;

                var badScore = moldSignal + darkSignal - freshnessSignal - yellowFreshSignal;

                // Evidencia fuerte de moho/sombra profunda.
                if (moldRatio >= 0.012f || darkRatio >= 0.08f)
                    badScore = Math.Max(badScore, 0.82f);

                // Compensación fuerte para plátano amarillo sano.
                if (yellowRatio >= 0.15f && moldRatio < 0.012f && darkRatio < 0.14f)
                    badScore = Math.Min(badScore, 0.32f);
                if (yellowRatio >= 0.25f && moldRatio < 0.02f)
                    badScore = Math.Min(badScore, 0.22f);

                badScore = Math.Clamp(badScore, 0.05f, 0.99f);
                var goodScore = 1f - badScore;
                return (badScore, goodScore);
            }
            catch
            {
                return (0.5f, 0.5f);
            }
        }

        private (string Label, float Confidence, (string label, float score)[] Scores, string ModelVersion) BuildResult(float badConfidence, float goodConfidence)
        {
            var badLabel = _labels.Length > 0 ? _labels[0] : "Fruta por echarse a perder";
            var goodLabel = _labels.Length > 1 ? _labels[1] : "Fruta en buen estado";
            var scores = new[]
            {
                (label: badLabel, score: badConfidence),
                (label: goodLabel, score: goodConfidence)
            }.OrderByDescending(x => x.score).ToArray();
            var best = scores.First();
            return (best.label, best.score, scores, _modelVersion);
        }

        private static float[] NormalizeScores(float[] rawScores)
        {
            if (rawScores.Length == 0)
                return rawScores;

            // Si ya parecen probabilidades [0..1] y suman ~1, no aplicar softmax otra vez.
            var sum = rawScores.Sum();
            var alreadyProbabilities = rawScores.All(s => s >= 0f && s <= 1f) && Math.Abs(sum - 1f) < 0.01f;
            if (alreadyProbabilities)
                return rawScores;

            var max = rawScores.Max();
            var exps = rawScores.Select(s => MathF.Exp(s - max)).ToArray();
            var expSum = exps.Sum();
            if (expSum <= 0f)
                return rawScores;

            for (var i = 0; i < exps.Length; i++)
                exps[i] = exps[i] / expSum;

            return exps;
        }
    }
}
