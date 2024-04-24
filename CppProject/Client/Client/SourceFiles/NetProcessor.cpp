#include "NetProcessor.h"


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
    _address.sin_port             = 9117;
    _address.sin_addr.S_un.S_addr = inet_addr("127.0.0.1");

    int _result = connect(mSocketClient, reinterpret_cast<sockaddr*>(&_address), sizeof(_address));

    if (SOCKET_ERROR == _result)
    {
        std::cout << u8"连接服务器出错，请检查" << '\n';

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

void NetProcessor::HandleMsg()
{
}

void NetProcessor::SendHeartBeat()
{
    
}
