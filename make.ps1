dotnet publish -r win10-x64 -c release --self-contained=False /p:PublishSingleFile=True -o "OutputProgram"
$hasPath = Test-Path "OutputProgram\�ϳ�ȻȻ.exe"
if ($hasPath) { 
    Move-Item -Force "OutputProgram\�ϳ�ȻȻ.exe" "OutputProgram\�½��ļ���\�ϳ�ȻȻ.exe"
}
Rename-Item "OutputProgram\MergeDiana_GUI.exe" "�ϳ�ȻȻ.exe"