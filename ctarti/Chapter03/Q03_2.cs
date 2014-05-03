using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ctarti.DataStructures;

namespace Chapter03
{
    /*	2. How would you design a stack which, in addition to push and pop, 
     * also has a function min which returns the minimum element? Push, pop 
     * and min should all operate in O(1) time. */

    public class StackWithMin : StackCollection
    {
        Stack<int> minStack = new Stack<int>();

        public void Push(StackNode node)
        {
            if (base.Count == 0)
                //Empty Stack
                minStack.Push(node.Data);
            else if (node.Data <= minStack.Peek())
                //If Pushed Node Less Than Current Min
                minStack.Push(node.Data);

            base.Push(node);
        }

        public StackNode Pop()
        {
            if (minStack.Peek() == base.Peek().Data)
                minStack.Pop();

            return base.Pop();
        }

        public int Min()
        {
            if (minStack.Count > 0)
                return minStack.Peek();
            else
            {
                int newMin = int.MaxValue;
                StackNode currentNode = base.Bottom;

                while (currentNode != null)
                {
                    if (currentNode.Data < newMin)
                    {
                        minStack.Push(currentNode.Data);
                        newMin = currentNode.Data;
                    }
                    currentNode = currentNode.Next;
                }

                return minStack.Pop();                
            }
        }

    }

    public class Q03_2 : IQuestion
    {
        public void Run()
        {
            StackWithMin stack = new StackWithMin();
            stack.Push(new StackNode(8));
            stack.Push(new StackNode(5));
            stack.Push(new StackNode(6));
            stack.Push(new StackNode(3));
            stack.Push(new StackNode(7));
            stack.Bottom.Print();
            Console.WriteLine("Min={0}", stack.Min());

            stack.Pop();
            stack.Bottom.Print();
            Console.WriteLine("Min={0}", stack.Min());

            stack.Pop();
            stack.Bottom.Print();
            Console.WriteLine("Min={0}", stack.Min());

            stack.Pop();
            stack.Bottom.Print();
            Console.WriteLine("Min={0}", stack.Min());

            stack.Pop();
            stack.Bottom.Print();
            Console.WriteLine("Min={0}", stack.Min());
        }
    }
}
