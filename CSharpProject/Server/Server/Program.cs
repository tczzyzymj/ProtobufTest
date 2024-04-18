namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MsgHandler _msg = new MsgHandler();

            if (!_msg.Init())
            {
                Console.WriteLine("初始化失败，请检查");
                return;
            }

            bool _process = true;

            while (_process)
            {
                // TODO : 这里处理消息相关
            }

            Console.WriteLine("Hello, World!");
        }
    }
}
