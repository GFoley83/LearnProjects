using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02
{
    /*	3. Implement an algorithm to delete a node in the middle of a singly linked list, given only access to that node. Example. 
		§ Input: the node c from the linked list a->b->c->d->e
		§ Result: nothing is returned, but the new linked list looks like a->b->d->e
    */
    public class llNode2_3
    {
        public llNode2_3 Next;
        public int Data;
        public llNode2_3(int data)
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
    }

    public class Q02_3 : IQuestion
    {

        public void Run()
        {
            llNode2_3 head = new llNode2_3(0);
            head.Next = new llNode2_3(1);
            head.Next.Next = new llNode2_3(2);
            head.Next.Next.Next = new llNode2_3(3);
            head.Next.Next.Next.Next = new llNode2_3(4);

            head.PrintList();
            RemoveNode(head.Next.Next);
            head.PrintList();
        }

        public void RemoveNode(llNode2_3 node)
        {
            //Sceanrio #1 - Node.Next is Null
            if (node.Next == null)
                node = null;
            //Scearnio #2 - Node.Next.Next is Null
            else if (node.Next.Next == null)
            {
                node.Data = node.Next.Data;
                node.Next = null;
            }
            else
            //Scenario #3 - Node.Next.Next is Not Null
            {
                node.Data = node.Next.Data;
                node.Next = node.Next.Next;
            }
        }
    }
}
