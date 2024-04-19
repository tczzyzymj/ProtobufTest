using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class NetProcessor
    {
        private const    int    mBufferSize = 1024;
        private readonly string mIPAddress  = "127.0.0.1";

        private IPEndPoint mIPEndPoint;
        private Socket?    mSocket;
        private int        mTempCount;

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

        public void SendMsg()
        {
            if ((mSocket == null) || !mSocket.Connected)
            {
                Console.WriteLine("Socket 无效，请检查！");

                return;
            }

            try
            {
                ++mTempCount;
                string _msg    = "今天是 : " + mTempCount;
                byte[] _buffer = Encoding.UTF8.GetBytes(_msg);
                mSocket.Send(_buffer);
                Console.WriteLine($"客户端发送数据 : {_msg}");
            }
            catch (Exception _exception)
            {
                Console.WriteLine(_exception);
            }
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
