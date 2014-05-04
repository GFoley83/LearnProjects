using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interative
{
    public class Program
    {
        static Stack<int>[] lists;
        static void Main(string[] args)
        {
            lists = new Stack<int>[3];
            lists[0] = new Stack<int>();
            lists[1] = new Stack<int>();
            lists[2] = new Stack<int>();
            for (int x = 5; x > 0; x--) lists[0].Push(x);
            int onePos = 0;
            int[][] evenorder = { new int[]{ 0, 1 }, new int[]{ 0, 2 }, new int[]{ 1, 2 } };
            int[][] oddorder = { new int[] { 0, 2 }, new int[] { 0, 1 }, new int[] { 2, 1 } };
            
            int[][] order = lists[0].Count % 2 == 0 ? evenorder : oddorder;

            int[] oddOnePosOrder = new int[3] { 2, 1, 0 };
            int[] evenOnePosOrder = new int[3] { 1, 2, 0 };

            int[] onPosOrder = lists[0].Count % 2 == 0 ? evenOnePosOrder : oddOnePosOrder;


            int counter = 0;
            while (lists[2].Count < 5)
            {
                if (counter % 2 == 0)
                {
                    int dest = onPosOrder[counter%3];
                     lists[dest].Push(lists[onePos].Pop());
                    onePos = dest;
                }
                else
                {
                    int[] curOrder = order[counter % 3];

                    if (isValidMove(lists[curOrder[0]], lists[curOrder[1]]))
                    {
                        lists[curOrder[1]].Push(lists[curOrder[0]].Pop());
                    }
                }
                counter++;
            }

        }
        static bool isValidMove(Stack<int> from, Stack<int> to)
        {
            if(to.Count == 0 && from.Count>0) return true;
            if(from.Count == 0) return false;
            return from.Peek() < to.Peek();
        }
    }
}
