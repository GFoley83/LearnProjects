using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter01
{
    public class Q01_7 : IQuestion
    {
        public void Run()
        {
            int[,] a = {{1, 2, 3}, 
                         {4, 0, 6,}, 
                         {7, 8, 9,},
                         {10, 11, 12}};
            //Print
            Print(a, 4, 3);
            Console.WriteLine("");
            Print(ZeroFindAndSet(a, 4, 3), 4, 3);
        }

        public int [,] ZeroFindAndSet(int[,] a, int m, int n)
        {
            bool[] rows = new bool[m];
            bool[] columns = new bool[n];

            for (int j = 0; j < m; j++)
			{
			    for (int k = 0; k < n; k++)
			    {
			        if (a[j,k] == 0)
                    {
                        rows[j] = true;
                        columns[k] = true;
                    }
			    }
			}


            for (int j = 0; j < m; j++)
			{
                if (rows[j] == true)
                {
                    for (int k = 0; k < n; k++)
			        {
			            a[j,k] = 0;   
			        }
                }
			}

            for (int k = 0; k < n; k++)
			{
			    if (columns[k] == true)
                {
                    for (int j = 0; j < m; j++)
			        {
			            a[j,k] = 0;
			        }
                }
			}

            return a;
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
