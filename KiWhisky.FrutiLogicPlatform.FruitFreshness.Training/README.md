FruitFreshness Training microservice
===================================

Este microservicio ofrece una API simple para subir un export de Teachable Machine (zip, carpeta SavedModel o TFJS) y convertirlo a ONNX.

Endpoints
---------
- POST /convert
  - Form field: `file` (form-data file upload)
  - Respuesta JSON con rutas a ONNX y labels.txt y (si se pudo) las etiquetas.

Uso local
--------
1. Instala dependencias (se recomienda crear un virtualenv):

```bash
python -m venv .venv
.venv\Scripts\activate
pip install -r requirements.txt
```

2. Ejecuta:

```bash
python app.py
# o usando gunicorn
gunicorn --bind 0.0.0.0:5001 app:app
```

3. Convertir: POST con curl

```bash
curl -F "file=@/ruta/a/tu/export.zip" http://localhost:5001/convert
```

Notas
-----
- El script `convert_model.py` intenta manejar SavedModel y TFJS (model.json). Si tu export es TFJS, el contenedor necesita `tensorflowjs_converter` disponible (se instala con `tensorflowjs` pip package).
- La conversión puede requerir bastante memoria (TensorFlow). Para producción, usa runners dedicados o servicios con GPU si es necesario.

Integración con `FruitFreshness.API`
-----------------------------------
- El flujo recomendado: subes el modelo al servicio de training, este devuelve ONNX + labels. Luego sube los artefactos a un ModelStore (S3/Azure Blob) y actualiza el servicio `FruitFreshness.API` para descargar el modelo activo en arranque o bajo demanda.

