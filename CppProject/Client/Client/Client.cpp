// Client.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "NetProcessor.h"
#include "Core.pb.h"
#include "DailyAsk.pb.h"

int main()
{
    //NetProcessor* _netProcessor = new NetProcessor();
    //if(!_netProcessor->Init())
    //{
    //    std::cout << L"初始化失败，请检查" << std::endl;
    //    return -1;
    //}

    //while(true)
    //{
    //    
    //}

    C2SDailyAsk _dailyAsk;
    _dailyAsk.set_content("今天是个好日子");
    std::string _serializeStr = _dailyAsk.SerializeAsString();
    if(_serializeStr.length() <= 0)
    {
        std::cout << L"C2SDailyAsk 序列化出错，请检查" << std::endl;
        return -1;
    }

    NetMsg _msg;
    _msg.set_msgmainid(MsgMainIdEnum::DailyAsk);
    _msg.set_msgcontent(_serializeStr);

    std::string _msgStr = _msg.SerializeAsString();
    if (_msgStr.length() <=0)
    {
        std::cout << L"NetMsg 序列化出错，请检查" << std::endl;
        return -1;
    }

    NetMsg _receiveMsg;
    if(!_receiveMsg.ParseFromString(_msgStr))
    {
        std::cout << L"NetMsg 反序列化出错，请检查!" << std::endl;
        return -1;
    }

    std::cout << "MsgMainID is : " << _receiveMsg.msgmainid() << std::endl;

    C2SDailyAsk _receiveDailyAsk;
    if(!_receiveDailyAsk.ParseFromString(_receiveMsg.msgcontent()))
    {
        std::cout << L"C2SDailyAsk 反序列化失败，请检查！" << std::endl;
        return -1;
    }

    std::cout << "序列化消息是：" << _receiveDailyAsk.content() << std::endl;

    return 0;
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
