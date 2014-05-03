using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctarti.DataStructures
{
    public class StackNode
    {
        public StackNode Next;
        public int Data;
        public StackNode(int data)
        { this.Data = data; }

        public void Print()
        {
            if (Next == null)
                Console.Write("{0}\n", Data);
            else
            {
                Console.Write("{0}-->", Data);
                Next.Print();
            }
        }
    }

    public class StackCollection
    {
        public StackNode Top;
        public StackNode Bottom;
        public int Count = 0;

        public void Push(StackNode node)
        {
            if (Top == null)
            {
                //Empty Stack
                Bottom = node;
                Bottom.Next = Top;
                Top = node;
                Count++;
            }
            else
            {
                //Push Top of Stack
                Top.Next = node;
                Top = node;
                Count++;
            }
        }

        public StackNode Pop()
        {
            if (Count == 0)
                throw new Exception("Stack is Empty");
            else if (Count == 1)
            {
                //One Element Remaining
                StackNode node = Top;
                Count--;
                Top = null;
                Bottom = null; ;
                return node;
            }
            //Pop Top of Stack
            else
            {
                //Find Node->Top
                StackNode oldTop = Top;
                StackNode newTop = Bottom;
                while (newTop.Next != oldTop)
                    newTop = newTop.Next;

                Top = newTop;
                Top.Next = null;
                Count--;
                return oldTop;
            }
        }

        public StackNode Peek()
        {
            if (Count == 0)
                throw new Exception("Stack is Empty");
            else
                return Top;
        }

        public void Print()
        {
            if (Bottom == null)
                Console.WriteLine("Stack is Empty.");
            else
                Bottom.Print();
        }
    }
}
