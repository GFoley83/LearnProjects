using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctarti.Library
{
    public class Stack
    {
        Node top;

        object Pop()
        {
            if (top != null)
            {
                Object item = top.data;
                top = top.next;
                return item;
            }
            return null;
        }

        void Push(int item)
        {
            Node t = new Node(item);
            t.next = top;
            top = t;
        }

        Object Peak()
        {
            return top.data;
        }
    }
}
