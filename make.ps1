dotnet publish -r win10-x64 -c release --self-contained=False /p:PublishSingleFile=True -o "OutputProgram"
ren "OutputProgram\MergeDiana_GUI.exe" "合成然然.exe"