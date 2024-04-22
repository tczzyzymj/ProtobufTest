using System.Net;
using System.Net.Sockets;
using System.Text;
using Google.Protobuf;

namespace Server
{
    public class ServerProcessor
    {
        private const    int                        mBufferSize      = 2046;
        private readonly byte[]                     mBuffer          = new byte[mBufferSize];
        private readonly Dictionary<string, Socket> mClientSocketMap = new Dictionary<string, Socket>();
        private readonly int                        mPort            = 9117;
        private          int                        mCount;
        private          int                        mIntSize = 4;
        private          MemoryStream               mMemoryStream;
        private          byte[]                     mMsgBufferSizeBuffer;
        private          Socket?                    mServerSocket;

        public bool Init()
        {
            mIntSize             = sizeof(int);
            mMsgBufferSizeBuffer = new byte[mIntSize];
            mServerSocket        = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

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
                            Array.Clear(mMsgBufferSizeBuffer,0, mIntSize);
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

                            InternalParseMsg(_netMsg.MsgMainID, _netMsg.MsgContent);
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

        public void InternalParseMsg(MsgMainIdEnum msgmainId, ByteString byteString)
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
                    Console.WriteLine("心跳包");
                    break;
                }
                case MsgMainIdEnum.DailyAsk:
                {
                    MsgDailyAsk _msg = MsgDailyAsk.Parser.ParseFrom(byteString);

                    if (_msg == null)
                    {
                        Console.WriteLine("协议无法解析为 MsgDailyAsk ，请检查");
                        return;
                    }
                    Console.WriteLine(_msg.ToString());
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(msgmainId), msgmainId, null);
                }
            }
        }

        public void SendMsg()
        {
            try
            {
                if (mClientSocketMap.Count < 1)
                {
                    Console.WriteLine("尝试发送数据，但是没有任何连接");

                    return;
                }

                ++mCount;

                Socket[] _clientSocketValueArray = mClientSocketMap.Values.ToArray();
                string[] _clientSocketKeyArray   = mClientSocketMap.Keys.ToArray();

                for (int _i = _clientSocketKeyArray.Length - 1; _i >= 0; --_i)
                {
                    if (!_clientSocketValueArray[_i].Connected)
                    {
                        mClientSocketMap.Remove(_clientSocketKeyArray[_i]);

                        continue;
                    }

                    byte[] _buffer  = Encoding.UTF8.GetBytes(mCount.ToString());
                    string _tempMsg = $"发送数据 : {mCount}, 目标 : {_clientSocketKeyArray[_i]}";
                    _clientSocketValueArray[_i].Send(_buffer);
                    Console.WriteLine(_tempMsg);
                }
            }
            catch (Exception _exception)
            {
                Console.WriteLine(_exception);
            }
        }
    }
}
