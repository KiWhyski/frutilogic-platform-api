#!/usr/bin/env python3
"""
Generate a simple dummy ONNX model for fruit classification.
Input: 224x224x3 float32 image
Output: 2-class softmax probabilities
"""

import numpy as np
import onnx
from onnx import helper, TensorProto

# Create input tensor
input_name = 'input'
output_name = 'output'

# Define input shape: [1, 3, 224, 224] (batch, channels, height, width)
# But we'll match the expected preprocessing in the service
input_tensor = helper.make_tensor_value_info(
    input_name, TensorProto.FLOAT, [1, 224, 224, 3]
)

# Define output shape: [1, 2] (batch, num_classes)
output_tensor = helper.make_tensor_value_info(
    output_name, TensorProto.FLOAT, [1, 2]
)

# Create a simple constant node that outputs [0.4, 0.6] (dummy probabilities)
# This is just for testing; we'll replace with the real model later

# Flatten the input to shape [1, 150528]
flatten_node = helper.make_node(
    'Reshape',
    inputs=[input_name],
    outputs=['flattened'],
    dims=[1, -1]  # This won't work with make_node, we need a constant input
)

# Actually, let's create a simpler dummy model using Constant + dummy computation
# Create a constant node with shape [1, 2]
const_output = np.array([[0.4, 0.6]], dtype=np.float32)
const_tensor = helper.make_tensor(
    name='const_output',
    data_type=TensorProto.FLOAT,
    dims=[1, 2],
    vals=const_output.flatten(),
    raw=False,
)

# Create an Identity node that just passes through a constant dummy output
# For a real model, this would be a complex neural network
const_node = helper.make_node(
    'Constant',
    inputs=[],
    outputs=[output_name],
    value=const_tensor
)

# Create the graph
graph = helper.make_graph(
    [const_node],  # nodes
    'DummyFruitClassifier',  # graph name
    [input_tensor],  # inputs (we still need to consume input to be valid)
    [output_tensor],  # outputs
)

# Create the model
model = helper.make_model(graph, producer_name='FruitFreshness', opset_imports=[helper.make_opsetid('', 11)])

# Check and output model
model_output_path = 'fruit_model.onnx'
onnx.checker.check_model(model)
onnx.save(model, model_output_path)
print(f"✓ Dummy ONNX model created: {model_output_path}")
print(f"  Input shape: [1, 224, 224, 3] (NHWC format)")
print(f"  Output shape: [1, 2] (probabilities for 2 classes)")
print(f"  Note: This is a dummy model. Replace with real model after conversion.")

