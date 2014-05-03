using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctarti.DataStructures.Sorting
{
    public class BubbleSort : SortStrategy
    {
        public override void Sort(int[] array)
        {
            throw new NotImplementedException();
        }
    }

    public class InsertSort : SortStrategy
    {
        public override void Sort(int[] array)
        {
            throw new NotImplementedException();
        }
    }

    public class SelectSort : SortStrategy
    {
        public override void Sort(int[] array)
        {
            throw new NotImplementedException();
        }
    }

    public class MergeSort : SortStrategy
    {
        public override void Sort(int[] array)
        {
            throw new NotImplementedException();
        }
    }

    public class QuickSort : SortStrategy
    {
        public override void Sort(int[] array)
        {
            Quicksort(array, 0, array.Length);
        }

        public void Quicksort(int[] items, int left, int right)
        {
            int i = left, j = right;
            int pivot = items[(left + right) / 2];

            while (i <= j)
            {
                while (items[i].CompareTo(pivot) < 0)
                    i++;

                while (items[j].CompareTo(pivot) > 0)
                    j--;

                if (i <= j)
                {
                    // Swap
                    Swap(items, i, j);
                    i++;
                    j--;
                }
            }

            // Recursive Call Left
            if (left < j)
                Quicksort(items, left, j);

            //Recursive Call Right
            if (i < right)
                Quicksort(items, i, right);
        }


        private int[] Swap(int[] items, int index1, int index2)
        {

            int tmp = items[index1];
            items[index1] = items[index2];
            items[index2] = tmp;

            return items;
        }
    }

    public class RadixSort : SortStrategy
    {
        public override void Sort(int[] array)
        {
            throw new NotImplementedException();
        }
    }
}
