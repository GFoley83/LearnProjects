using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03
{
    /*	6. Write a program to sort a stack in ascending order (with biggest items on top).
     * You may use at most one additional stack to hold items, but you may not copy the 
     * elements into any other data structure (such as an array). The stack supports the 
     * following operations: push, pop, peek, is empty.*/

    public class Q03_6 : IQuestion
    {
        public void Run()
        {
            Console.WriteLine("Stack #1: max = Stack1.Largetst. S1.Pop and S2.Puch until reach Max. Save Max to Tmp Variable. Replace Other Itmes to S1, i.e. S2.Pop and S1.Push.");
            Console.WriteLine("Repeat until fully sorted on S2 in desc order. Then flip it to stack 1 in asending ordering.");
        }
    }
}
