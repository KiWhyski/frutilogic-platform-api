FruitFreshness microservice
==========================

Este microservicio expone un endpoint para clasificar imágenes de frutas según su estado (ej. `good`, `near_expiration`, `expired`) usando un modelo ONNX exportado desde Teachable Machine.

Quick start
-----------

1. Coloca tu modelo ONNX en `models/fruit_model.onnx` dentro del proyecto. Asegúrate de que `models/labels.txt` contenga las etiquetas en el mismo orden que el modelo (una por línea).

2. Ejecuta localmente con dotnet (desde la carpeta del proyecto):

```powershell
dotnet run --project KiWhisky.FrutiLogicPlatform.FruitFreshness.API\KiWhisky.FrutiLogicPlatform.FruitFreshness.API.csproj
```

3. Endpoint de inferencia:

- POST http://localhost:5000/api/v1/classify (multipart/form-data, campo `imageFile`)

Convertir modelo de Teachable Machine a ONNX (ejemplo)
---------------------------------------------------

Desde Teachable Machine descarga el modelo en formato TensorFlow (SavedModel). Luego en una máquina con Python:

```bash
python -m pip install --upgrade pip
pip install tensorflow tf2onnx
python -m tf2onnx.convert --saved-model path/to/saved_model --output fruit_model.onnx --opset 11
```

Si el modelo no se convierte directamente puede que requiera pasos intermedios (TFLite) o ajustar versiones de TensorFlow/tf2onnx.

Agregar el proyecto a la solution
---------------------------------

Desde la raíz del repo:

```powershell
dotnet sln add KiWhisky.FrutiLogicPlatform.FruitFreshness.API\KiWhisky.FrutiLogicPlatform.FruitFreshness.API.csproj
```

Docker
------

```powershell
docker build -t fruit-freshness-api KiWhisky.FrutiLogicPlatform.FruitFreshness.API
docker run -p 5000:80 fruit-freshness-api
```

Notas
-----

- Asegúrate de revisar los nombres de entrada/salida del ONNX (abre con Netron) y ajustar `FruitClassifierService` si no se llaman `input`/`output`.
- Ajusta el preprocesado (resizing, normalización, interleavePixelColors) para coincidir con el entrenamiento original.

