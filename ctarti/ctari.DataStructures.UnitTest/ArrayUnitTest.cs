using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ctarti.DataStructures;
using System.Collections.Generic;
using System.Linq;

namespace ctari.DataStructures.UnitTest
{
    [TestClass]
    public class ArrayUnitTest
    {
        [TestMethod]
        public void TestSorting()
        {
            ArrayCollection arrayCollection = new ArrayCollection();
            arrayCollection.GenerateRandomCollection(100, 0, 1000);

            int[] aBubbleSort = (int[])arrayCollection.Items.Clone();
            int[] aInsertSort = (int[])arrayCollection.Items.Clone();
            int[] aSelectSort = (int[])arrayCollection.Items.Clone();
            int[] aMergeSort = (int[])arrayCollection.Items.Clone();
            int[] aQuickSort = (int[])arrayCollection.Items.Clone();


            ArrayCollection acBubbleSort = new ArrayCollection(aBubbleSort);
            ArrayCollection acInsertSort = new ArrayCollection(aInsertSort);
            ArrayCollection acSelectSort = new ArrayCollection(aSelectSort);
            ArrayCollection acMergeSort = new ArrayCollection(aMergeSort);
            ArrayCollection acQuickSort = new ArrayCollection(aQuickSort);

            List<int> list = arrayCollection.Items.ToList<int>();
            list.Sort();
            int[] array = list.ToArray();

            acBubbleSort.BubbleSort();
            acInsertSort.InsertSort();
            acSelectSort.SelectSort();
            acMergeSort.MergeSort();
            acQuickSort.QuickSort();

            for (int i = 0; i < array.Length; i++)
            {
                Assert.AreEqual(array[i], acBubbleSort.Items[i]);
                //Assert.AreEqual(array[i], acInsertSort.Items[i]);
                //Assert.AreEqual(array[i], acSelectSort.Items[i]);
                Assert.AreEqual(array[i], acMergeSort.Items[i]);
                Assert.AreEqual(array[i], acQuickSort.Items[i]);
            }
        }
    }
}
