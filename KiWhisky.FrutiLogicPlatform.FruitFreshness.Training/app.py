"""Flask app to accept model export upload and convert to ONNX using convert_model.py"""
import os
import tempfile
import subprocess
from pathlib import Path
from flask import Flask, request, jsonify

app = Flask(__name__)

CONVERT_SCRIPT = Path(__file__).parent / 'convert_model.py'

@app.route('/convert', methods=['POST'])
def convert():
    if 'file' not in request.files:
        return jsonify({'error': 'no file uploaded, expected field "file"'}), 400
    f = request.files['file']
    name = f.filename or 'upload'
    workdir = Path(tempfile.mkdtemp(prefix='tm_upload_'))
    try:
        inpath = workdir / name
        f.save(str(inpath))
        out_onnx = workdir / 'model.onnx'
        out_labels = workdir / 'labels.txt'
        cmd = [
            'python', str(CONVERT_SCRIPT),
            '--input', str(inpath),
            '--output', str(out_onnx),
            '--labels-out', str(out_labels)
        ]
        proc = subprocess.run(cmd, capture_output=True, text=True)
        if proc.returncode != 0:
            return jsonify({'error': 'conversion failed', 'stdout': proc.stdout, 'stderr': proc.stderr}), 500
        # return paths relative or content
        res = {'onnx_path': str(out_onnx), 'labels_path': str(out_labels)}
        # optionally include labels contents
        if out_labels.exists():
            res['labels'] = out_labels.read_text(encoding='utf-8').splitlines()
        return jsonify(res)
    finally:
        # keep workdir for debugging; in production, remove after some time
        pass

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5001)

