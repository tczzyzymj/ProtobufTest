#pragma once
#include <stdio.h>
#include "conio.h"
#include "windows.h"
#include "winsock.h"
#include <iostream>
#pragma comment(lib, "ws2_32.lib")

class NetProcessor
{
public:
    bool Init();

    bool IsConnect();

    bool Close();

    bool SendMsg();

    void HandleMsg();

private:
    SOCKET mSocketClient = NULL;

    std::string MyName = "米克";
};
