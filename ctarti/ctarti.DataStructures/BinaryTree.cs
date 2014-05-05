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
        public BinaryTreeNode Parent;
        public int Data;

        public BinaryTreeNode(int data)
        {
            this.Data = data;
        }

        public bool IsLeaf()
        {
            if (Left == null && Right == null)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            string leftString;
            string rightString;

            if (Left == null)
                leftString = "_";
            else
                leftString = Left.Data.ToString();

            if (Right == null)
                rightString = "_";
            else
                rightString = Right.Data.ToString();

            return string.Format("{0} ({1}) {2} ", leftString, Data, rightString);
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
                    currentNode.Left.Parent = currentNode;
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
                    currentNode.Right.Parent = currentNode;
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

        public void Clear() { Head = null; }

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



        #region Traversal
        public void PreOrderTraversal(BinaryTreeNode node)
        {
            //Process Node
            Console.WriteLine("PreOrderTraversal: {0}", node);

            //Move Left
            if (node.Left != null)
                PreOrderTraversal(node.Left);

            //Move Right
            if (node.Right != null)
                PreOrderTraversal(node.Right);
        }

        public void InOrderTraversal(BinaryTreeNode node)
        {
            //Move Left
            if (node.Left != null)
                InOrderTraversal(node.Left);

            //Process Node
            Console.WriteLine("InOrderTraversal: {0}", node);
            
            //Move Right
            if (node.Right != null)
                InOrderTraversal(node.Right);
        }

        public void PostOrderTraversal(BinaryTreeNode node)
        {
            //Move Left
            if (node.Left != null)
                PostOrderTraversal(node.Left);

            //Move Right
            if (node.Right != null)
                PostOrderTraversal(node.Right);

            //Process Node
            Console.WriteLine("PostOrderTraversal: {0}", node);
            }
        #endregion

        #region Height and IsBalanced
        /// <summary>
        /// Node's Height is the max number of children on either left or right, plus one for node. 
        /// </summary>
        /// <param name="node">Usually start with head node, then recursively iterate.</param>
        /// <returns>Height of Node</returns>
        public int GetHeight(BinaryTreeNode node)
        {
            if (node == null) return 0; //Break Recursion

            int height = Math.Max(GetHeight(node.Left),    //left height
                                  GetHeight(node.Right))   //right height
                                  + 1;                     //plus one for node

            //if (node == Head) //print summary for head only
                Console.WriteLine("Node: {0} , Height: {1}", node, height);

            return height;
        }

        /// <summary>
        /// For each node, left's and right's height do not differ by more than one.
        /// </summary>
        /// <param name="node">Usually start with head node, then recursively iterate.</param>
        /// <returns>True for Balanced Trees</returns>
        public bool IsBalanced(BinaryTreeNode node)
        {
            if (node == null) return true; //Break Recursion

            int heightDifference = Math.Abs(GetHeight(node.Left) - GetHeight(node.Right));

            if (heightDifference > 1)
            {
                //Tree is Not Balanced
                Console.WriteLine("Tree is not balanced at {0}\n\n", node);
                return false;
            }
            else
            {
                //Tree is Balanced... So Far.
                bool leftIsBalanced = IsBalanced(node.Left);
                if (leftIsBalanced == false) return false;

                bool rightIsBalanced = IsBalanced(node.Right);
                if (rightIsBalanced == false) return false;

                if (node == Head)
                    Console.WriteLine("Tree is balanced\n\n");
                return true;
            }
        }
        #endregion

        #region Q4_3: BalancedInsert

        //int[] sortedArray0 = { 0 };
        //int[] sortedArray1 = { 0, 1, 2 };
        //int[] sortedArray2 = { 0, 1, 2, 4 };
        //int[] sortedArray3 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public BinaryTreeNode BalancedInsert(int[] sortedItems, int lower, int upper)
        {
            //Recursion Break Condition
            if (lower > upper)
                return null;

            //Calc Pivot Index
            int pivot = (lower + upper) / 2;
            
            //Create Pivot Node
            BinaryTreeNode node = new BinaryTreeNode(sortedItems[pivot]);
            
            //Go Left
            node.Left = BalancedInsert(sortedItems, lower, pivot - 1);

            //Go Right
            node.Right = BalancedInsert(sortedItems, pivot + 1, upper);

            //Return Pivot Node
            return node;

            //Below is ineficient b/c Add(v) function requires another traversal.
            //Add(new BinaryTreeNode(sortedItems[pivot])); 
        }

        #endregion

        #region Q4_4: PrintEachDeapthLeavl
        Dictionary<int, LinkedListCollection> DeapthLevels = new Dictionary<int, LinkedListCollection>();

        public void PrintEachLevelPOT()
        {
            AddLevelsPOT(Head, 0);
            foreach (KeyValuePair<int, LinkedListCollection> level in DeapthLevels)
            {
                Console.Write("Level {0}: ");
                level.Value.PrintCollection();
            }
        }
        //InderOrderTraversal
        /*   1
            / \
           /   \
          0     2
                 \
                  4 */
        private void AddLevelsPOT(BinaryTreeNode currentTreeNode, int level)
        {
            //Recursion Break Condition 
            if (currentTreeNode == null)
                return;

            Console.WriteLine("currentTreeNode={0}, level={1}", currentTreeNode.Data, level);

            //Create New Level, If Doesn't Exist
            if (DeapthLevels.ContainsKey(level) == false) DeapthLevels.Add(level, new LinkedListCollection());

            //Add Value to Linked List Node
            DeapthLevels[level].Add(new LinkedListNode(currentTreeNode.Data));

            //Go Left
            AddLevelsPOT(currentTreeNode.Left, level + 1);
            
            //Go Right
            AddLevelsPOT(currentTreeNode.Right, level + 1);
        }

        public void PrintEachLevelBFT()
        {
            Queue<KeyValuePair<int, BinaryTreeNode>> q = new Queue<KeyValuePair<int, BinaryTreeNode>>();
            q.Enqueue(new KeyValuePair<int, BinaryTreeNode>(0, Head));

            do
            {
                int level = q.Peek().Key;
                BinaryTreeNode currentTreeNode = q.Dequeue().Value;

                //Process
                Console.WriteLine("currentTreeNode={0}, level={1}", currentTreeNode.Data, level);

                //Enqueue Left
                if (currentTreeNode.Left != null) //Break Condition
                    q.Enqueue(new KeyValuePair<int, BinaryTreeNode>(level + 1, currentTreeNode.Left));
                
                //Enqueue Right
                if (currentTreeNode.Right != null) //Break Condition
                    q.Enqueue(new KeyValuePair<int, BinaryTreeNode>(level + 1, currentTreeNode.Right));
                
            } while (q.Count > 0);
        }
        #endregion

        #region Q4_5
        public bool IsBST(BinaryTreeNode currentNode, int minValueFound)
        {
            //Recursion Break Point: Reached Leaf
            if (currentNode == null) return true;

            //Go Left
            if (!IsBST(currentNode.Left, minValueFound))
                return false;
            
            //Process Node
            Console.WriteLine("Processing {0}, minValueFound {1}", currentNode.Data, minValueFound);
            if((currentNode.Data < minValueFound))
                return false;
            else if (currentNode.Right != null)
                if (currentNode.Data >= currentNode.Right.Data)
                    return false;
                else;
            else
                minValueFound = currentNode.Data;

            //Go Right
            if (!IsBST(currentNode.Right, minValueFound))
                return false;

            return true;
        }
        #endregion

        #region IDebugging
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
        #endregion

        #region Q4_6
        public BinaryTreeNode NextInOrderNode(BinaryTreeNode targetNode)
        {
            //State #1: targetNode has Right Child
            if (targetNode.Right != null)
                return targetNode.Right;

            //State #2: targetNode is Left Child
            if (targetNode == targetNode.Parent.Left)
                return targetNode.Parent;

            //State #3: targetNode is Right Child
            BinaryTreeNode parentNode = targetNode.Parent;
            while (parentNode.Parent != null)
            {
                //Action: Move Up Until Parent Is Left Child, Stop if Reach Root
                if (parentNode == parentNode.Parent.Left)
                    return parentNode.Parent;
                else
                    parentNode = parentNode.Parent;
            }

            //State #4: targetNode is far-most Right Leaf
            return new BinaryTreeNode(int.MaxValue);
        }
        #endregion

        #region Q4_7
        
        public FindCommonParentResults FindCommonParent(BinaryTreeNode currentNode, BinaryTreeNode nodeA, BinaryTreeNode nodeB, FindCommonParentResults results)
        {
            if (currentNode == null)
                return results;

            //Go Left
            if (currentNode.Left != null) FindCommonParent(currentNode.Left, nodeA, nodeB, results);

            //Go Right
            if (currentNode.Right != null) FindCommonParent(currentNode.Right, nodeA, nodeB, results);

            Console.WriteLine(currentNode.Data);

            //Process Node
            if (results.foundA == true && results.foundB == true)
            {
                Console.WriteLine("Common Parent is {0}, nodeA is {1}, nodeB is {2}", currentNode.Data, nodeA.Data, nodeB.Data);
                results.foundA = false;
                results.foundB = false;
            }

            //Mark If Left Found
            if (currentNode.Data == nodeA.Data)
                results.foundA = true;
            if (currentNode.Data == nodeB.Data)
                results.foundB = true;

            return results;
        }
        #endregion
    }

    public class FindCommonParentResults
    {
        public FindCommonParentResults()
        {
        }

        public bool foundA = false;
        public bool foundB = false;
    }
}
