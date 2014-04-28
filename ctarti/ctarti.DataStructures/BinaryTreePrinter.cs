using System;
using System.Collections.Generic;

namespace ctarti.DataStructures
{
    public class BinaryTreePrinter
    {
        public static void PrintNode(BinaryTreeNode root) {
            int maxLevel = BinaryTreePrinter.MaxLevel(root);

            PrintNodeInternal(new List<BinaryTreeNode>() { root }, 1, maxLevel);
        }

        private static void PrintNodeInternal(List<BinaryTreeNode> nodes, int level, int maxLevel) {
            if (nodes.Count==0 || BinaryTreePrinter.IsAllElementsNull(nodes))
                return;

            int floor = maxLevel - level;
            int endgeLines = (int) Math.Pow(2, (Math.Max(floor - 1, 0)));
            int firstSpaces = (int) Math.Pow(2, (floor)) - 1;
            int betweenSpaces = (int) Math.Pow(2, (floor + 1)) - 1;

            BinaryTreePrinter.PrintWhitespaces(firstSpaces);

            List<BinaryTreeNode> newNodes = new List<BinaryTreeNode>();
            foreach (BinaryTreeNode node in nodes) {
                if (node != null) {
                    Console.Write(node.Data);
                    newNodes.Add(node.Left);
                    newNodes.Add(node.Right);
                } else {
                    newNodes.Add(null);
                    newNodes.Add(null);
                    Console.Write(" ");
                }

                BinaryTreePrinter.PrintWhitespaces(betweenSpaces);
            }
            Console.WriteLine();

            for (int i = 1; i <= endgeLines; i++) {
                for (int j = 0; j < nodes.Count; j++) {
                    BinaryTreePrinter.PrintWhitespaces(firstSpaces - i);
                    if (nodes[j] == null) {
                        BinaryTreePrinter.PrintWhitespaces(endgeLines + endgeLines + i + 1);
                        continue;
                    }

                    if (nodes[j].Left != null)
                        Console.Write("/");
                    else
                        BinaryTreePrinter.PrintWhitespaces(1);

                    BinaryTreePrinter.PrintWhitespaces(i + i - 1);

                    if (nodes[j].Right != null)
                        Console.Write("\\");
                    else
                        BinaryTreePrinter.PrintWhitespaces(1);

                    BinaryTreePrinter.PrintWhitespaces(endgeLines + endgeLines - i);
                }

                Console.WriteLine();
            }

            PrintNodeInternal(newNodes, level + 1, maxLevel);
        }

        private static void PrintWhitespaces(int count)
        {
            string padding = string.Format("{0}", count);
            padding = "{0," + padding + "}";
            Console.Write(padding, " ");
            //for (int i = 0; i < count; i++)
            //    Console.Write(" ");
        }

        //Recursively finds deapth
        private static int MaxLevel(BinaryTreeNode node) {
            if (node == null)
                return 0;

            return Math.Max(BinaryTreePrinter.MaxLevel(node.Left), BinaryTreePrinter.MaxLevel(node.Right)) + 1;
        }

        private static bool IsAllElementsNull<T>(IEnumerable<T> list) {
            foreach (object o in list) {
                if (o != null)
                    return false;
            }

            return true;
        }
    }
}
