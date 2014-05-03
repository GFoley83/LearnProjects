using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter01
{
    public class Q01_3 : IQuestion
    {
        public void Run()
        {
            string str1 = "BC";
            string str2 = "ABCD";

            Console.WriteLine("IsSubString({0}, {1}) ==> {2}", str1, str2, IsSubString(str1, str2));
            str1 = "bcda";
            str2 = "cd";
            Console.WriteLine("IsSubString({0}, {1}) ==> {2}", str1, str2, IsSubString(str1, str2));
            str1 = "A";
            str2 = "B";
            Console.WriteLine("IsSubString({0}, {1}) ==> {2}", str1, str2, IsSubString(str1, str2));
            str1 = "dxy78";
            str2 = "xy88";
            Console.WriteLine("IsSubString({0}, {1}) ==> {2}", str1, str2, IsSubString(str1, str2));

            char c = '3';

        }

        private bool IsSubString(string str1, string str2)
        {
            //Ensure str1 is >= str2
            if (str1.Length < str2.Length)
            {
                string tmp = str1;
                str1 = str2;
                str2 = tmp;
            }

            int j = 0;

            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] == str2[j])
                {
                    if (str2.Length == j + 1)
                        //Success
                        return true;
                    else
                        j++;
                }
                else
                    //Research Search
                    j = 0;
            }

            //No Match Found
            return false;
        }

    }
}
