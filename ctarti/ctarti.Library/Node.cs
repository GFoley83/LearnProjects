﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctarti.Library
{
    public class Node
    {
        public Node next = null;
        public int data;

        public Node(int d)
        {
            data = d;
        }

        void appendToTail(int d)
        {
            Node end = new Node(d);
            Node n = this;
            while (n.next != null)
                n = n.next;
            n.next = end;
        }

        Node deleteNote(Node head, int d)
        {
            Node n = head;

            if (n.data == d)
                return head.next; /* Move Ahead */

            while (n.next != null)
            {
                if (n.next.data == d)
                {
                    n.next = n.next.next;
                    return head; /* head didn't change */
                }
                n = n.next;
            }

            return head;
        }
    }
}
