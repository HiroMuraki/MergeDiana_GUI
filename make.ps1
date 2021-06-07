dotnet publish -r win10-x64 -c release --self-contained=False /p:PublishSingleFile=True -o "OutputProgram"
$hasPath = Test-Path "OutputProgram\合成然然.exe"
if ($hasPath) { 
    Move-Item -Force "OutputProgram\合成然然.exe" "OutputProgram\新建文件夹\合成然然.exe"
}
Rename-Item "OutputProgram\MergeDiana_GUI.exe" "合成然然.exe"