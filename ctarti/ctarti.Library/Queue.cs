using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctarti.Library
{
    public class Queue
    {
        Node first, last;

        public void Enqueue(int item)
        {
            if (first == null)
            {
                //Create First Node
                last = new Node(item);
                first = last;
            }
            else
            {
                last.next = new Node(item);
                last = last.next;
            }
        }

        public Object Dequeue()
        {
            if (first != null)
            {
                Object item = first.data;
                first = first.next;
                if (first == null) last = null; //empty queue
                return item;
            }

            //No Nodes
            return null;
        }

        public Object Peak()
        {
            return first;
        }

    }
}
