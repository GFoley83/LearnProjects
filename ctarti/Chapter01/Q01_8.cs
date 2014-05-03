using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter01
{
    public class Q01_8 :IQuestion
    {
        public void Run()
        {
            string str1 = "abc";
            string str2 = "cab";
            Console.WriteLine("{0},{1} ? {2}", str1, str2, IsRotation(str1, str2));
            str1 = "abc";
            str2 = "acb";
            Console.WriteLine("{0},{1} ? {2}", str1, str2, IsRotation(str1, str2));
        }

        private bool IsRotation(string str1, string str2)
        {
            if (str1.Length != str2.Length)
                return false;

            str1 += str1;

            return str1.Contains(str2);
        }

    }
}
