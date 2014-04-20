using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctarti.Library
{
    public class MyTreeNodeint
    {
        public MyTreeNodeint Left;
        public MyTreeNodeint Right;
        public MyTreeNodeint Parent;
        public TNode Data { get; private set; }

        public MyTreeNode(MyTreeNodeint parent, TNode value)
        {
            this.Parent = parent;
            this.Value = value;
        }
    }

    public class MyBinaryTree<T>: IEnumerable<T>
        where T: IComparable<T>
    {
        private MyTreeNode<T> Head;
        private int _count;

        #region Node Insert and Remove Operations
        public void Add(T value)
        {
            // Case 1: The tree is empty - allocate the head
            if (Head == null)
                new MyTreeNode<T>(null, value);
            // Case 2: The tree is not empty so find the right location to insert
            else
                AddTo(Head, value);
        }

        public void AddTo(MyTreeNode<T> node, T value)
        {
            //Case 1: The value is less than or equal to node
            if (value.CompareTo(node.Data) <= 0)
            {

            }
            else
            //Case 2: The value is greater than the node
            {

            }
        }

        public void RemoveSelfFromTree()
        {

        }
        #endregion

        #region BST Traversing Operations
        public MyTreeNode<T> NextNodeInOrder()
        {
            return null;
        }

        public MyTreeNode<T> NextNodePostOrder()
        {
            return null;
        }

        public MyTreeNode<T> NextNodePreOrder()
        {
            return null;
        }
        #endregion

        #region BFT and DFT Operations
        public MyTreeNode<T> NextNodeBreadthFirst()
        {
            return null;
        }

        public MyTreeNode<T> NextNodeDepthFirst()
        {
            return null;
        }
        #endregion

        #region Find Node Operations
        public MyTreeNode<T> FindRootNode()
        {
            //if (this.Parent == null)
            //    return this;
            //else
            //    return this.Parent.FindRootNode();
            return null;
        }
        public MyTreeNode<T> FindNodeValue(int value)
        {
            return null;
        }

        public bool IsBinarySearchTree()
        {
            return false;
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
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
