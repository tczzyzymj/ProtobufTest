#pragma once
#include <stdio.h>
#include "conio.h"
#include "windows.h"
#include "winsock.h"
#include <iostream>
#include "google/protobuf/message.h"
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

    ~NetProcessor()
    {
        if (mEnumDescriptor != nullptr)
        {
            delete(mEnumDescriptor);
            mEnumDescriptor = nullptr;
        }
    }

private:
    char                                    mReceiveBuffer[1024];
    char                                    mSendBuffer[1024];
    const bool                              mIsBigEndian    = static_cast<int>(htonl(1) == 1);
    const google::protobuf::EnumDescriptor* mEnumDescriptor = nullptr;
    const int                               mBufferSize     = 1024;
    const int                               mSizeOfInt      = sizeof(int);
    SOCKET                                  mSocketClient   = NULL;
    u_short                                 mConnectPort    = 9117;

    void InternalHandleMsg(NFProto::MsgMainIdEnum InMsgMainIdEnum, int InMsgSubId,const std::string& InMsgData);
};
