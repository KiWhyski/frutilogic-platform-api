#!/usr/bin/env python3
"""
Crea un modelo ONNX mínimo válido para ML.NET (clasificador binario 224x224 RGB).
Heurística demo: imágenes más brillantes -> "en buen estado".
"""
import sys
from pathlib import Path

try:
    import numpy as np
    import onnx
    from onnx import helper, TensorProto
except ImportError:
    print("Instalando dependencias...")
    import subprocess
    subprocess.check_call([sys.executable, "-m", "pip", "install", "onnx", "numpy", "-q"])
    import numpy as np
    import onnx
    from onnx import helper, TensorProto

def create_minimal_onnx():
    """Crea un modelo ONNX binario para imagen 224x224x3 (IR compatible con ML.NET antiguo)"""
    
    # Entrada: imagen 224x224x3 (float32)
    X = helper.make_tensor_value_info('input', TensorProto.FLOAT, [1, 224, 224, 3])
    
    # Salida: 2 clases (logits)
    Y = helper.make_tensor_value_info('output', TensorProto.FLOAT, [1, 2])
    
    # Reshape [1,224,224,3] -> [1,150528]
    shape_init = np.array([1, 150528], dtype=np.int64)
    shape_tensor = helper.make_tensor(
        name='reshape_shape',
        data_type=TensorProto.INT64,
        dims=[2],
        vals=shape_init
    )

    reshape_node = helper.make_node(
        'Reshape',
        inputs=['input', 'reshape_shape'],
        outputs=['reshaped'],
    )
    
    # Heurística determinista: usar brillo promedio para separar clases.
    # bad_logit  = 0.55 - mean_brightness
    # good_logit = mean_brightness - 0.55
    n_features = 224 * 224 * 3
    w = 1.0 / float(n_features)
    W_init = np.zeros((n_features, 2), dtype=np.float32)
    W_init[:, 0] = -w  # clase 0: "por echarse a perder"
    W_init[:, 1] = +w  # clase 1: "en buen estado"
    b_init = np.array([0.55, -0.55], dtype=np.float32)
    
    W_tensor = helper.make_tensor(
        name='W',
        data_type=TensorProto.FLOAT,
        dims=[150528, 2],
        vals=W_init.tobytes(),
        raw=True
    )
    
    b_tensor = helper.make_tensor(
        name='b',
        data_type=TensorProto.FLOAT,
        dims=[2],
        vals=b_init.tobytes(),
        raw=True
    )
    
    # Nodo Gemm: Y = X @ W + b
    gemm_node = helper.make_node(
        'Gemm',
        inputs=['reshaped', 'W', 'b'],
        outputs=['output'],
        alpha=1.0,
        beta=1.0,
        transB=0
    )
    
    # Crear el grafo
    graph_def = helper.make_graph(
        [reshape_node, gemm_node],
        'fruit_classifier',
        [X],
        [Y],
        [shape_tensor, W_tensor, b_tensor]
    )
    
    # Crear el modelo
    model_def = helper.make_model(
        graph_def,
        producer_name='custom',
        opset_imports=[helper.make_opsetid("", 10)]
    )

    # Fuerza IR compatible con runtimes que reportan max IR version 10.
    model_def.ir_version = 10
    
    # Guardar
    output_path = Path("KiWhisky.FrutiLogicPlatform.FruitFreshness.API/models/fruit_model.onnx")
    output_path.parent.mkdir(parents=True, exist_ok=True)
    
    onnx.save(model_def, str(output_path))
    print(f"✓ Modelo ONNX creado: {output_path}")
    
    # Actualizar labels
    labels_path = Path("KiWhisky.FrutiLogicPlatform.FruitFreshness.API/models/labels.txt")
    with open(labels_path, "w", encoding="utf-8") as f:
        f.write("Fruta por echarse a perder\nFruta en buen estado")
    print(f"✓ Labels actualizados: {labels_path}")
    
    return True

if __name__ == "__main__":
    try:
        success = create_minimal_onnx()
        print("\n✅ Modelo mínimo creado. El microservicio debería funcionar ahora.")
        sys.exit(0 if success else 1)
    except Exception as e:
        print(f"Error: {e}")
        sys.exit(1)

