using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Common
{
    public class NFLog
    {
        public static NFLog Ins()
        {
            return mIns;
        }

        private static NFLog mIns = new NFLog();

        public void LogMsg(string ContentStr)
        {
            Console.WriteLine(ContentStr);
        }

        public void LogWarning(string ContentStr)
        {
            Console.WriteLine(ContentStr);
        }

        public void LogError(string ContentStr)
        {
            Console.WriteLine(ContentStr);
        }
    }
}
