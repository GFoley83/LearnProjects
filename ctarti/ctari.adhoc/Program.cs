using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctari.adhoc
{
    class Program
    {
        static void Main(string[] args)
        {
            Array2D();


            Console.ReadKey();
        }

        private static void Array2D()
        {
            // The same array with dimensions specified. 
            int[,] array2D = new int[4, 2] { { 1, 2 }, 
                                             { 3, 4 }, 
                                             { 5, 6 }, 
                                             { 7, 8 } };
            array2D[3, 0] = 99;

            // Accessing array elements.
            System.Console.WriteLine(array2D[0, 0]);
            System.Console.WriteLine(array2D[0, 1]);
            System.Console.WriteLine(array2D[1, 0]);
            System.Console.WriteLine(array2D[1, 1]);
            System.Console.WriteLine(array2D[3, 0]);

            //Output:
            //1
            //2
            //3
            //4
            //7
        }

        private static void IntAddr()
        {
            int number = 4;

            unsafe
            {
                int* p = &number;

                // Commenting the following statement will remove the 
                // initialization of number.
                //*p = 0xffff;

                // Print the value of *p:
                System.Console.WriteLine("Value at the location pointed to by p: {0:X}", *p);

                // Print the address stored in p:
                System.Console.WriteLine("The address stored in p: {0}", (int)p);
            }

        }
    }
}
