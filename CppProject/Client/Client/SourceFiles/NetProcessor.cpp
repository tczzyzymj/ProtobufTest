#include "NetProcessor.h"

bool NetProcessor::Init()
{
    mSocketClient = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

    if (INVALID_SOCKET == mSocketClient)
    {
        std::cout << L"初始化出错，请检查!" << std::endl;
        return false;
    }

    sockaddr_in _addr;
    _addr.sin_family           = AF_INET;
    _addr.sin_port             = 9117;
    _addr.sin_addr.S_un.S_addr = inet_addr("127.0.0.1");

    int _result = connect(mSocketClient, reinterpret_cast<sockaddr*>(&_addr), sizeof(_addr));

    if (SOCKET_ERROR == _result)
    {
        std::cout << L"连接服务器出错，请检查" << std::endl;

        return false;
    }

    std::cout << L"连接服务器成功" << std::endl;
    std::string _content = "haha";

    int _sendReulst = send(mSocketClient, _content.c_str(), static_cast<int>(_content.length()), 0);
    if(SOCKET_ERROR == _sendReulst)
    {
        std::cout << L"发送协议出错，请假查" << std::endl;
    }

    return true;
}
