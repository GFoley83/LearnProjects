using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctarti.DataStructures
{
    public static class BitStuff
    {
        public static bool GetBit(int num, int i)
        {
            return ((num & (1 << i)) != 0);
        }

        public static int SetBit(int num, int i)
        {
            return num | (1 << i);
        }

        public static int ClearBit(int num, int i)
        {
            //Where i=3, mask = 1111 0111
            int mask = ~(1 << i);
            return num & mask;
        }

        public static int UpdateBit(int num, int i, int v)
        {
            //clear bit and then set
            int mask = ~(1 << i);
            return (num & mask) | (v << i);
        }
        public static int RotateLeft(int num, int i)
        {
            //Shift Bits Left
            int a = num << i;

            //Shift All Bits But (32 -i) to Right
            int b = num >> (32 - i);

            //Add Bits Togeather with Bit OR 
            int c = a | b;

            return (num << i) | (num >> (32 - i));
        }
        public static int RotateRight(int num, int i)
        {
            int a = num >> i;
            int b = num << (32 - i);
            int c = a | b;
            return c;
        }

        public static void PrintInt(int num)
        {
            int x = num;
            int counter = 2;

            if (x > 0)
            {
                //Reduce Preceding 0s
                x /= 4;
                while (x >= 256)
                {
                    x /= 2;
                    counter++;
                }
            }
            else
            {
                //Show Entire 32bit
                counter = 8;
            }

            Stack<char> binary = new Stack<char>();
            for (int i = 0; i < 4 * counter; i++)
            {
                if (i % 4 == 0)
                    binary.Push(' ');
                if (GetBit(num, i))
                    binary.Push('1');
                else
                    binary.Push('0');
            }

            StringBuilder sb = new StringBuilder();

            while (binary.Count > 0)
                sb.Append(binary.Pop());

            Console.WriteLine("{0}:\t{1}", num, sb.ToString());
            //Console.WriteLine("{0}: {1}", num, Convert.ToString(num, 2));
        }

        public static void PrintDouble(double d)
        {
            //IFormatProvider p = new pro
            //Console.WriteLine("{0}: {1}", d, Convert.ToString(d, 2));
        }
    }
}
