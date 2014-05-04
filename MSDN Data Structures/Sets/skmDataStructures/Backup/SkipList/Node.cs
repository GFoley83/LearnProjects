using System;
using System.Collections;

namespace skmDataStructures.SkipList
{
	/// <summary>
	/// Summary description for a SkipList Node.
	/// </summary>
	public class Node
	{
		#region Private Member Variables
		private NodeList nodes;
		IComparable myValue;
		#endregion

		#region Constructors
		private Node() {}		// no default constructor supported, must supply height

		public Node(int height) : this(null, height) {}

		public Node(IComparable value, int height)
		{
			this.myValue = value;
            this.nodes = new NodeList(height);
		}
		#endregion

		public void IncrementHeight()
		{
			// increases the height by one
			nodes.IncrementHeight();
		}

		public void DecrementHeight()
		{
			// decreases the height by one
			nodes.DecrementHeight();
		}

		#region Public Properties
		public int Height
		{
			get { return nodes.Capacity; }
		}

		public IComparable Value
		{
			get { return myValue; }
		}

		public Node this[int index]
		{
			get { return nodes[index]; }
			set { nodes[index] = value; }
		}
		#endregion
	}
}