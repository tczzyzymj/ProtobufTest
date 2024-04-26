using System.Net;
using System.Net.Sockets;
using Google.Protobuf;
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
        private          int                        mReceiveCount;
        private          Socket?                    mServerSocket;

        public bool Init()
        {
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

                            InternalParseMsg(_netMsg.MsgMainId, _netMsg.MsgContent, _clientSocketValueArray[_i]);
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

        public void InternalParseMsg(MsgMainIdEnum msgmainId, ByteString byteString, Socket targetSocket)
        {
            switch (msgmainId)
            {
                case MsgMainIdEnum.Invalid:
                {
                    Console.WriteLine("协议转换出错，请检查！");

                    break;
                }
                case MsgMainIdEnum.HeatBeat:
                {
                    Console.WriteLine("接收心跳包");

                    break;
                }
                case MsgMainIdEnum.DailyAsk:
                {
                    C2SDailyAsk _msg = C2SDailyAsk.Parser.ParseFrom(byteString);

                    if (_msg == null)
                    {
                        Console.WriteLine("协议无法解析为 MsgDailyAsk ，请检查");

                        return;
                    }

                    Console.WriteLine(_msg.ToString());
                    S2CDailyAsk _replyMsg = new S2CDailyAsk();
                    ++mReceiveCount;
                    _replyMsg.Content = $"哟西，已收到 {mReceiveCount} 次";
                    SendMsg(MsgMainIdEnum.DailyAsk, (int)MsgSubIdEnum.NoSpecific, targetSocket, _replyMsg);

                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(msgmainId), msgmainId, null);
                }
            }
        }

        public void SendMsg(MsgMainIdEnum MainIdEnum, int SubIdEnum, Socket TargetSocket, IMessage? TargetMsg)
        {
            if (TargetSocket == null)
            {
                Console.WriteLine("无连接，请检查!");

                return;
            }

            NetMsg _msg = new NetMsg { MsgMainId = MainIdEnum, MsgSubId = SubIdEnum };

            if (TargetMsg != null)
            {
                _msg.MsgContent = TargetMsg.ToByteString();
            }

            byte[] _msgBuffer   = _msg.ToByteArray();
            byte[] _finalBuffer = new byte[_msgBuffer.Length + 4];

            using (MemoryStream _ms = new MemoryStream(_finalBuffer))
            {
                byte[] _lengthBytes = BitConverter.GetBytes(_msgBuffer.Length);
                _ms.Write(_lengthBytes);
                _ms.Write(_msgBuffer);
            }

            TargetSocket.Send(_finalBuffer);

            Console.WriteLine($"发送消息，Main ID : {MainIdEnum}, SubID : {SubIdEnum}, Content : {TargetMsg}");
        }
    }
}
