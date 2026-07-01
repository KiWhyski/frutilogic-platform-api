@echo off
set PATH=C:\tfconvenv\Scripts;%PATH%
"C:\tfconvenv\Scripts\python.exe" "C:\Users\PC\RiderProjects\frutilogic-platform-api\KiWhisky.FrutiLogicPlatform.FruitFreshness.Training\convert_model.py" --input "C:\Users\PC\RiderProjects\frutilogic-platform-api\KiWhisky.FrutiLogicPlatform.FruitFreshness.Training\downloads\A9p8vlvFO" --output "C:\Users\PC\RiderProjects\frutilogic-platform-api\KiWhisky.FrutiLogicPlatform.FruitFreshness.API\models\fruit_model.onnx" --labels-out "C:\Users\PC\RiderProjects\frutilogic-platform-api\KiWhisky.FrutiLogicPlatform.FruitFreshness.API\models\labels.txt"
pause

