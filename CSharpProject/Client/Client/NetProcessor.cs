using System.Net;
using System.Net.Sockets;
using System.Text;
using Google.Protobuf;

namespace Client
{
    public class NetProcessor
    {
        private const    int    mBufferSize = 1024;
        private readonly string mIPAddress  = "127.0.0.1";
        private          int    mAskCount;

        private IPEndPoint mIPEndPoint;
        private Socket?    mSocket;

        public bool Init()
        {
            mSocket     = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mIPEndPoint = new IPEndPoint(IPAddress.Parse(mIPAddress), 9117);

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
                byte[] _buffer = new byte[mBufferSize];

                try
                {
                    int _length = mSocket.Receive(_buffer);

                    if (_length > 0)
                    {
                        string _serverMsgStr = Encoding.UTF8.GetString(_buffer);
                        Console.WriteLine("接收到数据 : " + _serverMsgStr);
                    }
                }
                catch (SocketException _socketException)
                {
                    if (_socketException.ErrorCode == 10054)
                    {
                        // 远程连接断开了
                        Console.WriteLine(_socketException);
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
                    MsgDailyAsk _msg = new MsgDailyAsk();
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
    }
}
