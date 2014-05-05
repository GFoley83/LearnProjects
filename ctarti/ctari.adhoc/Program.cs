using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ctarti.DataStructures;
using ctarti.DataStructures.Sorting;

namespace ctari.adhoc
{
    class Program
    {
        static void Main(string[] args)
        {
            //Bit();
            //SortStuff();
            while (true)
            {
                TreeStuff();
                Console.ReadKey();
            }

        }

        /*         4
                  / \
                 /   \
                /     \
               /       \
               2        7
              / \      / \
             /   \    /   \
             0    3   5     8
                       \     \
                        6     9
        */

        private static void TreeStuff()
        {
            int[] sortedArray0 = { 0 };
            int[] sortedArray1 = { 0, 1, 2 };
            int[] sortedArray2 = { 0, 1, 2, 4 };
            int[] sortedArray3 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] unsortedArray0 = { 4, 2, 7, 0, 3, 5, 8, 6, 9 };


            BinaryTreeCollection tree = new BinaryTreeCollection();
           
            tree.Clear();
            foreach (int i in unsortedArray0)
                tree.Add(new BinaryTreeNode(i));
            BinaryTreePrinter.PrintNode(tree.Head);
            tree.FindCommonParent(tree.Head, tree.Head.Left.Left, tree.Head.Left.Right, new FindCommonParentResults());

            BinaryTreePrinter.PrintNode(tree.Head);
            tree.FindCommonParent(tree.Head, tree.Head, tree.Head.Left.Right, new FindCommonParentResults());

            BinaryTreePrinter.PrintNode(tree.Head);
            tree.FindCommonParent(tree.Head, tree.Head.Right.Right.Right, tree.Head.Left.Right, new FindCommonParentResults());

            Console.ReadKey();

        }

        private static void SortStuff()
        {
            ArrayCollection master = new ArrayCollection();
            master.GenerateRandomCollection(100, 0, 1000);

            //Buble Sort
            ArrayCollection buble = new ArrayCollection((int[])master.Items.Clone());
            buble.SortViaStrategy(SortStrategyType.BubbleSort);
            buble.PrintCollection();

            //Select Sort
            ArrayCollection select = new ArrayCollection((int[])master.Items.Clone());
            select.SortViaStrategy(SortStrategyType.SelectSort);
            select.PrintCollection();

            //Insert Sort
            //i.e. Sorted Array or List


            //Merge Sort
            ArrayCollection merge = new ArrayCollection((int[])master.Items.Clone());
            merge.SortViaStrategy(SortStrategyType.MergeSort);
            merge.PrintCollection();

            //Quick Sort
            ArrayCollection quick = new ArrayCollection((int[])master.Items.Clone());
            quick.SortViaStrategy(SortStrategyType.QuickSort);
            quick.PrintCollection();
        }

        private static void Bit()
        {
            int a = 10;
            int b = 1;
            int c = 0;

            //Console.WriteLine("ROTL|SHL = num * 2");
            //BitStuff.PrintInt(a);
            //BitStuff.PrintInt(BitStuff.RotateLeft(a, 1));
            //BitStuff.PrintInt(a << 1);

            //Console.WriteLine("\nROTR = num / 2");
            //BitStuff.PrintInt(a);
            //BitStuff.PrintInt(BitStuff.RotateRight(a, 1));
            //BitStuff.PrintInt(a >> 1);

            BitStuff.PrintInt(BitStuff.GetBit(1, 0) ? 1 : 0);
            Console.WriteLine("{0}", (3 & (1 << 0)));

            
        }

        private static void Array2D()
        {
            // The same array with dimensions specified. 
            int[,] array2D = new int[4, 2] { { 1, 2 }, 
                                             { 3, 4 }, 
                                             { 5, 6 }, 
                                             { 7, 8 } };
            array2D[3, 0] = 99;

            // Accessing array elements.
            System.Console.WriteLine(array2D[0, 0]);
            System.Console.WriteLine(array2D[0, 1]);
            System.Console.WriteLine(array2D[1, 0]);
            System.Console.WriteLine(array2D[1, 1]);
            System.Console.WriteLine(array2D[3, 0]);

            //Output:
            //1
            //2
            //3
            //4
            //7
        }
        private static void IntAddr()
        {
            int number = 4;

            unsafe
            {
                int* p = &number;

                // Commenting the following statement will remove the 
                // initialization of number.
                //*p = 0xffff;

                // Print the value of *p:
                System.Console.WriteLine("Value at the location pointed to by p: {0:X}", *p);

                // Print the address stored in p:
                System.Console.WriteLine("The address stored in p: {0}", (int)p);
            }

        }
    }
}
