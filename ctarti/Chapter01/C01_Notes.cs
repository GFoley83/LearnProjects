using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Chapter01
{
    public class C01_Notes : IQuestion
    {
        public void Run()
        {
            //Hash Table aka Dictionary
            /* Simple: HashTable<hash(value), obj value>
             * Bad: Use array with index=key, n=value. 
             *      Must create large array for largetest key value, e.g. keys = {1,3, 1000, 10000}.
             * Better: Store Keys as a linked list.
             * Best: Store Keys as a binary search tree. 
             *      Guarantees O(log n) lookup time, since we can keep the tree balanced. 
             *      May use less space, since a large array no longer needs to be allocated in the very begainning.
             */
            Dictionary<int, string> myDictionary = new Dictionary<int, string>();
            int key = 0;
            string value = "value";
            myDictionary.Add(key, value);

            //ArrayList
            /* List dynamically resizing array as needed while still providing O(1) access.
             * A typical implemention is that when the array is full, the array doubles in size.
             * Each doubling takes O(n) time, but happens so rarely that its amortized time is still O(1).
             */
            List<string> myList = new List<string>();

            //StringBuffer aka StringBuilder
            StringBuilder myStringBuilder = new StringBuilder();
        }
    }
}
