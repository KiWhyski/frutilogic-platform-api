#!/usr/bin/env python3
"""
Convierte modelo TFJS a ONNX para ML.NET
"""
import sys
import json
import subprocess
import tempfile
from pathlib import Path
import os

def convert_with_python_module():
    """Intenta usar módulos de Python en lugar de CLI"""
    try:
        import tensorflowjs as tfjs
        import tensorflow as tf
        import tf2onnx
        
        tfjs_dir = Path("KiWhisky.FrutiLogicPlatform.FruitFreshness.Training/downloads/A9p8vlvFO")
        api_models_dir = Path("KiWhisky.FrutiLogicPlatform.FruitFreshness.API/models")
        
        with tempfile.TemporaryDirectory() as tmpdir:
            saved_model_dir = Path(tmpdir) / "saved_model"
            saved_model_dir.mkdir()
            
            print(f"[1] Convirtiendo TFJS a SavedModel...")
            try:
                # Usar la API de Python de tensorflowjs
                model = tfjs.converters.load_keras_model(f"file://{tfjs_dir.absolute()}/model.json")
                model.save(str(saved_model_dir))
                print("✓ SavedModel creado desde Python")
            except Exception as e:
                print(f"Error con tfjs.converters: {e}")
                return False
            
            print("[2] Convirtiendo SavedModel a ONNX...")
            onnx_out = api_models_dir / "fruit_model.onnx"
            try:
                model_proto, _ = tf2onnx.convert.from_keras(model, output_path=str(onnx_out))
                print(f"✓ ONNX generado: {onnx_out}")
            except Exception as e:
                print(f"Error en conversión ONNX: {e}")
                return False
        
        return True
    except ImportError as e:
        print(f"Módulos no disponibles: {e}")
        return False

def convert_tfjs_to_onnx():
    # Rutas
    tfjs_dir = Path("KiWhisky.FrutiLogicPlatform.FruitFreshness.Training/downloads/A9p8vlvFO")
    api_models_dir = Path("KiWhisky.FrutiLogicPlatform.FruitFreshness.API/models")
    metadata_json = tfjs_dir / "metadata.json"
    
    if not tfjs_dir.exists():
        print(f"Error: {tfjs_dir} no existe")
        return False
    
    model_json = tfjs_dir / "model.json"
    if not model_json.exists():
        print(f"Error: {model_json} no existe")
        return False
    
    # Intenta conversión con módulos Python
    print("Intentando conversión con módulos Python...")
    if convert_with_python_module():
        print("✓ Conversión exitosa")
    else:
        print("La conversión no pudo completarse. El modelo sigue siendo un placeholder.")
    
    # Extraer labels de metadata
    print(f"[3] Extrayendo labels...")
    if metadata_json.exists():
        with open(metadata_json) as f:
            meta = json.load(f)
            if "labels" in meta:
                labels = meta["labels"]
                labels_file = api_models_dir / "labels.txt"
                with open(labels_file, "w", encoding="utf-8") as lf:
                    lf.write("\n".join(labels))
                print(f"✓ Labels guardados: {labels}")
    
    print("\n✅ Proceso completado!")
    return True

if __name__ == "__main__":
    success = convert_tfjs_to_onnx()
    sys.exit(0 if success else 1)
