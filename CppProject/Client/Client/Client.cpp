// Client.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <future>
#include <iostream>
#include "NetProcessor.h"
#include "Core.pb.h"

int main()
{
    SetConsoleOutputCP(65001);

    auto _netProcessor = new NetProcessor();
    if (!_netProcessor->Init())
    {
        return -1;
    }

    bool _process = true;

    auto _asyncForSendHeartBeat = std::async(
        [&]()
        {
            while (_process)
            {
                // 发送心跳包
                _netProcessor->SendHeartBeat();
                Sleep(1000);
            }
        }
    );

    auto _asyncForHandleMsg = std::async(
        [&]()
        {
            while (_process)
            {
                // 发送心跳包
                _netProcessor->HandleMsg();
            }
        }
    );

    int _input;

    while (true)
    {
        _input = _getch(); // 调用非阻塞获取字符函数
        if (_input == 27) // 按下了esc
        {
            _process = false;
            break;
        }
    }

    while (true)
    {
        bool _waitSendHeartBeat = _asyncForSendHeartBeat.wait_for(std::chrono::seconds(0)) == std::future_status::timeout;
        bool _waitHandleMsg = _asyncForHandleMsg.wait_for(std::chrono::seconds(0)) == std::future_status::timeout;
        if(!_waitSendHeartBeat && !_waitHandleMsg)
        {
            break;
        }
    }

    _netProcessor->Close();
    delete(_netProcessor);

    return 0;
}
