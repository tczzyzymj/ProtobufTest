@echo off
setlocal enabledelayedexpansion
 
:: 设置Protobuf编译器的路径
set PROTOC_PATH=protoc
 
:: 设置Protobuf消息定义文件的目录
set PROTO_DIR=ProtoFiles
 
:: 设置生成的目标语言（例如：java, csharp）
set OUT_LANG=cpp
 
:: 设置目标目录
set OUT_DIR=CppFiles

:: 当前相对路径
set RELATE_DIR=%~dp0

set TEMP_OUT_DIR=%RELATE_DIR%%PROTO_DIR%\%OUT_DIR%

set CLIENT_COPY_DIR=../CppProject/Client/Client/ProtoFiles/

if not EXIST "%TEMP_OUT_DIR%" (
    mkdir "%TEMP_OUT_DIR%"
)
 
:: 删除目标目录的文件
del /s /q "%RELATE_DIR%\%OUT_DIR%\*.*"

set ANY_ERROR=0

:: 循环遍历目录下的所有.proto文件
for %%f in (%PROTO_DIR%\*.proto) do (
    %PROTOC_PATH% -I=%PROTO_DIR% --%OUT_LANG%_out=%OUT_DIR% %%f
    if !ERRORLEVEL! neq 0 (
        set ANY_ERROR=1
        GOTO END_LABEL
    )
)

:END_LABEL
if %ANY_ERROR% == 0 (
    for %%f in (%TEMP_OUT_DIR%\%PROTO_DIR%\*.*) do (
        move %%f %RELATE_DIR%%OUT_DIR%\%%~nxf
    )

    :: 拷贝到 Client
    for %%f in (%OUT_DIR%\*.*) do (
        copy "%%f" "%CLIENT_COPY_DIR%"
    )

    rd /s /q %TEMP_OUT_DIR%

    echo All Done !
) else (
    echo Compile Error , Please check !
)
endlocal
pause
