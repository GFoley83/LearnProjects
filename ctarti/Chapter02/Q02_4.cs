using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02
{
    /*	4. Write code to partition a linked list around a value x, 
     * such that all nodes less than x come before all nodes great than or equal to x.
     */
    public class Q02_4 : IQuestion
    {
        public void Run()
        {
            llNode head = new llNode(300);
            head.Next = new llNode(400);
            head.Next.Next = new llNode(200);
            head.Next.Next.Next = new llNode(200);
            head.Next.Next.Next.Next = new llNode(100);
            head.Next.Next.Next.Next.Next = new llNode(500);


            PartitionLL(head, 200).PrintList();
        }

        private llNode PartitionLL(llNode head, int partValue)
        {
            //Duplicates
            llNode left = null;
            llNode partNode = null;
            llNode right = null;

            llNode currentNode = head;

            //Seperate
            while (currentNode != null)
            {
                if (currentNode.Data < partValue)
                {
                    if (left == null)
                        left = new llNode(currentNode.Data);
                    else
                        left.Append(new llNode(currentNode.Data));
                }
                else if (currentNode.Data == partValue)
                {
                    if (partNode == null)
                        partNode = new llNode(currentNode.Data);
                    else
                        partNode.Append(new llNode(currentNode.Data));
                }
                else
                {
                    if (right == null)
                        right = new llNode(currentNode.Data);
                    else
                        right.Append(new llNode(currentNode.Data));
                }
                
                //Move Forward
                currentNode = currentNode.Next;
            }

            left.Append(partNode);
            left.Append(right);

            return left;
        }
    }
}
