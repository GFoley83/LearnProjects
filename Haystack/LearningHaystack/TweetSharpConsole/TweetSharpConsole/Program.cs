using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetSharpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TweetSharpConsole tsc = new TweetSharpConsole();
            tsc.Run();
            Console.ReadKey();
        }
    }
}
