using NFProto;

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
                Task _taskForHandleMsg = Task.Run(
                    () =>
                    {
                        while (_process)
                        {
                            _netProcessor.HandleMsg();
                        }
                    }
                );

                Task _taskForHearBeat = Task.Run(
                    async () =>
                    {
                        while (_process)
                        {
                            _netProcessor.SendHearBeat();

                            await Task.Delay(1000);
                        }
                    }
                );


                Task _taskForDailyAsk = Task.Run(
                    async () =>
                    {
                        while (_process)
                        {
                            C2SDailyAsk _msg = new C2SDailyAsk();
                            _msg.Content = "今天是第几次询问？";
                            _netProcessor.SendMsg(MsgMainIdEnum.DailyAsk, 0, _msg);

                            await Task.Delay(1000);
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

                while (_taskForHearBeat.Wait(0) || _taskForHandleMsg.Wait(0) || _taskForDailyAsk.Wait(0))
                {
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
