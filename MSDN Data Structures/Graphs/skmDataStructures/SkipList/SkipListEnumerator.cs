using System;
using System.Collections;

namespace skmDataStructures.SkipList
{
	/// <summary>
	/// Summary description for SkipListEnumerator.
	/// </summary>
	public class SkipListEnumerator : IEnumerator, IDisposable
	{
		SkipList sl;
		Node currentNode;

		public SkipListEnumerator(SkipList skipList)
		{
			this.sl = skipList;
			currentNode = sl.Head;
		}

		public void Reset()
		{
			currentNode = null;
		}

		public bool MoveNext()
		{
			currentNode = currentNode[0];
			return currentNode != null;
		}

		public Node Current
		{
			get
			{
				return currentNode;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}

		public void Dispose()
		{
			sl = null;
		}
	}
}
