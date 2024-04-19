namespace Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ServerProcessor _serverProcessor = new ServerProcessor();

            if (!_serverProcessor.Init())
            {
                Console.WriteLine("初始化失败，请检查");

                return;
            }

            bool _process = true;

            Task.Run(
                () =>
                {
                    while (_process)
                    {
                        _serverProcessor.TryAcceptClient();
                    }
                }
            );

            Task.Run(
                () =>
                {
                    while (_process)
                    {
                        _serverProcessor.HandleMsg();
                    }
                }
            );

            Task.Run(
                async () =>
                {
                    while (_process)
                    {
                        _serverProcessor.SendMsg();

                        await Task.Delay(2000);
                    }
                }
            );

            while (_process)
            {
                ConsoleKeyInfo _keyInfo = Console.ReadKey();

                if (_keyInfo.Key == ConsoleKey.Escape)
                {
                    _process = false;
                    break;
                }
            }

            Console.WriteLine("Hello, World!");
        }
    }
}
