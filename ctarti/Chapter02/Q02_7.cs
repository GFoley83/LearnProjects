using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02
{
    //7. Implement a function to check if a linked list is a palindrome. 
    public class Q02_7 : IQuestion
    {
        public void Run()
        {
            llNode notPalindrome = new llNode(0);
            notPalindrome.Next = new llNode(1);
            notPalindrome.Next.Next = new llNode(2);
            notPalindrome.Next.Next.Next = new llNode(3);
            notPalindrome.Next.Next.Next.Next = new llNode(4);
            notPalindrome.Next.Next.Next.Next.Next = new llNode(5);
            notPalindrome.Next.Next.Next.Next.Next.Next = new llNode(6);
            notPalindrome.Next.Next.Next.Next.Next.Next.Next = new llNode(7);
            notPalindrome.PrintList();
            Console.WriteLine("IsPalidndrome? {0}\n\n", IsPalidndrome(notPalindrome));


            llNode yesPalindrome = new llNode(0);
            yesPalindrome.Next = new llNode(1);
            yesPalindrome.Next.Next = new llNode(2);
            yesPalindrome.Next.Next.Next = new llNode(3);
            yesPalindrome.Next.Next.Next.Next = new llNode(3);
            yesPalindrome.Next.Next.Next.Next.Next = new llNode(2);
            yesPalindrome.Next.Next.Next.Next.Next.Next = new llNode(1);
            yesPalindrome.Next.Next.Next.Next.Next.Next.Next = new llNode(0);
            yesPalindrome.PrintList();
            Console.WriteLine("IsPalidndrome? {0}\n\n", IsPalidndrome(yesPalindrome));


        }

        private bool IsPalidndrome(llNode head)
        {
            llNode slow = head;
            llNode fast = head;
            Stack<llNode> halfList = new Stack<llNode>();
            
            while (fast != null && fast.Next != null)
            {
                
                halfList.Push(new llNode(slow.Data));
                slow = slow.Next;
                fast = fast.Next.Next;
            }

            //Has Odd Number of Elements, Skipp Middle Node
            if (fast != null)
            {
                slow = slow.Next;
            }

            //Compare New Lists
            while (halfList.Count > 0)
            {
                if (slow.Data != halfList.Pop().Data)
                    return false;
                slow = slow.Next;
            }

            return true;
        }
    }
}
