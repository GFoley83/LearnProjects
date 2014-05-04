using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ctarti.DataStructures;

namespace Chapter05
{
    public class Q05_1 : IQuestion
    {
        public void Run()
        {
            int m = 19;     //0000 0001 0011
            int n = 1032;   //0100 0000 0000
            int i = 2;
            int j = 6;

            BitStuff.PrintInt(m);

            Console.WriteLine("Clear j to i");
            BitStuff.PrintInt(n);
            for (; i < j + 1; i++)
            {
                int mask = ~(1 << i);
                n = n & mask;

            }
            BitStuff.PrintInt(n);

            int x = n | (m << 2);
            BitStuff.PrintInt(x);


        }
    }
}
