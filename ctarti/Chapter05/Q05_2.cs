using ctarti.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ctarti.DataStructures;

namespace Chapter05
{
    public class Q05_2 : IQuestion
    {
        //Given double betweem 0 and 1 (e.g. 0.72), print binary repersentation. 
        //If cannot be repersented with 32 charecters, print "ERROR".
        public void Run()
        {
            double good = 0.72;
            double bad = 0.123456789012345678901234567890123;

            BitStuff.PrintDouble(good);
        }
    }
}
