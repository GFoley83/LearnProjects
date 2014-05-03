using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02
{
    /*	2. Implement an algorithm to find the kth to last element of a singly linked list. 
     */
    public class Q02_2 : IQuestion
    {
        public void Run()
        {
            llNode head = new llNode(0);
            head.Next = new llNode(1);
            head.Next.Next = new llNode(2);
            head.Next.Next.Next = new llNode(3);
            head.Next.Next.Next.Next = new llNode(4);

            head.PrintList();
            int x= 3;
            Console.WriteLine("X={0}, XNode is {1}",x,  FindXFromTail(head, x).Data);

            head = new llNode(0);
            x = 1;
            Console.WriteLine("X={0}, XNode is {1}", x, FindXFromTail(head, x).Data);
        }

        public llNode FindXFromTail(llNode head, int x)
        {
            llNode currentNode = head;
            llNode xNode = head;
            
            for (int i = 0; i < x; i++)
            {
                if (currentNode == null)
                    throw new Exception("");
                else
                {
                    currentNode = currentNode.Next;
                }
            }

            while (currentNode != null)
            {
                currentNode = currentNode.Next;
                xNode = xNode.Next;
            }


            return xNode;
        }
    }
}
