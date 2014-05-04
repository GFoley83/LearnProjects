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
using Chapter05;
using ctarti.DataStructures;

namespace ctarti
{
    class Program
    {
        static void Main(string[] args)
        {
            RunQuestions();
            //TestBTPrinter();
            //TestBkST();
            //TestGraph();

            Console.WriteLine("\nPress [Enter] to quit");
            Console.ReadLine();
        }

        static private void TestBST()
        {
            BinaryTreeCollection tree = new BinaryTreeCollection();
            tree.GenerateRandomCollection(10, 1, 100);
            tree.Add(new DataStructures.BinaryTreeNode(50));
            tree.PrintCollection();
            tree.Remove(tree.SearchForNode(new DataStructures.BinaryTreeNode(50)));
            Console.WriteLine("");
            tree.PrintCollection();
        }

        static private void TestGraph()
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
            graph.GenerateRandomCollection(10, 10, 30);
            //graph.PrintCollection();

            //Console.WriteLine("\n\nDeapthFirstTraversal:");
            //graph.DeapthFirstTraversal(graph.Nodes.First<GraphNode>());

            //graph.ResetVisitedNodes();

            //Console.WriteLine("\n\nDeapthFirstTraversal:");
            //graph.BreadthFirstTraversal(graph.Nodes.First<GraphNode>());




        }

        static private void TestBTPrinter()
        {
            //Library.BinaryTreeNode n = new Library.BinaryTreeNode(5);
            //n.InsertInOrder(4);
            //n.InsertInOrder(7);
            
            //n.InsertInOrder(6);

            //n.InsertInOrder(2);
            //n.InsertInOrder(3);
            //n.InsertInOrder(88);
            BinaryTreeCollection tree = new BinaryTreeCollection();
            tree.GenerateRandomCollection(6, 0, 26);

            BinaryTreePrinter.PrintNode(tree.Head);
            int i;
        }

        static private void RunQuestions()
        {
            IQuestion[] questions = new IQuestion[]
            {
                new Q01_1(), 
                new Q01_3(), 
                new Q01_5(),
                new Q01_6(),
                new Q01_7(),
                new Q01_8(),

                new Q02_1(),
                new Q02_2(),
                new Q02_3(),
                new Q02_4(),
                new Q02_5(),
                new Q02_6(),
                new Q02_7(),

                new Q03_1(),
                new Q03_2(),
                new Q03_3(),
                new Q03_4(),
                new Q03_5(),
                new Q03_6(),
                new Q03_7(),

                //new Q04_1(),
                //new Q04_2(),
                //new Q04_3(),
                //new Q04_4(),
                //new Q04_5(),
                //new Q04_6(),
                //new Q04_7(),
                //new Q04_8(),
                //new Q04_9(),

                new Q05_1(),
                new Q05_2()
                //new Q05_3(),
                //new Q05_4(),
                //new Q05_5(),
                //new Q05_6(),
                //new Q05_7(),
                //new Q05_8(),



            };

            foreach (IQuestion q in questions)
            {
                Console.WriteLine(string.Format("{0}{1}", Environment.NewLine, Environment.NewLine));
                Console.WriteLine(string.Format("// Executing: {0}", q.GetType().ToString()));
                Console.WriteLine("// ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ----");

                q.Run();
            }

            Console.WriteLine(string.Format("{0}{1}", Environment.NewLine, Environment.NewLine));
        }
    }
}
