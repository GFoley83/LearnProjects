using System;
using System.Diagnostics;

namespace ctarti.Library
{
    [DebuggerDisplay("{Data}")]
    public class BinaryTreeNode
    {
        public int Data {get; set;}
        public BinaryTreeNode Left {get; set;}
	    public BinaryTreeNode Right {get; set;}
	    public BinaryTreeNode Parent {get; set;}
	    public int Size {get; set;}

	    public BinaryTreeNode(int d) 
        {
		    Data = d;
		    Size = 1;
	    }
	
	    public void SetLeftChild(BinaryTreeNode left) 
        {
		    this.Left = left;
		    if (left != null) {
			    left.Parent = this;
		    }
	    }
	
	    public void SetRightChild(BinaryTreeNode right) 
        {
		    this.Right = right;
		    if (right != null) {
			    right.Parent = this;
		    }
	    }
	
	    public void InsertInOrder(int d) 
        {
		    if (d <= Data) {
			    if (Left == null) {
				    SetLeftChild(new BinaryTreeNode(d));
			    } else {
				    Left.InsertInOrder(d);
			    }
		    } else {
			    if (Right == null) {
				    SetRightChild(new BinaryTreeNode(d));
			    } else {
				    Right.InsertInOrder(d);
			    }
		    }
		    Size++;
	    }
	
	    public bool IsBst() 
        {
		    if (Left != null) {
			    if (Data < Left.Data || !Left.IsBst()) {
				    return false;
			    }
		    }
		
		    if (Right != null) {
			    if (Data >= Right.Data || !Right.IsBst()) {
				    return false;
			    }
		    }		
		
		    return true;
	    }
	
	    public int Height() 
        {
		    int leftHeight = Left != null ? Left.Height() : 0;
		    int rightHeight = Right != null ? Right.Height() : 0;
		    return 1 + Math.Max(leftHeight, rightHeight);
	    }
	
	    public BinaryTreeNode Find(int d) 
        {
		    if (d == Data) {
			    return this;
		    } else if (d <= Data) {
			    return Left != null ? Left.Find(d) : null;
		    } else if (d > Data) {
			    return Right != null ? Right.Find(d) : null;
		    }
		    return null;
	    }
	
	    private static BinaryTreeNode CreateMinimalBst(int[] arr, int start, int end)
        {
		    if (end < start) {
			    return null;
		    }
		    int mid = (start + end) / 2;
		    BinaryTreeNode n = new BinaryTreeNode(arr[mid]);
		    n.SetLeftChild(CreateMinimalBst(arr, start, mid - 1));
		    n.SetRightChild(CreateMinimalBst(arr, mid + 1, end));
		    return n;
	    }
	
	    public static BinaryTreeNode CreateMinimalBst(int[] array) 
        {
		    return CreateMinimalBst(array, 0, array.Length - 1);
	    }
	
	    public void Print() {
            BTreePrinter.PrintNode(this);
	    }
    }
}
