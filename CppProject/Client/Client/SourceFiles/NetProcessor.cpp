#include "NetProcessor.h"

bool NetProcessor::Init()
{
    mSocketClient = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

    if(INVALID_SOCKET == mSocketClient)
    {
        std::cout << L"初始化出错，请检查!" << std::endl;
        return false;
    }

    sockaddr_in _addr;
    _addr.sin_family = AF_INET;
    _addr.sin_port = 9117;

    return true;
}
