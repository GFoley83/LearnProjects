using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnIEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            MyList<int> list = new MyList<int>();
            list.Add(3);
            list.Add(2);
            list.Add(1);

            foreach (int i in list)
            {
                Console.WriteLine("{0}", i);
            }

            Console.ReadKey();
        }
    }

    public class MyList<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private List<T> list = new List<T>();

        public void Add(T value)
        {
            list.Add(value);
        }

        public void Remove(T value)
        {
            list.Remove(value);
        }
    
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < list.Count<T>(); i++)
            {
                yield return list.ElementAt<T>(i);
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class MyListNode<TNode> : IComparable<TNode>
        where TNode : IComparable<TNode>
    {
        public TNode Data { get; private set; }

        //public MyListNode(TNode value)
        //{
        //    Data = value;
        //}

        public int CompareTo(TNode other)
        {
            return Data.CompareTo(other);
        }
    }
}
