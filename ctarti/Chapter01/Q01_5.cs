using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter01
{
    /// <summary>
    /// 	5. Implement a method to perform basic string compression using the counts of repeated charecters.
    /// 	For example, the string aabcccccaaa would be a2b1c5a3. if the "compressed" string would not become 
    /// 	smaller than the original string, your method should return the original string. You can assume 
    /// 	the string has only upper and lower case letters (a-z).
    /// </summary>
    public class Q01_5 : IQuestion
    {
        public void Run()
        {
            string uncompStr;

            uncompStr = "aaa";
            Console.WriteLine("{0}-->{1}", uncompStr, CompressStr(uncompStr));
            uncompStr = "abc";
            Console.WriteLine("{0}-->{1}", uncompStr, CompressStr(uncompStr));
            uncompStr = "abababa";
            Console.WriteLine("{0}-->{1}", uncompStr, CompressStr(uncompStr));
            uncompStr = "AAAAAAAAAAAAbbbbbbbbbbbbbbbDDDDDDDDDDkkkkke";
            Console.WriteLine("{0}-->{1}", uncompStr, CompressStr(uncompStr));
        }

        private string CompressStr(string uncompStr)
        {
            StringBuilder compStr = new StringBuilder();

            string lastChar = uncompStr.Substring(0, 1);
            int charCounter = 1;

            for (int i = 1; i < uncompStr.Length; i++)
            {
                string currentChar = uncompStr.Substring(i, 1);

                if (lastChar == currentChar)
                {
                    charCounter++;
                }
                else
                {
                    //print
                    compStr.AppendFormat("{0}{1}", lastChar, charCounter);
                    if (compStr.Length > uncompStr.Length)
                        return uncompStr;

                    //new char
                    lastChar = currentChar;

                    //reset counter
                    charCounter = 1;
                }
            }

            //Insert Last Char
            compStr.AppendFormat("{0}{1}", lastChar, charCounter);



            return compStr.ToString();
        }
    }
}
