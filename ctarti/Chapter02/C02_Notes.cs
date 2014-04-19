using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02
{
    public class Node
    {
        Node next = null;
        int data;

        public Node(int d)
        {
            data = d;
        }

        void appendToTail(int d)
        {
            Node end = new Node(d);
            Node n = this;
            while (n.next != null)
                n = n.next;
            n.next = end;
        }

        Node deleteNote(Node head, int d)
        {
            Node n = head;

            if (n.data == d)
                return head.next; /* Move Ahead */

            while (n.next != null)
            {
                if (n.next.data == d)
                {
                    n.next = n.next.next;
                    return head; /* head didn't change */
                }
                n = n.next;
            }

            return head;
        }
    }

    public class C02_Notes : IQuestion
    {

        public void Run()
        {
            //Creating Linked List

            //Deleting a Node From a Singlely Linked List

            //The "Runner" Technique
            /* Current State: a1->a2->an->b1->b2->bn
             * Desired State: a1->b1->a2->b2->an->bn
             * Assumption: Linked list has even number of nodes
             * 
             * Solution: create two points: p1 (fast pointer) and p2 (slow pointer)
             *      p1 moves every two nodes
             *      p2 moves every one node
             *      
             *      when p1 reaches end (bn), p2 will be in middle (an)
             *      move p1 back to a1
             *      weave p1 (a1), p2 (b1), ...
             */
        
            //Recursive Problems - entire chatper.
        }
    }
}
