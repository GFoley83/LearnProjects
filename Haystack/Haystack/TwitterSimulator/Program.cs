using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haystack;

namespace TwitterSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Haystack.TwitterSimulator simulator = new Haystack.TwitterSimulator();
            simulator.Run();
            Console.ReadKey();
        }
    }
}
