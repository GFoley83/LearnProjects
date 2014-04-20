using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctarti.Library
{
    public class Node
    {
        public Node Next = null;
        public int Data;

        public Node(int d)
        {
            Data = d;
            
        }

        public virtual void AppendToTail(Node item)
        {
            Node end = item;
            Node n = this;
            while (n.Next != null)
                n = n.Next;
            n.Next = end;
        }

        public virtual Node DeleteNote(Node head, int d)
        {
            Node n = head;

            if (n.Data == d)
                return head.Next; /* Move Ahead */

            while (n.Next != null)
            {
                if (n.Next.Data == d)
                {
                    n.Next = n.Next.Next;
                    /* head didn't change */
                    return head; 
                }
                n = n.Next;
            }

            return head;
        }

        public override string ToString()
        {
            if (Next.Data == null)
                return string.Format("Node: Data={0}, Next=Null", Data);                
            else
                return string.Format("Node: Data={0}, Next={1}", Data, Next.Data);
        }
    }

    //public class Node2 : Node
    //{
    //    public Node Previous = null;

    //    public override void AppendToTail(Node2 item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Node DeleteNote(Node2 head, int d)
    //    {
    //        throw new NotImplementedException();
    //        return null;
    //    }

    //}

    public class Stack
    {
        Node top;

        void Push(Node item)
        {
            Node t = item;
            t.Next = top;
            top = t;
        }

        Node Pop()
        {
            if (top != null)
            {
                Node item = top;
                top = top.Next;
                return item;
            }
            return null;
        }

        Node Peak()
        {
            return top;
        }
    }

    public class Queue
    {
        Node first, last;

        public void Enqueue(Node item)
        {
            if (first == null)
            {
                //Create First Node
                last = item;
                first = last;
            }
            else
            {
                last.Next = item;
                last = last.Next;
            }
        }

        public object Dequeue()
        {
            if (first != null)
            {
                Node item = first;
                first = first.Next;
                if (first == null) last = null; //empty queue
                return item;
            }

            //No Nodes
            return null;
        }

        public object Peak()
        {
            return first.Data;
        }
    }
}
