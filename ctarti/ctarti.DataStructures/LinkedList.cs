using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ctarti.DataStructures
{
    //Next
    //Previous
    //Data

    //Create
    //SetNext
    //SetPrevious

    //PrintForward
    //Clone
    [DebuggerDisplay("Data = {Data}")] 
    public class LinkedListNode : IComparable
    {
        public LinkedListNode Next { get; set; }
        public int Data;

        public LinkedListNode() { }
        public LinkedListNode(int data) {this.Data = data;}


        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }

    public class LinkedListCollection: IEnumerable<LinkedListNode>
    {
        public LinkedListNode Head { get; set; }

        public LinkedListCollection() { }

        public void Add(LinkedListNode node)
        {
            if (node == null)
                throw new Exception("Node is null");

            FindTail().Next = node;
        }
        public void Remove(LinkedListNode node)
        {
            if (node == null)
                throw new Exception("Node is null");

            FindPreviousNode(node).Next = node.Next;
        }

        public LinkedListNode FindPreviousNode(LinkedListNode node)
        {
            LinkedListNode currentNode = Head;

            while (currentNode.Next != null)
            {
                if (currentNode.Next == node)
                    return currentNode;

                currentNode = currentNode.Next;
            }

            throw new Exception("Node not found");

            return null;
        }
        public LinkedListNode FindTail()
        {
            LinkedListNode currentNode = Head;

            while (currentNode.Next != null)
            {
                currentNode = currentNode.Next;
            }

            return currentNode;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (LinkedListNode current in this)
            {
                sb.AppendFormat("-->{0}", current.ToString());
            }

            return sb.ToString();
        }
        public IEnumerator<LinkedListNode> GetEnumerator()
        {
            LinkedListNode current = Head;

            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

