@echo off
setlocal enabledelayedexpansion
 
:: 设置Protobuf编译器的路径
set PROTOC_PATH=protoc
 
:: 设置Protobuf消息定义文件的目录
set PROTO_DIR=ProtoFiles
 
:: 设置生成的目标语言（例如：java, csharp）
set OUT_LANG=csharp
 
:: 设置目标目录
set OUT_DIR=./CsharpFiles

set RELATE_DIR=%~dp0

:: 循环删除目标目录的文件和文件夹
del /s /q "%RELATE_DIR%\%OUT_DIR%\*.*"

set ANY_ERROR=0

:: 循环遍历目录下的所有.proto文件
for %%f in (%PROTO_DIR%\*.proto) do (
    %PROTOC_PATH% -I=%PROTO_DIR% --%OUT_LANG%_out=%OUT_DIR% %%f
    if !ERRORLEVEL! neq 0 (
        set ANY_ERROR=1
    )
)

set CLIENT_COPY_DIR=../CSharpProject/Client/Client/ProtoFiles/
set SERVER_COPY_DIR=../CSharpProject/Server/Server/ProtoFiles/

:: 拷贝到CSharp_Client
for %%f in (%OUT_DIR%\*.cs) do (
    copy "%%f" "%CLIENT_COPY_DIR%"
    copy "%%f" "%SERVER_COPY_DIR%"
)

if %ANY_ERROR% == 0 (
    echo All Done !
) else (
    echo Compile Error , Please check !
)

endlocal
pause
