using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctarti.DataStructures
{
    public class BinaryTreeNode
    {
        public BinaryTreeNode Left;
        public BinaryTreeNode Right;
        public int Data;

        public BinaryTreeNode(int data)
        {
            this.Data = data;
        }

        public override string ToString()
        {
            string leftString;
            string rightString;

            if (Left == null)
                leftString = "X";
            else
                leftString = Left.Data.ToString();

            if (Right == null)
                rightString = "X";
            else
                rightString = Right.Data.ToString();

            return string.Format("{0}/({1})\\{2} ", leftString, Data, rightString);
        }

        //In-Order Traversal
        public void PrintNode()
        {
            if (Left != null)
                Left.PrintNode();

            Console.WriteLine(ToString());

            if (Right != null)
                Right.PrintNode();
        }
    }

    public class BinaryTreeCollection: IDebugging
    {
        public BinaryTreeNode Head;

        /// <summary>
        /// Add Node.
        /// *Outcome #1: Head is Empty, Insert at Head.
        /// *Outcome #2: Head is Not Emplty. Starting at the Heam, Add New Node recursively.
        /// </summary>
        /// <param name="newNode"></param>
        public void Add(BinaryTreeNode newNode)
        {
            //Test Case: Head is Null
            if (Head == null)
                Head = newNode;
            else
                Add(Head, newNode);
        }
        
        /// <summary>
        /// Add Node Recursively.
        /// *Outcome #1: Insert Left
        /// *Outcome #2: Go Left
        /// *Outcome #3: Insert Right
        /// *Outcome #4: Go Right
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="newNode"></param>
        private void Add(BinaryTreeNode currentNode, BinaryTreeNode newNode)
        {
            //Test Case - newNode < currentNode
            if (newNode.Data < currentNode.Data)
            {
                //Test Case - currentNode.Left is null
                if (currentNode.Left == null)
                {
                    //Outcome #1: Insert Left.
                    currentNode.Left = newNode;
                }
                else
                {
                    //Outcome #2: Go Left.
                    Add(currentNode.Left, newNode);
                }
            }
            //Test Case - nodeNode >= currentNode
            else
            {
                //Test Case - currentNode.Right is null
                if (currentNode.Right == null)
                {
                    //Outcome #1: Insert Right.
                    currentNode.Right = newNode;
                }
                else
                {
                    //Outcome #2: Go Right.
                    Add(currentNode.Right, newNode);
                }
            }
        }

        //Remove Node
        public void Remove(BinaryTreeNode node)
        {
            //Step #1: Find Node
            BinaryTreeNode targetNode = SearchForNode(node);

            //Step #2: Find Parent
            BinaryTreeNode parentNode = SearchForParent(targetNode);

            //Step #3: Target Is Left or Right?
            bool IsTargetALeftChild = false;
            if (parentNode.Left == null)
                IsTargetALeftChild = false;
            else if (parentNode.Left.Data == targetNode.Data)
                IsTargetALeftChild = true;

            //Step #4: Determine Scenario
            //Scenario #1: Leaf Node
            if (targetNode.Left == null && targetNode.Right == null)
            {
                if (IsTargetALeftChild)
                    //Remove Left
                    parentNode.Left = null;
                else
                    //Remmove Right
                    parentNode.Right = null;
            }
            //Scenario #2: Target Has No Right Child
            else if (targetNode.Right == null)
            {
                if (IsTargetALeftChild)
                    parentNode.Left = targetNode.Left;
                else
                    parentNode.Right = targetNode.Left;
            }
            //Scenario #3: Target Has No Left Child
            else if (targetNode.Left == null)
            {
                if (IsTargetALeftChild)
                    parentNode.Left = targetNode.Right;
                else
                    parentNode.Right = targetNode.Right;
            }
            //Scenario #4: Target Has Two Children
            else
            {
                //Find targetNode.Right's Left Most Node
                BinaryTreeNode leftMostNode = targetNode.Right;
                while (leftMostNode.Left != null)
                    leftMostNode = leftMostNode.Left;

                if (IsTargetALeftChild)
                    parentNode.Left = leftMostNode;
                else
                    parentNode.Right = leftMostNode;
            }
        }

        /// <summary>
        /// Search for Node. 
        /// Throw Exception if head is empty.
        /// Throw Exception if node not found.
        /// </summary>
        /// <param name="findNode"></param>
        /// <returns>Found Node</returns>
        public BinaryTreeNode SearchForNode(BinaryTreeNode findNode)
        {
            if (Head == null)
                throw new Exception("Empty Tree");
            else
                return SearchForNode(Head, findNode);
        }
        private BinaryTreeNode SearchForNode(BinaryTreeNode currentNode, BinaryTreeNode findNode)
        {
            //Break Condition
            if (findNode.Data == currentNode.Data)
                return currentNode;
            else if (findNode.Data < currentNode.Data)
                if (currentNode.Left == null)
                    //Node Not Found
                    throw new Exception("Node Not Found");
                else
                    //Go Left
                    return SearchForNode(currentNode.Left, findNode);
            else
                if (currentNode.Right == null)
                    //Node Node Found
                    throw new Exception("Node Node Found");
                else
                    //Go Right
                    return SearchForNode(currentNode.Right, findNode);
        }

        /// <summary>
        /// Required for Removal.
        /// Not efficient as requires to searches for Remove Node.
        /// </summary>
        /// <param name="findNode"></param>
        /// <returns></returns>
        public BinaryTreeNode SearchForParent(BinaryTreeNode findNode)
        {
            if (Head == null)
                throw new Exception("Empty Tree");
            else
                return SearchForNode(Head, findNode);
        }
        private BinaryTreeNode SearchForParent(BinaryTreeNode currentNode, BinaryTreeNode findNode)
        {
            //Break Condition
            if ((findNode.Data == currentNode.Left.Data) || (findNode.Data == currentNode.Right.Data))
                //Found Parent
                return currentNode;
            else if (findNode.Data < currentNode.Data)
                if (currentNode.Left == null)
                    //Node Not Found
                    throw new Exception("Node Not Found");
                else
                    //Go Left
                    return SearchForNode(currentNode.Left, findNode);
            else
                if (currentNode.Right == null)
                    //Node Node Found
                    throw new Exception("Node Node Found");
                else
                    //Go Right
                    return SearchForNode(currentNode.Right, findNode);
        }



        public void PrintCollection()
        {
            //In-Order Traveral
            Head.PrintNode();
        }

        public void GenerateRandomCollection(int size, int minValue, int maxValue)
        {
            Random r = new Random();
            for (int i = 0; i < size; i++)
            {
                Add(new BinaryTreeNode(r.Next(minValue, maxValue)));
            }
        }
    }
}
