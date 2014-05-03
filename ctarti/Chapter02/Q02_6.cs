using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02
{
    /*	6. Given a circular linked list, implement an algorithm which returns 
     * the node at the beginning of the loop.
     */

    public class Q02_6 : IQuestion
    {
        public void Run()
        {
            //0-->1-->2-->3-->4-->2
            //2-->4-->3-->2-->4
            llNode cl = new llNode(0);
            cl.Next = new llNode(1);
            cl.Next.Next = new llNode(2);
            cl.Next.Next.Next = new llNode(3);
            cl.Next.Next.Next.Next = new llNode(4);
            cl.Next.Next.Next.Next.Next = new llNode(5);
            cl.Next.Next.Next.Next.Next.Next = new llNode(6);
            cl.Next.Next.Next.Next.Next.Next.Next = new llNode(7);
            cl.Next.Next.Next.Next.Next.Next.Next.Next = cl.Next.Next;

            //cl.PrintList();
            Console.WriteLine("Corrupt Node = {0}", FindCorruptNode(cl).Data);
        }

        public llNode FindCorruptNode(llNode head)
        {
            llNode slow = head;
            llNode fast = head;

            while (true)
            {
                //Move Forward
                slow = slow.Next;
                fast = fast.Next.Next;

                //Checking for Collision
                if (slow.Data == fast.Data)
                {
                    Console.WriteLine("Collision at {0}", slow.Data);
                    llNode fromHead = head;
                    llNode fromCollision = slow;

                    //collison node = loop size + k; where k = steps from head to loop start.
                    while (fromHead.Data != fromCollision.Data)
                    {
                        //Move Forward
                        fromHead = fromHead.Next;
                        fromCollision = fromCollision.Next;
                    }

                    Console.WriteLine("Start of loop at {0}", fromHead.Data);
                    return fromHead;
                }
            }

            return null;
        }
    }
}
