using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ctarti.DataStructures;

namespace Chapter05
{
    public class Chapter05_Notes : IQuestion
    {
        /*Bit Operators
        &	Both: Binary AND Operator copies a bit to the result if it exists in both operands.	
		        Example - (A & B) will give 12, which is 0000 1100

        |	Either: Binary OR Operator copies a bit if it exists in either operand.	
		        Example - (A | B) will give 61, which is 0011 1101

        ^	Only One: Binary XOR Operator copies the bit if it is set in one operand but not both.
		        Example - (A ^ B) will give 49, which is 0011 0001

        ~	Flip: Binary Ones Complement Operator is unary and has the effect of 'flipping' bits.	
		        Example - (~A ) will give -61, which is 1100 0011 in 2's complement due to a signed binary number.

        <<	Binary Left Shift Operator. The left operands value is moved left by the number of bits specified by the right operand.	
		        Example - A << 2 will give 240, which is 1111 0000

        >>	Binary Right Shift Operator. The left operands value is moved right by the number of bits specified by the right operand.
		        Example - A >> 2 will give 15, which is 0000 1111
         */

        public void Run()
        {
            int a = 60;
            int b = 13;
            int c = 0;

            BitStuff.PrintInt(a);
            BitStuff.PrintInt(b);

            c = a & b;
            //0011 1100
            //0000 1101
            //=========
            //0000 1100 =s 12
            BitStuff.PrintInt(c);

            c = a | b;
            //0011 1100
            //0000 1101
            //=========
            //0011 1101 = 32+16+8+4+1 = 61
            BitStuff.PrintInt(c);

            c = a ^ b;
            //0011 1100
            //0000 1101
            //=========
            //0011 0001 = 32+16+1 = 49
            BitStuff.PrintInt(c);

            c = ~a;
            //0011 1100
            //=========
            //11111111111111111111111111000011 = -61
            BitStuff.PrintInt(c);

            //-1: 11111111111111111111111111111111
            BitStuff.PrintInt(-1);

            //-2: 11111111111111111111111111111110
            BitStuff.PrintInt(-2);

            c = a >> 2;
            //0011 1100
            //=========
            //0000 1111 = 15
            BitStuff.PrintInt(c);

            c = a << 2;
            //0011 1100
            //=========
            //1111 0000 = 16+32+64+128 = 240
            BitStuff.PrintInt(c);

            Console.WriteLine("\nRotateLeft, RotateRight:");
            BitStuff.PrintInt(a);
            c = BitStuff.RotateLeft(a, 1);
            //0011 1100
            //=========
            //0111 1000 = 8+16+32+64 = 120
            BitStuff.PrintInt(c);

            c = BitStuff.RotateRight(c, 1);
            BitStuff.PrintInt(c);

            //SHORTCUTS
            //ROTL/SHL = num * 2
            Console.WriteLine("ROTL|SHL = num * 2");
            BitStuff.PrintInt(a);
            BitStuff.PrintInt(BitStuff.RotateLeft(a, 1));
            BitStuff.PrintInt(a << 1);

            //ROTL/SHL = num / 2
            Console.WriteLine("\nROTR = num / 2");
            BitStuff.PrintInt(a);
            BitStuff.PrintInt(BitStuff.RotateRight(a, 1));
            BitStuff.PrintInt(a >> 1);

            /* XOR
             * x ^ 0000 = x
             * x ^ 1111 = ~x
             * x ^ X    = 0000
             */

            /* AND
             * x & 0000 = 0000
             * x & 1111 = x
             * x & X    = x
             */

            /* OR
             * x | 0000 = x
             * x | 1111 = 1111
             * x | X    = x
             */

        }
    }
}
