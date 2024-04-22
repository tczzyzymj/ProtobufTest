using System.Net;
using System.Net.Sockets;
using Google.Protobuf;

namespace Client
{
    public class NetProcessor
    {
        private const    int          mBufferSize = 1024;
        private readonly byte[]       mBuffer     = new byte[mBufferSize];
        private readonly string       mIPAddress  = "127.0.0.1";
        private          int          mAskCount;
        private          int          mIntSize = 4;
        private          IPEndPoint   mIPEndPoint;
        private          MemoryStream mMemoryStream;
        private          byte[]       mMsgBufferSizeBuffer;
        private          Socket?      mSocket;

        public bool Init()
        {
            mIntSize             = sizeof(int);
            mMsgBufferSizeBuffer = new byte[mIntSize];
            mMemoryStream        = new MemoryStream(mBuffer);
            mSocket              = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mIPEndPoint          = new IPEndPoint(IPAddress.Parse(mIPAddress), 9117);

            try
            {
                mSocket.Connect(mIPEndPoint);
                Console.WriteLine("连接服务器成功!");
            }
            catch (Exception _exception)
            {
                Console.WriteLine(_exception);

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

                        InternalParseMsg(_netMsg.MsgMainID, _netMsg.MsgContent);
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
                //{
                //    SendMsg(MsgMainIdEnum.HeatBeat, MsgSubIdEnum.NoSpecific, null);
                //}

                {
                    ++mAskCount;
                    C2SDailyAsk _msg = new C2SDailyAsk();
                    _msg.Content = $"这是第 {mAskCount} 次询问";
                    SendMsg(MsgMainIdEnum.DailyAsk, MsgSubIdEnum.NoSpecific, _msg);
                }
            }
            catch (Exception _exception)
            {
                Console.WriteLine(_exception);
            }
        }

        public void SendMsg(MsgMainIdEnum mainIdEnum, MsgSubIdEnum subIdEnum, IMessage? targetMsg)
        {
            if (mSocket == null)
            {
                Console.WriteLine("无连接，请检查!");

                return;
            }

            NetMsg _msg = new NetMsg { MsgMainID = mainIdEnum, MsgSubID = subIdEnum };

            if (targetMsg != null)
            {
                _msg.MsgContent = targetMsg.ToByteString();
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

            Console.WriteLine($"发送消息，Main ID : {mainIdEnum}, SubID : {subIdEnum}, Content : {targetMsg}");
        }

        public void Close()
        {
            if (mSocket != null)
            {
                mSocket.Close();
            }
        }

        private void InternalParseMsg(MsgMainIdEnum msgmainId, ByteString byteString)
        {
            switch (msgmainId)
            {
                case MsgMainIdEnum.Invalid:
                {
                    break;
                }
                case MsgMainIdEnum.HeatBeat:
                {
                    break;
                }
                case MsgMainIdEnum.DailyAsk:
                {
                    S2CDailyAsk _replyMsg = S2CDailyAsk.Parser.ParseFrom(byteString);
                    Console.WriteLine("收到消息 : " + _replyMsg);
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(msgmainId), msgmainId, null);
                }
            }
        }
    }
}
