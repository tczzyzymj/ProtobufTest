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

    NetMsg _msg;
    _msg.set_msgmainid(MsgMainIdEnum::DailyAsk);

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
