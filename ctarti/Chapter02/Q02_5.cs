using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02
{
    /* 	5. You have two numbers represented by a linked list, 
     * where each node contains a single digit. The digits are stored in reverse order,
     * such that the 1s digit is at the head of the list. Write a function that adds the 
     * two numbers and returns the sum as a linked list. 
     */

    public class llNode
    {
        public llNode Next;
        public int Data;
        public llNode(int data)
        { this.Data = data; }
        public void PrintList()
        {
            if (Next == null)
                Console.Write("{0}\n", Data);
            else
            {
                Console.Write("{0}-->", Data);
                Next.PrintList();
            }
        }

        public void Append(llNode node)
        {
            llNode currentNode = this;

            while (currentNode.Next != null)
                currentNode = currentNode.Next;

            currentNode.Next = node;
        }

    }

    public class Q02_5 : IQuestion
    {
        public void Run()
        {
            //TEST #1 and #2: Setup SameSize Lists
            llNode sameSizeLeft = new llNode(0);
            sameSizeLeft.Next = new llNode(1);
            sameSizeLeft.Next.Next = new llNode(2);
            sameSizeLeft.Next.Next.Next = new llNode(3);
            sameSizeLeft.Next.Next.Next.Next = new llNode(4);

            llNode sameSizeRight = new llNode(5);
            sameSizeRight.Next = new llNode(6);
            sameSizeRight.Next.Next = new llNode(7);
            sameSizeRight.Next.Next.Next = new llNode(8);
            sameSizeRight.Next.Next.Next.Next = new llNode(9);

            //Test SameSize on Reverse Order Method
            sameSizeLeft.PrintList();
            sameSizeRight.PrintList();
            Console.WriteLine("+ ===============");
            llNode sameSizeSum = AddListsDigitsReverseOrder(sameSizeLeft, sameSizeRight);
            sameSizeSum.PrintList();
            Console.WriteLine("\n");

            //Test SameSize on Forward Order Method
            sameSizeLeft.PrintList();
            sameSizeRight.PrintList();
            Console.WriteLine("+ ===============");
            sameSizeSum = AddListsDigitsForwardOrder(sameSizeLeft, sameSizeRight);
            sameSizeSum.PrintList();
            Console.WriteLine("\n");


            //TEST #3 and #4: Setup DiffSize Lists
            llNode diffSizeLeft = new llNode(0);
            diffSizeLeft.Next = new llNode(1);

            llNode diffSizeRight = new llNode(5);
            diffSizeRight.Next = new llNode(6);
            diffSizeRight.Next.Next = new llNode(7);
            diffSizeRight.Next.Next.Next = new llNode(8);
            diffSizeRight.Next.Next.Next.Next = new llNode(9);

            //Test DiffSize on Reverse Order Method
            diffSizeLeft.PrintList();
            diffSizeRight.PrintList();
            Console.WriteLine("+ ===============");
            llNode diffSizeSum = AddListsDigitsReverseOrder(diffSizeLeft, diffSizeRight);
            diffSizeSum.PrintList();
            Console.WriteLine("\n");


            //Test DiffSize on Forward Order Method
            diffSizeLeft.PrintList();
            diffSizeRight.PrintList();
            Console.WriteLine("+ ===============");
            diffSizeSum = AddListsDigitsForwardOrder(diffSizeLeft, diffSizeRight);
            diffSizeSum.PrintList();
            Console.WriteLine("\n");

        }

        // Reverse Order: 1-->2-->3 = 321
        private llNode AddListsDigitsReverseOrder(llNode left, llNode right )
        {
            //Initialize Variables
            llNode headNode = null;
            llNode sumNode = null;
            int sumOverflow = 0;

            //Step #1: Iterate While Left and Right Has Nodes
            while (left != null && right != null)
            {
                int sum = left.Data + right.Data + sumOverflow;

                //Check for Overflow
                if (sum > 9)
                {
                    sum -= 10;
                    sumOverflow = 1;
                }
                else
                    sumOverflow = 0;

                //Create Sum Node
                if (headNode == null)
                {
                    //Create Head Node
                    sumNode = new llNode(sum);
                    headNode = sumNode;
                }
                else
                {
                    //Next Node
                    sumNode.Next = new llNode(sum);
                    sumNode = sumNode.Next;
                }

                //Move Forward
                //sumNode = sumNode.Next;
                left = left.Next;
                right = right.Next;
            }

            //Step #2: Insert Any Remaining Nodes
            if (left != null)
            {
                Console.WriteLine("Left is longer than Right.");
                while (left != null)
                {
                    sumNode = new llNode(left.Data);

                    //Move Forward
                    sumNode = sumNode.Next;
                    left = left.Next;
                }
            }
            else if (right != null)
            {
                Console.WriteLine("Right is longer than Left.");

                while (right != null)
                {
                    sumNode.Next = new llNode(right.Data);

                    //Move Foward
                    sumNode = sumNode.Next;
                    right = right.Next;
                }
            }
            else
            {
                Console.WriteLine("Left and Right are same size lists.");
            }

            return headNode;
        }

        // Forward ORder: 1-->2-->3 = 123
        private llNode AddListsDigitsForwardOrder(llNode left, llNode right)
        {
            //Reverse Left
            llNode newLeft = ReverseList(left);

            //Reverse Right
            llNode newRight = ReverseList(right);

            llNode sum = AddListsDigitsReverseOrder(newLeft, newRight);
            return sum;
        }

        private llNode ReverseList(llNode currentNode)
        {
            Stack<llNode> llNodes = new Stack<llNode>();

            while (currentNode != null)
            {
                llNodes.Push(currentNode);
                currentNode = currentNode.Next;
            }

            llNode newHead = llNodes.Pop();
            llNode newCurrentNode = newHead;

            while (llNodes.Count > 0)
            {
                newCurrentNode.Next = llNodes.Pop();
                newCurrentNode = newCurrentNode.Next;
            }

            //For the Last Node, Remove the Pointer
            newCurrentNode.Next = null;

            return newHead;
        }
    }
}
