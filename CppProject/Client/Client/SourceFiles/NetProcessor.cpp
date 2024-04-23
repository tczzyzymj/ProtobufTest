#include "NetProcessor.h"

bool NetProcessor::Init()
{
    mSocketClient = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

    if(INVALID_SOCKET == mSocketClient)
    {
        std::cout << L"��ʼ����������!" << std::endl;
        return false;
    }

    sockaddr_in _addr;
    _addr.sin_family = AF_INET;
    _addr.sin_port = 9117;

    return true;
}
