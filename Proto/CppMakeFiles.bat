@echo off
setlocal enabledelayedexpansion
 
:: 设置Protobuf编译器的路径
set PROTOC_PATH=protoc.exe
 
:: 设置Protobuf消息定义文件的目录
set PROTO_DIR=ProtoFiles
 
:: 设置生成的目标语言（例如：java, csharp）
set OUT_LANG=cpp
 
:: 设置目标目录
set OUT_DIR=./CppFiles
 
:: 循环删除目标目录的文件
for %%f in (%OUT_DIR%\*) do (
    del /s /q "%%f"
)

set ANY_ERROR=0

:: 循环遍历目录下的所有.proto文件
for %%f in (%PROTO_DIR%\*.proto) do (
    %PROTOC_PATH% %%f --%OUT_LANG%_out=%OUT_DIR%
    if !ERRORLEVEL! neq 0 (
        set ANY_ERROR=1
    )
)

if %ANY_ERROR% == 0 (
    echo All Done !
) else (
    echo Compile Error , Please check !
)

endlocal
pause
