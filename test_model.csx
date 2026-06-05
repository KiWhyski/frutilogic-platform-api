using Microsoft.ML;
using System;
using System.IO;

var ml = new MLContext();
var modelPath = @"C:\Users\PC\RiderProjects\frutilogic-platform-api\KiWhisky.FrutiLogicPlatform.FruitFreshness.API\models\fruit_model.onnx";

Console.WriteLine($"Model path: {modelPath}");
Console.WriteLine($"File exists: {File.Exists(modelPath)}");

if (File.Exists(modelPath))
{
    try
    {
        var pipeline = ml.Transforms.LoadImages(outputColumnName: "input", imageFolder: "", inputColumnName: "ImagePath")
            .Append(ml.Transforms.ResizeImages(outputColumnName: "input", imageWidth: 224, imageHeight: 224, inputColumnName: "input"))
            .Append(ml.Transforms.ExtractPixels(outputColumnName: "input", interleavePixelColors: true, scaleImage: 1f / 255f))
            .Append(ml.Transforms.ApplyOnnxModel(modelFile: modelPath, outputColumnNames: new[] { "output" }, inputColumnNames: new[] { "input" }));

        Console.WriteLine("✓ Pipeline created successfully");

        // Try to fit
        var empty = ml.Data.LoadFromEnumerable(new[] { new { ImagePath = "" } });
        var model = pipeline.Fit(empty);
        Console.WriteLine("✓ Model loaded and fitted successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("✗ Error loading model:");
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.StackTrace);
    }
}

