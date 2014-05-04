using System;

namespace ctarti.DataStructures.Sorting
{
    public class BubbleSort : SortStrategy
    {
        public override void Sort(int[] items)
        {
            Console.WriteLine("\n\nBuble Sort"); ResetCounters();

            bool swapped;

            do
            {
                swapped = false;

                for (int i = 1; i < items.Length; i++)
                {
                    CompCount++;
                    if (items[i-1] > items[i])
                    {
                        Swap(items, i-1, i);
                        swapped = true;
                    }
                }
            } while (swapped != false);
        }
    }

    public class SelectSort : SortStrategy
    {
        public override void Sort(int[] items)
        {
            Console.WriteLine("\n\nSelect Sort"); ResetCounters();

            //move to right, reduce right range by 1, repeat
            for (int i = items.Length - 1; i > 0 ; i--)
            {
                int maxIndex = 0;
                int j = 0;
                while (j <= i)
                {
                    CompCount++;

                    //Find Max Element in Range(0 to i)
                    if (items[maxIndex] < items[j])
                        maxIndex = j;

                    //Next
                    j++;
                }

                Swap(items, maxIndex, i);
            }
        }
    }

    public class InsertSort : SortStrategy
    {
        public override void Sort(int[] items)
        {
            //used to Insert an element into already sorted array
            throw new NotImplementedException();
            //items = {0 1 2 4 5 6 7}
            //insertValue = 3
            //insertIndex = 3
            //shift {4 5 6 7} to right
        }
    }

    public class MergeSort : SortStrategy
    {
        public override void Sort(int[] items)
        {
            Console.WriteLine("\n\nMerge Sort"); ResetCounters();
            MergeSortInner(items);
        }

        //Recursive Sort 
        private void MergeSortInner(int[] items)
        {
            //Recursive Break Condition
            if (items.Length == 1)
                return;
                
            //Create left & right. Handel even/odd Items.Length
            int[] left = new int[items.Length / 2];
            int[] right = new int[items.Length - left.Length];

            //Copy Array
            Array.Copy(items, 0, left, 0, left.Length);
            Array.Copy(items, left.Length, right, 0, right.Length);

            //Recursively Divide
            MergeSortInner(left);
            MergeSortInner(right);
            
            //Merge and Sort
            Merge(items, left, right);
        }

        //Merge and Sort
        private void Merge(int[] items, int[] left, int[] right)
        {
            int targetIndex = 0; //for items
            int leftIndex = 0;
            int rightIndex = 0;

            int remaining = left.Length + right.Length;

            while (remaining > 0)
            {
                //left is unloaded
                if (leftIndex >= left.Length)
                {
                    //unload right...
                    items[targetIndex] = right[rightIndex++]; 
                    SwapCount++; CompCount++;
                }
                //right is unloaded
                else if (rightIndex >= right.Length)
                {
                    //unload left...
                    items[targetIndex] = left[leftIndex++];
                    SwapCount++; CompCount += 2;
                }
                //left < right
                else if (left[leftIndex] < right[rightIndex])
                {
                    //insert left...
                    items[targetIndex] = left[leftIndex++];
                    SwapCount++; CompCount += 3;
                }
                //right <= left
                else
                {
                    //insert right...
                    items[targetIndex] = right[rightIndex++];
                    SwapCount++; CompCount += 3;
                }

                targetIndex++;
                remaining--;
            }
        }
    }

    public class QuickSort : SortStrategy
    {
        public override void Sort(int[] array)
        {
            Console.WriteLine("\n\nQuick Sort"); ResetCounters();

            QuicksortInner(array, 0, array.Length -1);
        }

        public void QuicksortInner(int[] items, int left, int right)
        {
            int i = left, j = right;
            int pivot = items[(left + right) / 2]; //Choose New Pivot Point

            while (i <= j)
            {
                while (items[i] < pivot)    //move left-to-right until value > pivot
                { i++; CompCount++; }

                while (pivot < items[j])    //move right-to-left until value < pivot
                { j--; CompCount++; }

                CompCount++; 
                if (i <= j)
                {
                    //swap
                    Swap(items, i, j);
                    i++;
                    j--;
                }
            }

            //recursive Calls Left and Right
            if (left < j)   //left recursive break condition
                QuicksortInner(items, left, j);
            if (i < right)  //right recursive break condition
                QuicksortInner(items, i, right);
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