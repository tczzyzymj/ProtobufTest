using System.Net;
using System.Net.Sockets;
using System.Reflection;
using Client.Common;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using NFProto;

namespace Server
{
    public class ServerProcessor
    {
        private const    int                        mBufferSize      = 1024;
        private const    int                        mSizeOfInt       = sizeof(int);
        private readonly byte[]                     mBuffer          = new byte[mBufferSize];
        private readonly Dictionary<string, Socket> mClientSocketMap = new Dictionary<string, Socket>();
        private readonly byte[]                     mIntSizeBuffer   = new byte[mSizeOfInt];
        private readonly int                        mPort            = 9117;
        private          MemoryStream?              mMemoryStream;
        private          EnumDescriptor?            mMsgMainIdDescriptor;
        private          int                        mReceiveCount;
        private          Socket?                    mServerSocket;

        public bool Init()
        {
            mMsgMainIdDescriptor = DefineReflection.Descriptor.FindTypeByName<EnumDescriptor>(nameof(MsgMainIdEnum));

            if (mMsgMainIdDescriptor == null)
            {
                return false;
            }

            mServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            mMemoryStream = new MemoryStream(mBuffer);

            IPAddress  _ipAddress  = IPAddress.Parse("127.0.0.1");
            IPEndPoint _ipEndPoint = new IPEndPoint(_ipAddress, mPort);

            // 绑定
            mServerSocket.Bind(_ipEndPoint);

            // 开始监听
            mServerSocket.Listen(1000);

            Console.WriteLine("服务器启动，开始监听");

            return true;
        }

        public void TryAcceptClient()
        {
            if (mServerSocket == null)
            {
                Console.WriteLine("服务器没有启动，请检查");

                return;
            }

            Socket _clientSocket = mServerSocket.Accept();

            if ((_clientSocket != null) && _clientSocket.Connected && (_clientSocket.RemoteEndPoint != null))
            {
                string? _remoteIpAddress = _clientSocket.RemoteEndPoint.ToString();

                if (!string.IsNullOrEmpty(_remoteIpAddress))
                {
                    if (mClientSocketMap.TryGetValue(_remoteIpAddress, out Socket? _recordClientSocket))
                    {
                        _recordClientSocket.Disconnect(false);
                        Console.WriteLine("存在连接，关闭旧连接，IP是：" + _remoteIpAddress);
                    }

                    mClientSocketMap[_remoteIpAddress] = _clientSocket;
                    Console.WriteLine("添加了连接，IP：" + _remoteIpAddress);
                }
                else
                {
                    Console.WriteLine("连接出错，客户端的IP无效，请检查！");
                }
            }
        }

        private void InternalFlushBuffer()
        {
            Array.Clear(mBuffer, 0, mBufferSize);
        }

        public void HandleMsg()
        {
            if (mMemoryStream == null)
            {
                Console.WriteLine("mMemoryStream 为空，请检查");

                return;
            }

            if (mIntSizeBuffer == null)
            {
                Console.WriteLine("mIntSizeBuffer 为空，请检查");

                return;
            }

            try
            {
                if (mClientSocketMap.Count < 1)
                {
                    return;
                }

                Socket[] _clientSocketValueArray = mClientSocketMap.Values.ToArray();
                string[] _clientSocketKeyArray   = mClientSocketMap.Keys.ToArray();

                for (int _i = _clientSocketKeyArray.Length - 1; _i >= 0; --_i)
                {
                    if (_clientSocketValueArray[_i] == null)
                    {
                        mClientSocketMap.Remove(_clientSocketKeyArray[_i]);

                        continue;
                    }

                    if (!_clientSocketValueArray[_i].Connected)
                    {
                        mClientSocketMap.Remove(_clientSocketKeyArray[_i]);

                        continue;
                    }

                    InternalFlushBuffer();

                    try
                    {
                        int _length = _clientSocketValueArray[_i].Receive(mBuffer);

                        if (_length <= 0)
                        {
                            continue;
                        }

                        mMemoryStream.Position = 0;

                        while (true)
                        {
                            bool _shouldReverseByte = BitConverter.IsLittleEndian;

                            Array.Clear(mIntSizeBuffer, 0, mSizeOfInt);
                            mMemoryStream.Read(mIntSizeBuffer, 0, mSizeOfInt);

                            if (_shouldReverseByte)
                            {
                                Array.Reverse(mIntSizeBuffer);
                            }

                            int _msgLength = BitConverter.ToInt32(mIntSizeBuffer);

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

                            NFLog.Ins().LogInfo($"收到消息, MainId : {_netMsg.MsgMainId}, SubId : {_netMsg.MsgSubId}");
                            InternalParseMsg(_netMsg, _clientSocketValueArray[_i]);
                        }
                    }
                    catch (SocketException _socketException)
                    {
                        Console.WriteLine(_socketException);
                    }
                }
            }
            catch (Exception _exception)
            {
                Console.WriteLine(_exception);
            }
        }

        private void InternalParseMsg(NetMsg InMsg, Socket InSocket)
        {
            MsgMainIdEnum _msgMainId = InMsg.MsgMainId;

            if (_msgMainId == MsgMainIdEnum.HeatBeat)
            {
                NFLog.Ins().LogInfo($"收到:{InSocket.RemoteEndPoint} 心跳包");

                return;
            }

            if (mMsgMainIdDescriptor == null)
            {
                mMsgMainIdDescriptor = DefineReflection.Descriptor.FindTypeByName<EnumDescriptor>(nameof(MsgMainIdEnum));
            }

            EnumValueDescriptor? _tempDescriptor = mMsgMainIdDescriptor.FindValueByNumber((int)_msgMainId);

            if (_tempDescriptor == null)
            {
                NFLog.Ins().LogError($"没有找到目标 {_msgMainId} 的 Descriptor");

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

            string? _rspProtoName = _targetOptions.GetExtension(ExtendExtensions.NetReqProto);

            if (string.IsNullOrEmpty(_rspProtoName))
            {
                NFLog.Ins().LogError($"MsgMainIdEnum : {_msgMainId} ，没有Proto对象，请检查");

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

            MethodInfo? _method = _propertyInfo.PropertyType.GetMethod("ParseFrom", new[] { typeof(ByteString) });

            if (_method == null)
            {
                NFLog.Ins().LogError($"错误，类：{_rspProtoName} 的 成员 ： Parser ，无法获取方法 ParseFrom(ByteString)");

                return;
            }

            object? _resultMsgIns = _method.Invoke(_propertyInfo.GetValue(null), new object[] { InMsg.MsgContent });

            if (_resultMsgIns == null)
            {
                NFLog.Ins().LogError($"协议反序列化出错，目标类：{_rspProtoName},请检查");

                return;
            }

            // 这里用 Event 发送要消息给注册了的目标就行，这里是临时的
            if (_msgMainId == MsgMainIdEnum.DailyAsk)
            {
                C2SDailyAsk? _receiveMsg = _resultMsgIns as C2SDailyAsk;

                if (_receiveMsg == null)
                {
                    NFLog.Ins().LogError("消息无法转化为 S2CDailyAsk，请检查");

                    return;
                }

                NFLog.Ins().LogInfo($"收到日常询问 : {_receiveMsg.Content}");

                S2CDailyAsk _sendMsg = new S2CDailyAsk();
                ++mReceiveCount;
                _sendMsg.Content = $"今天是第 {mReceiveCount} 次询问";
                SendMsg(MsgMainIdEnum.DailyAsk, (int)MsgSubIdEnum.NoSpecific, InSocket, _sendMsg);
            }
        }

        public void SendMsg(MsgMainIdEnum InMsgMainId, int InSubId, Socket InSocket, IMessage? InMsg)
        {
            if ((InSocket == null) || !InSocket.Connected)
            {
                Console.WriteLine("无连接，请检查!");

                return;
            }

            NetMsg _msg = new NetMsg { MsgMainId = InMsgMainId, MsgSubId = InSubId };

            if (InMsg != null)
            {
                _msg.MsgContent = InMsg.ToByteString();
            }

            byte[] _msgBuffer   = _msg.ToByteArray();
            byte[] _finalBuffer = new byte[_msgBuffer.Length + 4];

            using (MemoryStream _ms = new MemoryStream(_finalBuffer))
            {
                byte[] _lengthBytes = BitConverter.GetBytes(_msgBuffer.Length);

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(_lengthBytes);
                }

                _ms.Write(_lengthBytes);
                _ms.Write(_msgBuffer);
            }

            InSocket.Send(_finalBuffer);

            Console.WriteLine($"发送消息，Main ID : {InMsgMainId}, SubID : {InSubId}, Content : {InMsg}");
        }
    }
}
