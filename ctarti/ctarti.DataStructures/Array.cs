using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ctarti.DataStructures.Sorting;
namespace ctarti.DataStructures
{
    public class ArrayCollection : IDebugging, ISorting
    {
        public int[] Items { get; set; }

        public ArrayCollection()
        {
            GenerateRandomCollection(10, 1, 100);
        }
        public ArrayCollection(int[] arrayData)
        {
            this.Items = arrayData;
        }

        #region IDebugging
        public void PrintCollection()
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("Size={0}, SwapCount={1}, CompCount={2}, IsSorted={3}", Items.Length, Strategy.SwapCount, Strategy.CompCount, IsSorted());
            foreach (int i in Items)
            {
                Console.Write("{0}, ", i);
            }
            Console.WriteLine("\n-----------------------");

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", Items[0]);

            for (int i = 1; i < Items.Length; i++)
            {
                sb.AppendFormat(", {0}", Items[i]);
            }

            return sb.ToString();
        }

        public void GenerateRandomCollection(int size, int minValue, int maxValue)
        {
            int[] rndArray = new int[size];
            List<int> randCollection = new List<int>();
            Random rnd = new Random();

            for (int i = 0; i < size; i++)
            {
                rndArray[i] = rnd.Next(minValue, maxValue);
            }

            Items = rndArray;
        }
        #endregion

        #region ISoring
        public int SwapCount { get; set; }
        public int CompCount { get; set; }

        /// <summary>
        /// Move Largest Value to Right, Mulitple Passes
        /// Comparision: O(n^2) 
        /// Swaps: 
        /// Algorithm
        /// -Compare each array item to it's right neighor
        /// -If the right neighbor is smaller then Swap right and left
        /// -Repeat for the remaining array items
        /// </summary>
        public void BubbleSort()
        {
            SwapCount = 0;
            CompCount = 0;
            bool swapped;

            do
            {
                swapped = false;
                for (int i = 1; i < Items.Length; i++)
                {
                    CompCount++;
                    if (Items[i -1] > Items[i])
                    {
                        Swap(i - 1, i);
                        swapped = true;
                    }   
                }
            } while (swapped != false);
        }

        /// <summary>
        /// Sorts Left-to-Right, Mulitple Passes
        /// Avg. Comparision: O(n^2) 
        /// Best Comparision: O(n)
        /// Space Required: O(n)
        /// </summary>
        public void InsertSort()
        {
            throw new NotImplementedException("InsertSort Not Implemented");
        }

        /// <summary>
        /// Sorts Left-to-Right
        /// Comparision: O(n^2)
        /// Swaps: O(n-1)
        /// Typically better than Bubble but worst than Insert
        /// Algorithm
        /// -Enumerate the array from the first unsorted item to the end
        /// -Identify the smallest item
        /// -Swap the smallest item with the first unsorted item
        /// </summary>
        public void SelectSort()
        {
            SwapCount = 0;
            CompCount = 0;

            for (int i = 0; i < Items.Length - 1; i++)
            {
                Swap(i, FindMinIndex(i + 1));
            }
        }
        private int FindMinIndex(int start)
        {
            int minIndex = start;
            for (int i = start + 1; i < Items.Length; i++)
            {
                CompCount++;
                if (Items[minIndex] > Items[i])
                    minIndex = i;
            }
            return minIndex;
        }

        /// <summary>
        /// Worst Case Space/Comp: O(n log n) 
        /// -Appropriate for large datasets
        /// -data splitting means that the algrithm can be made parallel
        /// -performance is fixed
        /// Algorithm
        /// -the array is recursively split in half
        /// -when the array is in groups of 1, it is reconstructured in sort order
        /// -each reconstructed array is merged with the other half
        /// </summary>
        public void MergeSort()
        {
            CompCount = 0;
            SwapCount = 0;
            MergeSortInner(Items);
        }

        //Recursive Sort 
        private void MergeSortInner(int[] items)
        {
            //Recursive Break Point
            if (items.Length <= 1)
                return;

            int leftSize = items.Length / 2;
            int rightSize = items.Length - leftSize;

            int[] left = new int[leftSize];
            int[] right = new int[rightSize];

            //String Equiv ==> LEft = Items.SubString(0 to LeftSize)
            Array.Copy(items, 0, left, 0, leftSize);
            //String Equiv ==> Right = Item.Substring(LeftSize to RightSize)
            Array.Copy(items, leftSize, right, 0, rightSize);

            //Recursively Sort Left
            MergeSortInner(left);
            //Recursively Sort Right
            MergeSortInner(right);
            //Recursively Merge
            Merge(items, left, right);
        }

        //Merge and Sort
        private void Merge(int[] items, int[] left, int[] right)
        {
            int leftIndex = 0;
            int rightIndex = 0;
            int targetIndex = 0;

            int remaining = left.Length + right.Length;

            while (remaining > 0)
            {
                if (leftIndex >= left.Length)
                {
                    //++ is post-processing
                    items[targetIndex] = right[rightIndex++];
                }
                else if (rightIndex >= right.Length)
                {
                    //++ is post-processing
                    items[targetIndex] = left[leftIndex++];
                }
                else if (left[leftIndex] < right[rightIndex])
                {
                    //++ is post-processing
                    items[targetIndex] = left[leftIndex++];
                }
                else
                {
                    //++ is post-processing
                    items[targetIndex] = right[rightIndex++];
                }

                targetIndex++;
                remaining--;
            }
        }

        /// <summary>
        /// Avg. Case Space/Comp: O(n log n)
        /// 
        /// Algorithm
        /// -Divde and Conquer algorithm
        /// -Pick a pivot value and partition the array
        /// --many schools of thoughts of picking best pivot value
        /// -Put all values before the pivot to the left and above to the right
        /// --the pivot point is now sorted---everthing right is larger, everything left is smaller 
        /// -Perform pivot and partition algorithm on the left and right partitions
        /// -Repeat until sorted
        /// </summary>
        public void QuickSort()
        {
            SwapCount = 0;
            CompCount = 0;
            QuickSortInner(Items, 0, Items.Length - 1);
        }

        Random _pivotRng = new Random(); //randmoize pivot number
        private void QuickSortInner(int[] items, int left, int right)
        {
            CompCount++;
            if (left < right)
            {
                int pivotIndex = _pivotRng.Next(left, right);
                int newPivot = partition(items, left, right, pivotIndex);

                //Recursive Call to Sort Left
                QuickSortInner(items, left, newPivot - 1);
                //Recursive Call to Sort Right
                QuickSortInner(items, newPivot + 1, right);
            }
            else
                //Recursive Break Point
                return;
        }

        private int partition(int[] items, int left, int right, int pivotIndex)
        {
            int pivotValue = items[pivotIndex];

            Swap(pivotIndex, right);

            int storeIndex = left;

            for (int i = left; i < right; i++)
            {
                CompCount++;
                if (items[i] < pivotValue)
                {
                    Swap(i, storeIndex);
                    storeIndex++;
                }
            }

            Swap(storeIndex, right);

            return storeIndex;
        }



        public void RadixSort()
        {
            throw new NotImplementedException();
        }

        public void Swap(int index1, int index2)
        {
            SwapCount++;
            string before = ToString();
            int value1 = Items[index1];
            int value2 = Items[index2];

            if (index1 != index2)
            {
                int tmp = Items[index1];
                Items[index1] = Items[index2];
                Items[index2] = tmp;
            }

            string after = ToString();

            Console.WriteLine("{0} ==> Swap({1},{2}) ==> {3} | CompCount={4}, SwapCount is {5}", before, value1, value2, after, CompCount, SwapCount);
        }
        #endregion

        #region SortStrategy
        SortStrategy Strategy;

        public void SortViaStrategy(SortStrategyType sortType)
        {
            switch (sortType)
            {
                case SortStrategyType.BubbleSort:
                    Strategy = new BubbleSort();
                    Strategy.Sort(Items);
                    break;
                case SortStrategyType.InsertSort:
                    Strategy = new InsertSort();
                    Strategy.Sort(Items);
                    break;
                case SortStrategyType.SelectSort:
                    Strategy = new SelectSort();
                    Strategy.Sort(Items);
                    break;
                case SortStrategyType.MergeSort:
                    Strategy = new MergeSort();
                    Strategy.Sort(Items);
                    break;
                case SortStrategyType.QuickSort:
                    Strategy = new QuickSort();
                    Strategy.Sort(Items);
                    break;
                case SortStrategyType.RadixSort:
                    Strategy = new RadixSort();
                    Strategy.Sort(Items);
                    break;
                default:
                    throw new Exception("Sort Type No Found");
            }
        }

        public bool IsSorted()
        {
            List<int> itemsList = Items.ToList<int>();
            itemsList.Sort();

            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] != itemsList[i])
                {
                    //Console.WriteLine("Not Sorted!");
                    return false;
                }
            }

            //Console.WriteLine("Sorted!");
            return true;
        }
        
        #endregion
    }
}
