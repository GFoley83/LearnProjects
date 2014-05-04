using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            hanoi(3, "source", "sparse", "dest");
            while (true)
            { 
            }
        }

        static void hanoi(int n, string source, string sparse, string destination)
        {
            if (n == 0)
            {
               
                Console.WriteLine(source);
                Console.WriteLine("To");
                Console.WriteLine(destination);
                Console.WriteLine("\n");
            }
            else
            {
                hanoi(n - 1, source, destination, sparse);
                Console.WriteLine(source);
                Console.WriteLine("To");
                Console.WriteLine(destination);
                Console.WriteLine("\n");
                hanoi(n - 1, sparse, source, destination);
            
            }
        
        }
    }
}
