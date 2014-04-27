using System;
using System.Collections;


namespace skmDataStructures.SkipList
{
	/// <summary>
	/// Summary description for NodeCollection.
	/// </summary>
	public class NodeList : CollectionBase
	{
		public NodeList(int height)
		{
			// set the capacity based on the height
			base.InnerList.Capacity = height;

			// create dummy values up to the Capacity
			for (int i = 0; i < height; i++)
				base.InnerList.Add(null);
		}

		public void Add(Node n)
		{
			base.InnerList.Add(n);
		}

		public Node this[int index]
		{
			get { return (Node) base.InnerList[index]; }
			set { base.InnerList[index] = value; }
		}

		public void IncrementHeight()
		{
			base.InnerList.Capacity++;

			// add a dummy entry
			base.InnerList.Add(null);
		}

		public void DecrementHeight()
		{
			// delete the last entry
			base.InnerList.RemoveAt(base.InnerList.Count - 1);
			base.InnerList.Capacity--;
		}

		public int Capacity
		{
			get { return base.InnerList.Capacity; }
		}
	}
}
