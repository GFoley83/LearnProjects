using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ctarti.Library;
using Chapter01;
using Chapter02;
using Chapter03;
using Chapter04;

namespace ctarti
{
    class Program
    {
        static void Main(string[] args)
        {
            IQuestion[] questions = new IQuestion[]
            {
                // Chapters
                new Q01_1(), 
                new Q02_1(),
                new Q03_1(),
                new Q04_1()
            };

            foreach (IQuestion q in questions)
            {
                Console.WriteLine(string.Format("{0}{1}", Environment.NewLine, Environment.NewLine));
                Console.WriteLine(string.Format("// Executing: {0}", q.GetType().ToString()));
                Console.WriteLine("// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ----");

                q.Run();
            }

            Console.WriteLine(string.Format("{0}{1}", Environment.NewLine, Environment.NewLine));
            Console.WriteLine("Press [Enter] to quit");
            Console.ReadLine();
        }
    }
}
