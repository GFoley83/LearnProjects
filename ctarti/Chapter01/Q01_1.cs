using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ctarti.Library;

namespace Chapter01
{
    public class Q01_1 : IQuestion
    {
        public void Run()
        {
            //Implement an algorithm to determine if a string has all unique chareters. Cannot use additional data structures.
            string strFalse = "abcdefghijkllmnopo";
            string strTrue = "asdfjk;l09234";

            Console.WriteLine("#1 strFalse={0}, {1}", strFalse, AllUniqueChars1(strFalse));
            Console.WriteLine("#1 strTrue={0}, {1}", strTrue, AllUniqueChars1(strTrue));
        }

        /// <summary>
        /// Solution #1 - Bit Array
        /// </summary>
        /// <param name="str">string to test</param>
        /// <returns>true is all chars are unique in str</returns>
        private bool AllUniqueChars1(string str)
        {
            bool[] bitArray = new bool[128];
            foreach (char c in str)
            {
                if (bitArray[c] == true)
                    return false;
                else
                    bitArray[c] = true;
            }

            return true;
        }

        //Solution #2 - Use 32 Bit Int as Array. Saves space, assumes only lower case (a-z is 26 chars or 32 bits). O(n).
        //Solution #3 - Restrcitions: no other data structures. Sort String first. O(log n) + O(n).
        //Solution #4 - Restrcitions: no other data structures. Compare each char to each other char. O(n^2).
    }
}
