using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class ServerProcessor
    {
        private const int mBufferSize = 2046;
        private readonly Dictionary<string, Socket> mClientSocketMap = new Dictionary<string, Socket>();
        private readonly int mPort = 9117;
        private readonly byte[] mBuffer = new byte[mBufferSize];
        private int mCount;
        private Socket? mServerSocket;

        public bool Init()
        {
            mServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress _ipAddress = IPAddress.Parse("127.0.0.1");
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
                string[] _clientSocketKeyArray = mClientSocketMap.Keys.ToArray();

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

                        string _clientMsg = Encoding.UTF8.GetString(mBuffer);
                        Console.WriteLine($"接收客户端 : {_clientSocketKeyArray[_i]} 的消息，内容是：{_clientMsg}");
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
                string[] _clientSocketKeyArray = mClientSocketMap.Keys.ToArray();

                for (int _i = _clientSocketKeyArray.Length - 1; _i >= 0; --_i)
                {
                    if (!_clientSocketValueArray[_i].Connected)
                    {
                        mClientSocketMap.Remove(_clientSocketKeyArray[_i]);
                        continue;
                    }

                    byte[] _buffer = Encoding.UTF8.GetBytes(mCount.ToString());
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
