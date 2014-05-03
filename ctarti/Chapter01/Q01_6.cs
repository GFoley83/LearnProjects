using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter01
{
    public class Q01_6 : IQuestion
    {
        public void Run()
        {
            int[,] a = {{1, 2, 3}, 
                         {4, 5, 6,}, 
                         {7, 8, 9,},
                         {10, 11, 12}};
            //Print
            Print(a, 4, 3);
            Console.WriteLine("");
            Print(Rotate90dRight(a, 4, 3), 3, 4);

        }

        private int[,] Rotate90dRight(int[,] a, int m, int n)
        {
            int[,] b = new int[n,m];
            for (int j = 0; j < m; j++)
			{
			    for (int k = 0; k < n; k++)
			    {
			        b[k, m -1 - j] = a[j,k];
			    }
			}

            return b;
        }

        private void Swap(int[,] b, int r, int c)
        {
            int tmp = b[r, c];
            b[r, c] = b[c, r];
            b[c, r] = tmp;
        }

        private void Print(int[,] a, int m, int n)
        {
            for (int j = 0; j < m; j++)
            {
                for (int k = 0; k < n; k++)
                {
                    Console.Write("{0}\t", a[j, k]);
                }
                Console.Write("\n");
            }
        }

    }
}
