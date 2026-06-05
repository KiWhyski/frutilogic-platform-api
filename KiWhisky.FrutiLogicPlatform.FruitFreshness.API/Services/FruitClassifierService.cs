using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;
using System.Linq;

namespace FruitFreshness.Services
{
    public class FruitClassifierService : IDisposable
    {
        private readonly MLContext _ml;
        private ITransformer? _model;
        private readonly string[] _labels;
        private readonly string _modelPath;
        private readonly string _modelVersion;
        private readonly object _loadLock = new();
        private string? _loadError;

        public FruitClassifierService(string onnxModelPath, string[] labels, string modelVersion)
        {
            _ml = new MLContext();
            _labels = labels;
            _modelPath = onnxModelPath;
            _modelVersion = modelVersion;

            // Intentar cargar el modelo en el arranque, pero sin lanzar excepción.
            TryLoadModel();
        }

        public (string Label, float Confidence, (string label, float score)[] Scores, string ModelVersion) PredictFromFile(string imagePath)
        {
            EnsureModelLoaded();
            var engine = _ml.Model.CreatePredictionEngine<ImageData, ModelOutput>(_model!);
            var output = engine.Predict(new ImageData { ImagePath = imagePath });
            var scores = output.Output ?? Array.Empty<float>();
            var indexed = scores.Select((s, i) => (label: i < _labels.Length ? _labels[i] : i.ToString(), score: s))
                .OrderByDescending(x => x.score)
                .ToArray();
            var best = indexed.FirstOrDefault();
            return (best.label, best.score, indexed, _modelVersion);
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
    }
}
