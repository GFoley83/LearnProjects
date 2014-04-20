using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ctarti.Library;

namespace Chapter02
{
    public class Q02_1 : IQuestion
    {
        int _tapB = 0;
        int _tapC = 0;

        void Tap(int i)
        {
            if (i == 0)
            {
                _tapB++;
            }
            else
            {
                _tapC++;
            }
        }

        public void Run()
        {
            //AssortedMethods.RandomLinkedList(1000, 0, 2);
            LinkedListNode first = new LinkedListNode(0, null, null);
            LinkedListNode originalList = first;
            LinkedListNode second;// = first;
            for (int i = 1; i < 9; i++)
            {
                second = new LinkedListNode(i % 3, null, null);
                first.SetNext(second);
                second.SetPrevious(first);
                first = second;
            }

            LinkedListNode list1 = originalList.Clone();
            LinkedListNode list2 = originalList.Clone();
            LinkedListNode list3 = originalList.Clone();

            DeleteDupsA(list1);
            DeleteDupsB(list2);
            DeleteDupsC(list3);

            Console.WriteLine(originalList.PrintForward());
            Console.WriteLine(list1.PrintForward());
            Console.WriteLine(list2.PrintForward());
            Console.WriteLine(list3.PrintForward());

            Console.WriteLine(_tapB);
            Console.WriteLine(_tapC);
            
        }



        private void DeleteDupsA(LinkedListNode list1)
        {
            if (list1 == null)
                throw new Exception("Null List");

            Dictionary<int, bool> dups = new Dictionary<int, bool>();
            LinkedListNode target = list1;

            //Enumerate
            while (target != null)
            {
                //Check for Duplicate
                if (dups.ContainsKey(target.Data))
                {
                    //Remove Duplicate
                    //P->T->N
                    //P---->N

                    target.RemoveSelf();
                }
                else
                    //Head will always be unique
                    dups.Add(target.Data, true);
                
                //Move Target Forward
                target = target.Next;
            }
        }


        private void DeleteDupsB(LinkedListNode list2)
        {
            if (list2 == null)
                throw new Exception("Null List");

            LinkedListNode target = list2;
            LinkedListNode runner;

            //Enumerate
            while (target != null)
            {
                runner = target.Next;

                Tap(0);

                //Check for Duplicate
                while (runner != null)
                {
                    if (target.Data == runner.Data)
                        runner.RemoveSelf();

                    runner = runner.Next;
                }
                
                //Move Target Forward
                target = target.Next;
            }
        }


        private void DeleteDupsC(LinkedListNode list3)
        {
        }
    }
}
