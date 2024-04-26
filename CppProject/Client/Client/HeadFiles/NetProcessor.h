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

    void SendMsg(NFProto::MsgMainIdEnum InMsgMainIdEnum, int MsgSubId, google::protobuf::Message* InMsg);

    void HandleMsg();

    void SendHeartBeat();

private:
    SOCKET mSocketClient = NULL;
    u_short mConnectPort = 9117;
    const int mSizeOfInt = sizeof(int);
    const bool mIsBigEndian = static_cast<int>(htonl(1) == 1);

    void InternalHandleMsg(NFProto::MsgMainIdEnum InMsgMainIdEnum, int InMsgSubId, std::string& InMsgData);
};
