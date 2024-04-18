namespace Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MsgHandler _msgHandler = new MsgHandler();

            if (!_msgHandler.Init())
            {
                return;
            }

            Console.WriteLine("Hello, World!");

            bool _process = true;

            while (_process)
            {
                // todo : 这里处理消息
            }
        }
    }
}
