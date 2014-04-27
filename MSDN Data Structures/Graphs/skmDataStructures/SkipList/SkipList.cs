using System;
using System.Collections;

namespace skmDataStructures.SkipList
{
	/// <summary>
	/// Summary description for SkipList.
	/// </summary>
	public class SkipList : IEnumerable
	{
		#region Private Member Variables
		Node head;
		protected const double PROB = 0.5;
		int comparisons;
		int count;
		Random rndNum;
		#endregion

		public SkipList() : this(-1) {}

		public SkipList(int randomSeed)
		{
			head = new Node(1);
			comparisons = count = 0;
			if (randomSeed < 0)
				rndNum = new Random();
			else
				rndNum = new Random(randomSeed);
		}

		protected virtual int chooseRandomHeight(int maxLevel)
		{
			int level = 1;

			while (rndNum.NextDouble() < PROB && level < maxLevel)
				level++;

			return level;
		}

		public virtual bool Contains(IComparable value)
		{
			Node current = head;
			int i = 0;

			// first, determine the nodes that need to be updated at each level
			for (i = head.Height - 1; i >= 0; i--)
			{
				while (current[i] != null)
				{
					comparisons++;
					int results = current[i].Value.CompareTo(value);
					if (results == 0)
						return true;
					else if (results < 0)
						current = current[i];
					else
						break;	// exit while loop
				}
			}

			// if we reach here, we searched to the end of the list without finding the element
			return false;
		}


		public virtual void Add(IComparable value)
		{
			Node [] updates = new Node[head.Height];
			Node current = head;
			int i = 0;

			// first, determine the nodes that need to be updated at each level
			for (i = head.Height - 1; i >= 0; i--)
			{

				if (!(current[i] != null && current[i].Value.CompareTo(value) < 0))
					comparisons++;

				while (current[i] != null && current[i].Value.CompareTo(value) < 0)
				{
					current = current[i];
					comparisons++;
				}

				updates[i] = current;
			}

			// see if a duplicate is being inserted
			if (current[0] != null && current[0].Value.CompareTo(value) == 0)
				// cannot enter a duplicate, handle this case by either just returning or by throwing an exception
				return;

            // create a new node
			Node n = new Node(value, chooseRandomHeight(head.Height + 1));
			count++;

			// if the node's level is greater than the head's level, increase the head's level
			if (n.Height > head.Height)
			{
				head.IncrementHeight();
				head[head.Height - 1] = n;
			}

			// splice the new node into the list
			for (i = 0; i < n.Height; i++)
			{
				if (i < updates.Length)
				{
					n[i] = updates[i][i];
					updates[i][i] = n;
				}
			}
		}

		public virtual void Remove(IComparable value)
		{
			Node [] updates = new Node[head.Height];
			Node current = head;
			int i = 0;

			// first, determine the nodes that need to be updated at each level
			for (i = head.Height - 1; i >= 0; i--)
			{
				if (!(current[i] != null && current[i].Value.CompareTo(value) < 0))
					comparisons++;

				while (current[i] != null && current[i].Value.CompareTo(value) < 0)
				{
					current = current[i];
					comparisons++;
				}

				updates[i] = current;
			}

			current = current[0];
			if (current != null && current.Value.CompareTo(value) == 0)
			{
				count--;

				// We found the data to delete
				for (i = 0; i < head.Height; i++)
				{
					if (updates[i][i] != current)
						break;
					else
						updates[i][i] = current[i];
				}

				// finally, see if we need to trim the height of the list
				if (head[head.Height - 1] == null)
				{
					// we removed the single, tallest item... reduce the list height
					head.DecrementHeight();
				}
			}
			else
			{
				// the data to delete wasn't found.  Either return or throw an exception
				return;
			}
		}


		public IEnumerator GetEnumerator()
		{
			return new SkipListEnumerator(this);
		}

		internal Node Head
		{
			get
			{
				return head;
			}
		}

		public virtual int Height
		{
			get { return head.Height; }
		}

		public virtual int Count
		{
			get { return count; }
		}

		public virtual int Comparisons
		{
			get { return comparisons; }
		}

		public void ResetComparisons()
		{
			comparisons = 0;
		}

	}
}
