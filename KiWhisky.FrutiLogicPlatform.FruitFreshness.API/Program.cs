using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Configuration defaults - can be overridden by appsettings or environment
builder.Configuration.AddInMemoryCollection(new[] {
    new KeyValuePair<string,string?>("Model:Path","models/fruit_model.onnx"),
    new KeyValuePair<string,string?>("Model:Labels","models/labels.txt"),
    new KeyValuePair<string,string?>("Model:Version","1.0")
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register classifier as singleton
var modelPath = builder.Configuration["Model:Path"] ?? "models/fruit_model.onnx";
var labelsPath = builder.Configuration["Model:Labels"] ?? "models/labels.txt";
var modelVersion = builder.Configuration["Model:Version"] ?? "1.0";

// Ensure relative paths are resolved from content root
modelPath = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, modelPath));
labelsPath = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, labelsPath));

builder.Services.AddSingleton(sp => new FruitFreshness.Services.FruitClassifierService(
    modelPath,
    File.Exists(labelsPath) ? File.ReadAllLines(labelsPath) : new[] { "good", "near_expiration", "expired" },
    modelVersion));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.MapControllers();

app.Run();
