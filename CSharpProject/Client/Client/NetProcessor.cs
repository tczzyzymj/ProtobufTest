using System.Net;
using System.Net.Sockets;
using System.Reflection;
using Client.Common;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using NFProto;

namespace Client
{
    public class NetProcessor
    {
        private const    int             mBufferSize = 1024;
        private const    string          mIpAddress  = "127.0.0.1";
        private const    int             mIntSize    = sizeof(int);
        private readonly byte[]          mBuffer     = new byte[mBufferSize];
        private          IPEndPoint?     mIpEndPoint;
        private          MemoryStream?   mMemoryStream;
        private          byte[]          mMsgBufferSizeBuffer = new byte[mIntSize];
        private          EnumDescriptor? mMsgMainIdDescriptor;
        private          Socket?         mSocket;

        public bool Init()
        {
            mMsgBufferSizeBuffer = new byte[mIntSize];
            mMemoryStream        = new MemoryStream(mBuffer);
            mSocket              = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mIpEndPoint          = new IPEndPoint(IPAddress.Parse(mIpAddress), 9117);

            try
            {
                mSocket.Connect(mIpEndPoint);
                Console.WriteLine("连接服务器成功!");
            }
            catch (Exception _exception)
            {
                Console.WriteLine(_exception);

                return false;
            }

            mMsgMainIdDescriptor = DefineReflection.Descriptor.FindTypeByName<EnumDescriptor>(nameof(MsgMainIdEnum));

            if (mMsgMainIdDescriptor == null)
            {
                return false;
            }

            return true;
        }

        public void HandleMsg()
        {
            if ((mSocket == null) || !mSocket.Connected)
            {
                // Console.WriteLine("Socket 无效，请检查！");

                return;
            }

            if (mMemoryStream == null)
            {
                Console.WriteLine("错误，mMemoryStream 为空， 请检查!");

                return;
            }

            try
            {
                try
                {
                    Array.Clear(mBuffer, 0, mBufferSize);
                    int _length = mSocket.Receive(mBuffer);

                    if (_length <= 0)
                    {
                        return;
                    }

                    mMemoryStream.Position = 0;

                    while (true)
                    {
                        Array.Clear(mMsgBufferSizeBuffer, 0, mIntSize);
                        mMemoryStream.Read(mMsgBufferSizeBuffer, 0, mIntSize);
                        int _msgLength = BitConverter.ToInt32(mMsgBufferSizeBuffer);

                        if (_msgLength <= 0)
                        {
                            break;
                        }

                        byte[] _finalMsgBuffer = new byte[_msgLength];
                        mMemoryStream.Read(_finalMsgBuffer, 0, _msgLength);
                        NetMsg? _netMsg = NetMsg.Parser.ParseFrom(_finalMsgBuffer);

                        if (_netMsg == null)
                        {
                            Console.WriteLine("数据解析错误，请检查!");

                            continue;
                        }

                        InternalParseMsg(_netMsg.MsgMainId, _netMsg.MsgSubId, _netMsg.MsgContent);
                    }
                }
                catch (SocketException _socketException)
                {
                    if (_socketException.ErrorCode == 10054)
                    {
                        // 远程连接断开了
                        Console.WriteLine(_socketException);
                    }
                    else
                    {
                        Console.WriteLine(_socketException.ToString());
                    }
                }
            }
            catch (Exception _exception)
            {
                Console.WriteLine(_exception);
            }
        }

        public void SendHearBeat()
        {
            if ((mSocket == null) || !mSocket.Connected)
            {
                Console.WriteLine("Socket 无效，请检查！");

                return;
            }

            try
            {
                SendMsg(MsgMainIdEnum.HeatBeat, (int)MsgSubIdEnum.NoSpecific, null);
            }
            catch (Exception _exception)
            {
                Console.WriteLine(_exception);
            }
        }

        public void SendMsg(MsgMainIdEnum MsgMainId, int MsgSubId, IMessage? InMsg)
        {
            if (mSocket == null)
            {
                Console.WriteLine("无连接，请检查!");

                return;
            }

            NetMsg _msg = new NetMsg { MsgMainId = MsgMainId, MsgSubId = MsgSubId };

            if (InMsg != null)
            {
                _msg.MsgContent = InMsg.ToByteString();
            }

            byte[] _msgBuffer   = _msg.ToByteArray();
            byte[] _finalBuffer = new byte[_msgBuffer.Length + 4];

            using (MemoryStream _ms = new MemoryStream(_finalBuffer))
            {
                byte[] _lengthBytes = BitConverter.GetBytes(_msgBuffer.Length);
                _ms.Write(_lengthBytes);
                _ms.Write(_msgBuffer);
            }

            mSocket.Send(_finalBuffer);

            NFLog.Ins().LogError($"发送消息，Main ID : {MsgMainId}, SubID : {MsgSubId}, Content : {InMsg}");
        }

        public void Close()
        {
            if (mSocket != null)
            {
                mSocket.Close();
            }
        }

        public void InternalParseMsg(MsgMainIdEnum InMsgMainId, int InMsgSubId, ByteString InByteString)
        {
            if (mMsgMainIdDescriptor == null)
            {
                mMsgMainIdDescriptor = DefineReflection.Descriptor.FindTypeByName<EnumDescriptor>(nameof(MsgMainIdEnum));
            }

            EnumValueDescriptor? _tempDescriptor = mMsgMainIdDescriptor.FindValueByNumber((int)InMsgMainId);

            if (_tempDescriptor == null)
            {
                NFLog.Ins().LogError($"没有找到目标 {InMsgMainId} 的 Descriptor");

                return;
            }

            EnumValueOptions? _targetOptions = _tempDescriptor.GetOptions();

            if (_targetOptions == null)
            {
                return;
            }

            bool _specificProto = _targetOptions.GetExtension(ExtendExtensions.SpecificProto);

            if (!_specificProto)
            {
                return;
            }

            string? _rspProtoName = _targetOptions.GetExtension(ExtendExtensions.NetRspProto);

            if (string.IsNullOrEmpty(_rspProtoName))
            {
                NFLog.Ins().LogError($"MsgMainIdEnum : {InMsgMainId} ，没有Proto对象，请检查");

                return;
            }

            Type? _type = Type.GetType(_rspProtoName);

            if (_type == null)
            {
                NFLog.Ins().LogError($"无法获取指定 Type , class name : {_rspProtoName}");

                return;
            }

            PropertyInfo? _propertyInfo = _type.GetProperty("Parser");

            if (_propertyInfo == null)
            {
                NFLog.Ins().LogError($"无法获取类：{_rspProtoName} 里面的 Parser 属性，请检查");

                return;
            }

            MethodInfo? _method = _propertyInfo.GetType().GetMethod("ParseFrom", BindingFlags.Default, new[] { typeof(ByteString) });

            if (_method == null)
            {
                NFLog.Ins().LogError("错误，类：{} 的 成员 ： {} ，无法获取方法 ParseFrom(ByteString)");

                return;
            }

            object? _resultObj = _method.Invoke(_propertyInfo, new object[] { InByteString });

            if ((InMsgMainId == MsgMainIdEnum.DailyAsk) && (_resultObj != null))
            {
                S2CDailyAsk? _replyMsg = _resultObj as S2CDailyAsk;

                if (_replyMsg == null)
                {
                    NFLog.Ins().LogError("消息无法转化为 S2CDailyAsk，请检查");
                }
                else
                {
                    NFLog.Ins().LogError("收到消息 : " + _replyMsg);
                }
            }
        }
    }
}
