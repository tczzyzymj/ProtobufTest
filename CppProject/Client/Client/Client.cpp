// Client.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
//#include "NetProcessor.h"
//#include "Core.pb.h"
//#include "DailyAsk.pb.h"

bool IsUTF8(const void* pBuffer, int size)
{
    bool           IsUTF8 = false;
    auto           start  = (unsigned char*)pBuffer;
    unsigned char* end    = (unsigned char*)pBuffer + size;
    while (start < end)
    {
        if (*start < 0x80) // (10000000): 值小于0x80的为ASCII字符 
        {
            start++;
        }
        else if (*start < (0xC0)) // (11000000): 值介于0x80与0xC0之间的为无效UTF-8字符 
        {
            IsUTF8 = false;
            break;
        }
        else if (*start < (0xE0)) // (11100000): 此范围内为2字节UTF-8字符 
        {
            IsUTF8 = true;
            if (start >= end - 1)
                break;
            if ((start[1] & (0xC0)) != 0x80)
            {
                IsUTF8 = false;
                break;
            }
            start += 2;
        }
        else if (*start < (0xF0)) // (11110000): 此范围内为3字节UTF-8字符 
        {
            IsUTF8 = true;
            if (start >= end - 2)
                break;
            if ((start[1] & (0xC0)) != 0x80 || (start[2] & (0xC0)) != 0x80)
            {
                IsUTF8 = false;
                break;
            }
            start += 3;
        }
        else if (*start < (0xF8)) // (11111000): 此范围内为4字节UTF-8字符 
        {
            IsUTF8 = true;
            if (start >= end - 3)
                break;
            if ((start[1] & (0xC0)) != 0x80 || (start[2] & (0xC0)) != 0x80 || (start[3] & (0xC0)) != 0x80)
            {
                IsUTF8 = false;
                break;
            }
            start += 4;
        }
        else
        {
            IsUTF8 = false;
            break;
        }
    }
    return IsUTF8;
}

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

    std::cout << "天气不错啊" << std::endl;
    //std::string _content = "你好，今天天气不错";

    //std::cout << _content << std::endl;

    //   C2SDailyAsk _dailyAsk;
    //   std::string _content = "你好，今天天气不错";
    //bool _bIsUTF8 = IsUTF8(&_content, _content.length());
    //if (_bIsUTF8)
    //{
    //	std::cout << "true" << std::endl;
    //}
    //else
    //{
    //	std::cout << "false" << std::endl;
    //}

    //_dailyAsk.set_content(_content);
    //std::string _serializeStr = _dailyAsk.SerializeAsString();
    //if(_serializeStr.length() <= 0)
    //{
    //    std::cout << "序列化出错" << std::endl;
    //    return -1;
    //}

    //NetMsg _msg;
    //_msg.set_msgmainid(MsgMainIdEnum::DailyAsk);
    //_msg.set_msgcontent(_serializeStr);

    //std::string _msgStr = _msg.SerializeAsString();
    //if (_msgStr.length() <=0)
    //{
    //    std::cout << "NetMsg 序列化出错，请检查" << std::endl;
    //    return -1;
    //}

    //NetMsg _receiveMsg;
    //if(!_receiveMsg.ParseFromString(_msgStr))
    //{
    //    std::cout << "NetMsg 反序列化出错，请检查!" << std::endl;
    //    return -1;
    //}

    //std::cout << "MsgMainID is : " << _receiveMsg.msgmainid() << std::endl;

    //C2SDailyAsk _receiveDailyAsk;
    //if(!_receiveDailyAsk.ParseFromString(_receiveMsg.msgcontent()))
    //{
    //    std::cout << "C2SDailyAsk 反序列化失败，请检查！" << std::endl;
    //    return -1;
    //}

    //std::cout << "序列化消息是：" << _receiveDailyAsk.content() << std::endl;

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
