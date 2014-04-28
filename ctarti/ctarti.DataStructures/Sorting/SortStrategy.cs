using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctarti.DataStructures.Sorting
{
    public abstract class SortStrategy
    {
        public int CompCount { get; protected set; }        
        public int SwapCount { get; protected set; }
        public void ResetCounters() { CompCount = 0; SwapCount = 0; }

        public abstract void Sort(int[] array);

        public void Swap(int[] items, int index1, int index2)
        {
            SwapCount++;

            StringBuilder before = new StringBuilder();
            foreach (int i in items)
                before.AppendFormat("{0},", i);

            //string before ;
            int value1 = items[index1];
            int value2 = items[index2];

            if (index1 != index2)
            {
                int tmp = items[index1];
                items[index1] = items[index2];
                items[index2] = tmp;
            }

            StringBuilder after = new StringBuilder();
            foreach (int i in items)
                after.AppendFormat("{0},", i);

            Console.WriteLine("{0} ==> Swap({1},{2}) ==> {3} | CompCount={4}, SwapCount is {5}", before, value1, value2, after, CompCount, SwapCount);
        }
        public void Reverse(int[] items)
        {
            int startIndex = 0;
            int endIndex = items.Length - 1;
            
            while (startIndex < endIndex) ;
                Swap(items, startIndex++, endIndex--);
        }
    }
}
