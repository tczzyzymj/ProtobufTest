#include "NetProcessor.h"

using namespace NFProto;
using namespace google::protobuf;

bool NetProcessor::Init()
{
    if (mEnumDescriptor == nullptr)
    {
        mEnumDescriptor = google::protobuf::GetEnumDescriptor<MsgMainIdEnum>();
    }
    memset(mReceiveBuffer, 0, mBufferSize);
    memset(mSendBuffer, 0, mBufferSize);

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

bool NetProcessor::IsConnect()
{
    return true;
    //int len;

    //// 获取socket的错误状态
    //if (getsockopt(mSocketClient, SOL_SOCKET, SO_ERROR, err, &len) < 0) {
    //    std::cout << "get sockopt failed" << "\n";
    //    return false; // 获取错误失败，假定socket不再连接
    //}

    //return err == 0; // 如果err为0，表示socket连接正常
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

void NetProcessor::SendMsg(MsgMainIdEnum InMsgMainIdEnum, int InMsgSubId, Message* InMsg)
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

    int _bigEndianMsgLength = 0;
    if (!mIsBigEndian)
    {
        _bigEndianMsgLength = static_cast<int>(htonl(_sendMsgLength));
    }
    else
    {
        _bigEndianMsgLength = _sendMsgLength;
    }

    int _finalSize = mSizeOfInt + _sendMsgLength;

    memset(mSendBuffer, 0, mBufferSize);

    // 写入数据长度
    char* _writePtr = mSendBuffer;
    memcpy(_writePtr, &_bigEndianMsgLength, mSizeOfInt);

    // 最后写入proto
    if (_sendMsgLength > 0)
    {
        char* _writeMsgPtr = _writePtr + mSizeOfInt;
        memcpy(_writeMsgPtr, _sendMsgStr.c_str(), _sendMsgLength);
    }

    const int _sendResult = send(mSocketClient, mSendBuffer, _finalSize, 0);
    if (_sendResult == SOCKET_ERROR || _sendResult == 0)
    {
        std::cout << u8"发送消息失败，ID : " << InMsgMainIdEnum << u8", 错误内容：" << WSAGetLastError() << "\n";
    }
    else
    {
        std::cout << u8"发送消息 , MsgId：" << InMsgMainIdEnum << "\n";
    }

    delete(_sendMsg);
}

void NetProcessor::HandleMsg()
{
    memset(mReceiveBuffer, 0, mBufferSize);
    int _receiveLength = recv(mSocketClient, mReceiveBuffer, mBufferSize, 0);
    if (_receiveLength <= 0)
    {
        return;
    }

    int _msgLength = 0;
    memcpy(&_msgLength, mReceiveBuffer, mSizeOfInt);

    _msgLength = static_cast<int>(ntohl(_msgLength));

    if (_msgLength <= 0)
    {
        std::cout << u8"错误，解析的数据长度为0，请检查" << "\n";
    }
    char*  mFromPtr = mReceiveBuffer + mSizeOfInt;
    NetMsg _replyMsg;
    if (!_replyMsg.ParseFromArray(mFromPtr, _msgLength))
    {
        std::cout << u8"解析数据出错，请检查" << "\n";
        return;
    }
    auto _msgMainIdEnum = _replyMsg.msgmainid();
    std::cout << u8"收到消息 , MainId : " << _msgMainIdEnum << u8", SubId :" << _replyMsg.msgsubid() << "\n";
    InternalHandleMsg(_msgMainIdEnum, _replyMsg.msgsubid(), _replyMsg.msgcontent());
}

void NetProcessor::SendHeartBeat()
{
    SendMsg(HeatBeat, NoSpecific, nullptr);
}

void NetProcessor::InternalHandleMsg(MsgMainIdEnum InMsgMainIdEnum, int InMsgSubId, const std::string& InMsgData)
{
    if (mEnumDescriptor == nullptr)
    {
        mEnumDescriptor = google::protobuf::GetEnumDescriptor<MsgMainIdEnum>();
    }

    const EnumValueDescriptor* _tempDescriptor = mEnumDescriptor->FindValueByNumber(InMsgMainIdEnum);
    if (_tempDescriptor == nullptr)
    {
        std::cout << u8"获取 MsgMainIdEnum 的 EnumValueDescriptor 失败，请检查" << "\n";
        return;
    }

    EnumValueOptions _options   = _tempDescriptor->options();
    bool             _extension = _options.GetExtension(SpecificProto);
    if (!_extension)
    {
        return;
    }

    const std::string& _rspProtoName = _options.GetExtension(NetRspProto);
    if (_rspProtoName.length() <= 0)
    {
        std::cout << u8"MsgMainID : " << InMsgMainIdEnum << u8", 没有指定回复协议，请检查" << "\n";
        return;
    }

    const Descriptor* _desc   = DescriptorPool::generated_pool()->FindMessageTypeByName(_rspProtoName);
    Message*          _msgIns = MessageFactory::generated_factory()->GetPrototype(_desc)->New();
    if (_msgIns == nullptr)
    {
        std::cout << "协议 Proto ：" << _rspProtoName << ", 无法实例化，请检查" << "\n";
        return;
    }

    // 这里用 Event 去发送消息，让注册的地方自行处理内容即可
    if (InMsgMainIdEnum == MsgMainIdEnum::DailyAsk)
    {
        S2CDailyAsk* _finalMsg = dynamic_cast<S2CDailyAsk*>(_msgIns);
        if(_finalMsg==nullptr)
        {
            std::cout << u8"协议转换出错，无法转化为 S2CDailyAsk ，请检查" << "\n";
            return;
        }
        if(!_finalMsg->ParseFromString(InMsgData))
        {
            std::cout << u8"协议: S2CDailyAsk 反序列化出错" << "\n";
            return;
        }
        const std::string _replyContent = _finalMsg->content();
        std::cout << _replyContent << "\n";
    }

    _desc = nullptr;
    delete(_msgIns);
    _msgIns = nullptr;
}
