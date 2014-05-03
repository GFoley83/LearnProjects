using ctarti.DataStructures;
using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03
{
    public class SetOfStack
    {
        public List<StackCollection> SetOfStacks = new List<StackCollection>();
        
        public SetOfStack()
        {
            StackCollection firstStack = new StackCollection();
            firstStack.Push(new StackNode(9));
            firstStack.Push(new StackNode(8));
            firstStack.Push(new StackNode(7));
            firstStack.Push(new StackNode(6));
            firstStack.Push(new StackNode(5));
            firstStack.Push(new StackNode(4));
            firstStack.Push(new StackNode(3));
            firstStack.Push(new StackNode(2));
            firstStack.Push(new StackNode(1));
            firstStack.Push(new StackNode(0));
            SetOfStacks.Add(firstStack);

            StackCollection secondStack = new StackCollection();
            SetOfStacks.Add(secondStack);

            StackCollection thirdStack = new StackCollection();
            SetOfStacks.Add(thirdStack);
        }

        public void Print()
        {

            SetOfStacks.ElementAt(0).Print();
            SetOfStacks.ElementAt(1).Print();
            SetOfStacks.ElementAt(2).Print();
        }

        public void Validate()
        {
            int belowValue;
            StackNode currentNode;

            //TEST Stack #0
            belowValue = int.MaxValue;
            currentNode = SetOfStacks.ElementAt(0).Bottom;
            while (currentNode != null)
            {
                if (currentNode.Data > belowValue)
                    throw new Exception(string.Format("Stack #0 Failed Validation: {0}>{1}", currentNode.Data, belowValue));

                belowValue = currentNode.Data;
                currentNode = currentNode.Next;
            }

            //TEST Stack #1
            belowValue = int.MaxValue;
            currentNode = SetOfStacks.ElementAt(1).Bottom;
            while (currentNode != null)
            {
                if (currentNode.Data > belowValue)
                    throw new Exception(string.Format("Stack #1 Failed Validation: {0}>{1}", currentNode.Data, belowValue));

                belowValue = currentNode.Data;
                currentNode = currentNode.Next;
            }

            //TEST Stack #2
            belowValue = int.MaxValue;
            currentNode = SetOfStacks.ElementAt(2).Bottom;
            while (currentNode != null)
            {
                if (currentNode.Data > belowValue)
                    throw new Exception(string.Format("Stack #2 Failed Validation: {0}>{1}", currentNode.Data, belowValue));

                belowValue = currentNode.Data;
                currentNode = currentNode.Next;
            }

        }

        public int Peak0()
        { return SetOfStacks.ElementAt(0).Peek().Data; }

        public int Peak1()
        { return SetOfStacks.ElementAt(0).Peek().Data; }

        public int Peak2()
        { return SetOfStacks.ElementAt(0).Peek().Data; }

        public void Move0To1()
        {
            SetOfStacks.ElementAt(1).Push(SetOfStacks.ElementAt(0).Pop());
            Validate();
        }

        public void Move0To2()
        {
            SetOfStacks.ElementAt(2).Push(SetOfStacks.ElementAt(0).Pop());
            Validate();
        }

        public void Move1To0()
        {
            SetOfStacks.ElementAt(0).Push(SetOfStacks.ElementAt(1).Pop());
            Validate();
        }

        public void Move1To2()
        {
            SetOfStacks.ElementAt(2).Push(SetOfStacks.ElementAt(1).Pop());
            Validate();
        }

        public void Move2To0()
        {
            SetOfStacks.ElementAt(0).Push(SetOfStacks.ElementAt(2).Pop());
            Validate();
        }

        public void Move2To1()
        {
            SetOfStacks.ElementAt(1).Push(SetOfStacks.ElementAt(2).Pop());
            Validate();
        }
    }

    /*	4. In the class problem of the Tower of Hanoi, you have 3 towers and N disk of different 
     * sizes which can slide onto any tower. The puzzle starts with disks sorted in ascending order
     * of size from top to bottom (i.e., each disk sits on top of an even larger one). You have the
     * following constraints:
		§ Only one disk can be moved at a time.
		§ A disk is slide off the top of one tower onto the next tower. 
		§ A disk can only be placed on top of a larger disk. 
		§ //Write a program to move the disks from the first tower to the last using stacks.
    */
    public class Q03_4 : IQuestion
    {
        public void Run()
        {
            SetOfStack set = new SetOfStack();
            set.Print();
            set.Move0To1();
            set.Print();
            set.Move0To1();

        }
    }
}
