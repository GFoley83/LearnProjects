using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctarti.Library
{
    public static class Logger
    {
        public static void LogEnter(string className, string methodName, List<string> param)
        {
            Console.WriteLine("Entering {0}.{1}", className, methodName);
        }

        public static void LogExit(string className, string methodName, List<string> returnValue)
        {
            Console.WriteLine("Exiting {0}.{1}", className, methodName);
        }
    }
}
