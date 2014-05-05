using ctarti.DataStructures;
using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter04
{
    public class Q04_4 : IQuestion
    {
        public void Run()
        {
            PreOrderTraversalSolution();
            BreadthFirstTraversalSolution();
        }

        public void PreOrderTraversalSolution()
        {
            int[] sortedArray0 = { 0 };
            int[] sortedArray1 = { 0, 1, 2 };
            int[] sortedArray2 = { 0, 1, 2, 4 };
            int[] sortedArray3 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            BinaryTreeCollection tree = new BinaryTreeCollection();
            //tree.Head = tree.BalancedInsert(sortedArray0, 0, sortedArray0.Length - 1);
            //BinaryTreePrinter.PrintNode(tree.Head);
            //tree.PrintEachDeapthLeavl();
            //Console.ReadKey();

            //tree.Clear();
            //tree.Head = tree.BalancedInsert(sortedArray1, 0, sortedArray1.Length - 1);
            //BinaryTreePrinter.PrintNode(tree.Head);
            //tree.PrintEachDeapthLeavl();
            //Console.ReadKey();

            //tree.Clear();
            //tree.Head = tree.BalancedInsert(sortedArray2, 0, sortedArray2.Length - 1);
            //BinaryTreePrinter.PrintNode(tree.Head);
            //tree.PrintEachDeapthLeavl();
            //Console.ReadKey();

            tree.Clear();
            tree.Head = tree.BalancedInsert(sortedArray3, 0, sortedArray3.Length - 1);
            BinaryTreePrinter.PrintNode(tree.Head);
            tree.PrintEachLevelPOT();
            Console.ReadKey();
        }

        public void BreadthFirstTraversalSolution()
        {
            int[] sortedArray0 = { 0 };
            int[] sortedArray1 = { 0, 1, 2 };
            int[] sortedArray2 = { 0, 1, 2, 4 };
            int[] sortedArray3 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            BinaryTreeCollection tree = new BinaryTreeCollection();
            tree.Clear();
            tree.Head = tree.BalancedInsert(sortedArray3, 0, sortedArray3.Length - 1);
            BinaryTreePrinter.PrintNode(tree.Head);
            tree.PrintEachLevelBFT();
            Console.ReadKey();
        }
    }
}
