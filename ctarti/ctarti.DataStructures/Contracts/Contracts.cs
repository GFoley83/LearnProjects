using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctarti.DataStructures
{
    public interface IDebugging
    {
            void PrintCollection();
            void GenerateRandomCollection(int size, int minValue, int maxValue);
    }

    public interface ISorting
    {
        int SwapCount { get; set; }
        int CompCount { get; set; }
        int[] Items { get; set; }

        void BubbleSort();
        void SelectSort();
        void InsertSort();
        void QuickSort();
        void MergeSort();
        void RadixSort();
        void Swap(int index1, int index2);
    }
}
