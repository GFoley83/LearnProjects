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
using ctarti.DataStructures;

namespace ctarti
{
    class Program
    {
        static void Main(string[] args)
        {
            TestDataStructures();

            Console.WriteLine("Press [Enter] to quit");
            Console.ReadLine();
        }

        static private void TestDataStructures()
        {
            //LinkedListCollection llc = new LinkedListCollection();
            //llc.Add(new DataStructures.LinkedListNode(0));
            //llc.Add(new DataStructures.LinkedListNode(1));
            //llc.Add(new DataStructures.LinkedListNode(2));
            //llc.Add(new DataStructures.LinkedListNode(3));

            ArrayCollection ac = new ArrayCollection();
            //ac.BubbleSort();
            //ac.SelectSort();
            //ac.PrintCollection();
            //ac.MergeSort();
            //ac.PrintCollection();
            //ac.QuickSort();

            GraphCollection graph = new GraphCollection();
            graph = ((GraphCollection)graph.GenerateRandomCollection(10, 10, 30));
            graph.PrintCollection();

            Console.WriteLine("\n\nDeapthFirstTraversal:");
            graph.DeapthFirstTraversal(graph.Nodes.First<GraphNode>());

            graph.ResetVisitedNodes();

            Console.WriteLine("\n\nDeapthFirstTraversal:");
            graph.BreadthFirstTraversal(graph.Nodes.First<GraphNode>());
        }

        static private void RunQuestions()
        {
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
        }
    }
}
