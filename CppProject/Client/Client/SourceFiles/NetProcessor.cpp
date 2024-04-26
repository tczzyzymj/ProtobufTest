#include "NetProcessor.h"

using namespace NFProto;

bool NetProcessor::Init()
{
    WSADATA _wsaData;
    int     _wsaStartResult = WSAStartup(MAKEWORD(2, 2), &_wsaData);
    if (_wsaStartResult != 0)
    {
        // 错误处理
        std::cout << u8"加载 winsock.dll 失败，请检查！" << "\n";
        return false;
    }

    mSocketClient = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (INVALID_SOCKET == mSocketClient)
    {
        std::cout << u8"初始化出错，请检查!" << '\n';
        return false;
    }

    sockaddr_in _address;
    _address.sin_family           = AF_INET;
    _address.sin_port             = htons(mConnectPort);
    _address.sin_addr.S_un.S_addr = inet_addr("127.0.0.1");

    int _result = connect(mSocketClient, reinterpret_cast<sockaddr*>(&_address), sizeof(_address));

    if (SOCKET_ERROR == _result)
    {
        std::cout << u8"连接服务器出错，错误内容：" << WSAGetLastError() << '\n';

        return false;
    }

    std::cout << u8"连接服务器成功" << '\n';

    return true;
}

void NetProcessor::Close()
{
    if (mSocketClient)
    {
        closesocket(mSocketClient);
        mSocketClient = NULL;
    }

    WSACleanup(); // 清理网络环境
}

void NetProcessor::SendMsg(MsgMainIdEnum InMsgMainIdEnum, int InMsgSubId, google::protobuf::Message* InMsg)
{
    auto _sendMsg = new NetMsg();
    _sendMsg->set_msgmainid(InMsgMainIdEnum);
    _sendMsg->set_msgsubid(InMsgSubId);

    if (InMsg != nullptr)
    {
        std::string _msgString = InMsg->SerializeAsString();
        _sendMsg->set_msgcontent(_msgString);
    }

    std::string _sendMsgStr    = _sendMsg->SerializeAsString();
    int         _sendMsgLength = _sendMsgStr.length();

    int _bigEndianMsgLength = static_cast<int>(htonl(_sendMsgLength));

    int  _finalSize   = mSizeOfInt + _sendMsgLength;

    // 分配内存空间
    char* _sendBuffer = new char[_finalSize];

    // 写入数据长度
    char* _writePtr = _sendBuffer;
    memcpy(_writePtr, &_bigEndianMsgLength, mSizeOfInt);

    // 最后写入proto
    if (_sendMsgLength > 0)
    {
        char* _writeMsgPtr = _writePtr + mSizeOfInt;
        memcpy(_writeMsgPtr, _sendMsgStr.c_str(), _sendMsgLength);
    }

    const int _sendResult = send(mSocketClient, _sendBuffer, _finalSize, 0);
    if (_sendResult == SOCKET_ERROR || _sendResult == 0)
    {
        std::cout << u8"发送消息失败，ID : " << InMsgMainIdEnum << u8", 错误内容：" << WSAGetLastError() << "\n";
    }
    else
    {
        std::cout << u8"发送消息：" << InMsgMainIdEnum << "\n";
    }

    delete[](_sendBuffer);
    delete(_sendMsg);
}

void NetProcessor::HandleMsg()
{
}

void NetProcessor::SendHeartBeat()
{
    SendMsg(HeatBeat, NoSpecific, nullptr);
}

void NetProcessor::InternalHandleMsg(MsgMainIdEnum InMsgMainIdEnum, int InMsgSubId, std::string& InMsgData)
{
}
