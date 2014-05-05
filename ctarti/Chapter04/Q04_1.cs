using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ctarti.Library;
using ctarti.DataStructures;


namespace Chapter04
{
    public class Q04_1 : IQuestion
    {
        public void Run()
        {
            BinaryTreeCollection tree = new BinaryTreeCollection();
            tree.GenerateRandomCollection(6, 0, 100);
            BinaryTreePrinter.PrintNode(tree.Head);
            tree.GetHeight(tree.Head);
            tree.IsBalanced(tree.Head);
        }
    }
}
