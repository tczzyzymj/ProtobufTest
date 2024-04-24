// Client.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "NetProcessor.h"
#include "Core.pb.h"
#include "DailyAsk.pb.h"
#include <string>

int main()
{
    SetConsoleOutputCP(65001);

    NetProcessor* _netProcessor = new NetProcessor();
    if(!_netProcessor->Init())
    {
        return -1;
    }

    while(true)
    {
    }

    return 0;
}
