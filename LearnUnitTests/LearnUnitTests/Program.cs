using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnUnitTests
{
    class Program
    {
        static void Main(string[] args)
        {
            MyService service = new MyService();
            int i = service.Run("4444");
            Console.WriteLine(i);
        }
    }

    public class MyService
    {
        public int Run(string number)
        {
            return int.Parse(number);
        }
    }
}
