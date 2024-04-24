#pragma once
#include <stdio.h>
#include "conio.h"
#include "windows.h"
#include "winsock.h"
#include <iostream>
#include "google\protobuf\message.h"
#include "Core.pb.h"
#include "DailyAsk.pb.h"

#pragma comment(lib, "ws2_32.lib")

class NetProcessor
{
public:
    bool Init();

    bool IsConnect();

    void Close();

    bool SendMsg(MsgMainIdEnum MsgMainIdEnum, INT32 MsgSubIdEnum, google::protobuf::Message &InMsg);

    void HandleMsg();

    void SendHeartBeat();

private:
    SOCKET mSocketClient = NULL;
};
