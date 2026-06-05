#!/usr/bin/env python3
"""
convert_model.py

Intenta convertir automáticamente exportes comunes de Teachable Machine a ONNX.

Soporta:
- SavedModel (carpeta con saved_model.pb)
- TFJS Layers model (model.json + weights.bin) -> convierte a SavedModel con tensorflowjs_converter y luego a ONNX

Uso (CLI):
  python convert_model.py --input /path/to/export.zip --output /out/fruit_model.onnx --labels-out /out/labels.txt

Devuelve código 0 en éxito y escribe ONNX y labels.
"""
import argparse
import json
import os
import shutil
import subprocess
import sys
import tempfile
import zipfile
from pathlib import Path


def run(cmd, cwd=None):
    print("RUN:", " ".join(cmd))
    res = subprocess.run(cmd, cwd=cwd)
    if res.returncode != 0:
        raise RuntimeError(f"Command failed: {' '.join(cmd)}")


def find_saved_model_dir(folder: Path):
    # saved_model.pb indicates a TF SavedModel
    for root, dirs, files in os.walk(folder):
        if 'saved_model.pb' in files:
            return Path(root)
    return None


def find_tfjs_model(folder: Path):
    # TFJS layers model has model.json
    for root, dirs, files in os.walk(folder):
        if 'model.json' in files:
            return Path(root) / 'model.json'
    return None


def extract_labels_from_metadata(folder: Path, labels_out: Path):
    # Look for metadata.json or labels.txt
    metadata = None
    labels_file = None
    for root, dirs, files in os.walk(folder):
        if 'labels.txt' in files:
            labels_file = Path(root) / 'labels.txt'
            break
        if 'metadata.json' in files:
            metadata = Path(root) / 'metadata.json'
            break

    if labels_file and labels_file.exists():
        shutil.copy(labels_file, labels_out)
        return True

    if metadata and metadata.exists():
        try:
            j = json.loads(metadata.read_text(encoding='utf-8'))
            # buscar keys comunes que puedan contener labels
            for key in ('labels', 'class_names', 'categories', 'classes'):
                if key in j and isinstance(j[key], list):
                    labels_out.write_text('\n'.join(str(x) for x in j[key]), encoding='utf-8')
                    return True
            # en algunos exports de teachable machine etiquetas están en j['modelSpecs'] o j['metadata']
            def walk(o):
                if isinstance(o, dict):
                    for k, v in o.items():
                        if isinstance(v, list) and all(isinstance(x, str) for x in v):
                            return v
                        r = walk(v)
                        if r:
                            return r
                return None
            found = walk(j)
            if found:
                labels_out.write_text('\n'.join(found), encoding='utf-8')
                return True
        except Exception:
            pass

    return False


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('--input', required=True, help='Path to exported model (zip, saved_model dir, tfjs folder)')
    parser.add_argument('--output', required=True, help='Output ONNX path')
    parser.add_argument('--labels-out', required=True, help='Output labels.txt path')
    parser.add_argument('--opset', default='11', help='ONNX opset (default 11)')
    args = parser.parse_args()

    p = Path(args.input)
    out = Path(args.output)
    labels_out = Path(args.labels_out)

    work = Path(tempfile.mkdtemp(prefix='convert_model_'))
    try:
        # unpack zip if needed
        if p.is_file() and p.suffix.lower() == '.zip':
            with zipfile.ZipFile(p, 'r') as z:
                z.extractall(work)
        elif p.is_dir():
            # copy to work
            shutil.copytree(p, work / 'export', dirs_exist_ok=True)
        else:
            # single file other than zip -> copy
            shutil.copy(p, work / p.name)

        # now inspect
        saved_model = find_saved_model_dir(work)
        if saved_model:
            print('Found SavedModel at', saved_model)
            # try tf2onnx conversion
            cmd = [sys.executable, '-m', 'tf2onnx.convert', '--saved-model', str(saved_model), '--output', str(out), '--opset', args.opset]
            run(cmd)
            # extract labels
            if not extract_labels_from_metadata(work, labels_out):
                print('Warning: no labels found automatically. Create labels.txt manually.')
            print('Converted to', out)
            return 0

        tfjs = find_tfjs_model(work)
        if tfjs:
            print('Found TFJS model at', tfjs)
            sm_out = work / 'saved_model'
            sm_out.mkdir(parents=True, exist_ok=True)
            # Convert TFJS -> SavedModel using tensorflowjs_converter
            cmd = ['tensorflowjs_converter', '--input_format', 'tfjs_layers_model', str(tfjs), str(sm_out)]
            run(cmd)
            # then tf2onnx
            cmd2 = [sys.executable, '-m', 'tf2onnx.convert', '--saved-model', str(sm_out), '--output', str(out), '--opset', args.opset]
            run(cmd2)
            if not extract_labels_from_metadata(work, labels_out):
                print('Warning: no labels found automatically. Create labels.txt manually.')
            print('Converted TFJS -> ONNX to', out)
            return 0

        # look for .tflite
        tflite_files = list(work.rglob('*.tflite'))
        if tflite_files:
            print('Found TFLite model(s):', tflite_files)
            # try tf2onnx convert with --tflite option if available
            tflite_path = str(tflite_files[0])
            cmd = [sys.executable, '-m', 'tf2onnx.convert', '--tflite', tflite_path, '--output', str(out), '--opset', args.opset]
            run(cmd)
            if not extract_labels_from_metadata(work, labels_out):
                print('Warning: no labels found automatically. Create labels.txt manually.')
            print('Converted TFLite -> ONNX to', out)
            return 0

        raise RuntimeError('No supported model format found in input. Provide SavedModel, TFJS export (model.json) or zip containing them.')
    finally:
        try:
            shutil.rmtree(work)
        except Exception:
            pass


if __name__ == '__main__':
    try:
        sys.exit(main())
    except Exception as e:
        print('Error:', e, file=sys.stderr)
        sys.exit(3)

