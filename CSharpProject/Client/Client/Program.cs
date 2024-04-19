namespace Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            NetProcessor _netProcessor = new NetProcessor();

            if (!_netProcessor.Init())
            {
                return;
            }

            bool _process = true;

            try
            {
                Task _taskForHandle = Task.Run(
                    () =>
                    {
                        while (_process)
                        {
                            _netProcessor.HandleMsg();
                        }
                    }
                );

                Task _taskForSend = Task.Run(
                    async () =>
                    {
                        while (_process)
                        {
                            _netProcessor.SendMsg();

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
            }
            catch (Exception _exception)
            {
                Console.WriteLine(_exception);
            }
            finally
            {
                _netProcessor.Close();
            }
        }
    }
}
