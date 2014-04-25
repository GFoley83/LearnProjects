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
            MyBinarySearchTree<int> tree = new MyBinarySearchTree<int>();
            tree.Add(8);
            tree.Add(9);
            tree.Add(10);
            tree.Add(7);
            tree.Add(6);
            tree.Add(4);
            tree.Add(103);
            tree.Add(44);
            tree.Add(23);


            //foreach (int  i in tree)
            //{
            //    Console.WriteLine("ProcessNode={0}", i);
            //}

            //tree.PreOrderTraversal();

            //tree.InOrderTraversal();

            //tree.PostOrderTraversal();

            Console.WriteLine("IsBinarySearchTree={0}", tree.IsBinarySearchTree());

            //IQuestion[] questions = new IQuestion[]
            //{
            //    // Chapters
            //    new Q01_1(), 
            //    new Q02_1(),
            //    new Q03_1(),
            //    new Q04_1()
            //};

            //foreach (IQuestion q in questions)
            //{
            //    Console.WriteLine(string.Format("{0}{1}", Environment.NewLine, Environment.NewLine));
            //    Console.WriteLine(string.Format("// Executing: {0}", q.GetType().ToString()));
            //    Console.WriteLine("// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ----");

            //    q.Run();
            //}

            //Console.WriteLine(string.Format("{0}{1}", Environment.NewLine, Environment.NewLine));
            Console.WriteLine("Press [Enter] to quit");
            Console.ReadLine();
        }
    }
}
