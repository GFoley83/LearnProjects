using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctarti.Library
{
    public class MyBinarySearchTreeNode<TNode> : IComparable<TNode>
        where TNode : IComparable<TNode>
    {
        public MyBinarySearchTreeNode<TNode> Left;
        public MyBinarySearchTreeNode<TNode> Right;
        //public MyTreeNode<TNode> Parent;
        public TNode Value { get; private set; }

        public MyBinarySearchTreeNode(TNode value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Compares the current node to the provided value
        /// </summary>
        /// <param name="other">The node value to compare to</param>
        /// <returns>1 if the instance value is greater than the provided value, -1 if less or 0 if equal.</returns>
        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }
    }

    public class MyBinarySearchTree<T>: IEnumerable<T>
        where T: IComparable<T>
    {
        public MyBinarySearchTreeNode<T> Head;
        private int _count;


        #region Add and Remove Node Operations
        /// <summary>
        /// Adds the provided value to the binary tree.
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            if (Head == null)
                //Case #1 - Empty Tree
                Head = new MyBinarySearchTreeNode<T>(value);
            else
                //Case #2 - Find Insert Point, Start With Head
                AddTo(Head, value);
        }

        // Recursive add algorithm
        private void AddTo(MyBinarySearchTreeNode<T> node, T value)
        {
            //Case #1 - value <= node
            if (value.CompareTo(node.Value) <= 0)
	        {
                if (node.Left == null)
                    //Insert Left
                    node.Left = new MyBinarySearchTreeNode<T>(value);
                else
                    //Move Left
                    AddTo(node.Left, value);
	        }
            //Case #2 l- value > node
            else
            {
                if (node.Right == null)
                    //Insert Right
                    node.Right = new MyBinarySearchTreeNode<T>(value);
                else
                    AddTo(node.Right, value);
            }
        }

        public void RemoveSelfFromTree()
        {

        }
        #endregion

        #region BST Traversing Operations
        /// <summary>
        /// Process-->Left-->Right
        /// </summary>
        public void PreOrderTraversal()
        {
            if (Head == null)
                throw new Exception("Tree is empty.");
            else
                PreOrderTraversal(Head);
        }
        private void PreOrderTraversal(MyBinarySearchTreeNode<T> node)
        {
            //Process Node
            Console.WriteLine("PreOrderTraversal: {0}", node.Value);

            //Move Left
            if (node.Left != null)
                PreOrderTraversal(node.Left);

            //Move Right
            if (node.Right != null)
                PreOrderTraversal(node.Right);
        }

        /// <summary>
        /// Left-->Process-->Rgith
        /// </summary>
        public void InOrderTraversal()
        {
            if (Head == null)
                throw new Exception("Tree is empty.");
            else
                InOrderTraversal(Head);
        }
        private void InOrderTraversal(MyBinarySearchTreeNode<T> node)
        {
            //Move Left
            if (node.Left != null)
                InOrderTraversal(node.Left);

            //Process Node
            Console.WriteLine("InOrderTraversal: {0}", node.Value);

            //Move Right
            if (node.Right != null)
                InOrderTraversal(node.Right);
        }

        /// <summary>
        /// Left-->Right-->Process
        /// </summary>
        public void PostOrderTraversal()
        {
            if (Head == null)
                throw new Exception("Tree is empty.");
            else
                PostOrderTraversal(Head);
        }
        private void PostOrderTraversal(MyBinarySearchTreeNode<T> node)
        {
            //Move Left
            if (node.Left != null)
                PostOrderTraversal(node.Left);

            //Move Right
            if (node.Right != null)
                PostOrderTraversal(node.Right);

            //Process Node
            Console.WriteLine("PostOrderTraversal: {0}", node.Value);
        }

        /// <summary>
        /// This is a non-recursive algorithm using a stack to demonstrate 
        /// removing recursion to make using the yield syntax easier.
        /// </summary>
        /// <returns>Node's value using in in-order traversal</returns>
        public IEnumerator<T> InOrderTraversalEnumeration()
        {
            if (Head == null)
                throw new Exception("Tree is empty.");
            
            //Store the nodes we've skipped in this stack (avoids recursion)
            Stack<MyBinarySearchTreeNode<T>> stack = new Stack<MyBinarySearchTreeNode<T>>();

            MyBinarySearchTreeNode<T> currentNode = Head;

            //Skip Root Node
            stack.Push(currentNode);
            
            //
            bool goLeftNext = true;

            while (stack.Count<MyBinarySearchTreeNode<T>>() > 0)
            {
                if (goLeftNext)
                {
                    //Go To Far Left
                    while (currentNode.Left != null)
                    {
                        stack.Push(currentNode);
                        currentNode = currentNode.Left;
                    }
                }

                //Process Result
                yield return currentNode.Value;

                //Go Right If Can...
                if (currentNode.Right != null)
                {
                    //Move Right
                    currentNode = currentNode.Right;
                    goLeftNext = true;
                }
                else
                {
                    currentNode = stack.Pop();
                    goLeftNext = false;
                }

            }

        }
        #endregion

        #region Find Node Operations
        public MyBinarySearchTreeNode<T> FindRootNode()
        {
            //if (this.Parent == null)
            //    return this;
            //else
            //    return this.Parent.FindRootNode();
            return null;
        }
        public MyBinarySearchTreeNode<T> FindNodeValue(int value)
        {
            return null;
        }

        public bool IsBinarySearchTree()
        {

            //Store the nodes we've skipped in this stack (avoids recursion)
            Stack<MyBinarySearchTreeNode<T>> stack = new Stack<MyBinarySearchTreeNode<T>>();

            MyBinarySearchTreeNode<T> currentNode = Head;
            MyBinarySearchTreeNode<T> lastNode = Head;

            //Skip Root Node
            stack.Push(currentNode);

            //
            bool goLeftNext = true;

            while (stack.Count<MyBinarySearchTreeNode<T>>() > 0)
            {
                if (goLeftNext)
                {
                    //Go To Far Left
                    while (currentNode.Left != null)
                    {
                        stack.Push(currentNode);
                        currentNode = currentNode.Left;
                    }
                }

                //Process Result
                if ((currentNode.CompareTo(lastNode.Value) < 0) && (lastNode != Head))
                    return false;
                else
                    lastNode = currentNode;                    


                //Go Right If Can...
                if (currentNode.Right != null)
                {
                    //Move Right
                    currentNode = currentNode.Right;
                    goLeftNext = true;
                }
                else
                {
                    currentNode = stack.Pop();
                    goLeftNext = false;
                }

            }
            
            return true;
        }
        #endregion

        #region Tree Balancing Operations
        public bool IsBalancedBinarySearchTree()
        {
            return false;
        }

        public void BalanceTree()
        {

        }
        #endregion

        #region Print Tree Operations
        public void Print()
        {
            //BTreePrinter.PrintNode(this);
        }
        #endregion

        #region Enumeration
        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversalEnumeration();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
