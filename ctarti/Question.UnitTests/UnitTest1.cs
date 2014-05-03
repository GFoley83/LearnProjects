using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ctarti.Library;
using Chapter01;
using System.Diagnostics;

namespace Question.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        /* Test Cases:
         * Unique Chars Even
         * Non-Unique Chars
         * Empty String
         * Whitespace
         * Single Char
         * Unicode
         */

        Q01_1 q = new Q01_1();
        string strUnique = @"!#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
        string strNonUnique = "1233";
        string strEmptyString = "";
        string strWhiteSpace = " \t";
        string strSingleChar = "5";
        string strUnicode = "\uD8FF";
        
        [TestMethod]
        public void TestQ01_1_SucessUnique()
        {
            Assert.IsTrue(q.AllUniqueChars1(strUnique), "TestQ01_1_SucessUnique", strUnique);
        }

        [TestMethod]
        public void TestQ01_1_BadNonUnique()
        {
            Assert.IsFalse(q.AllUniqueChars1(strNonUnique), "TestQ01_1_BadNonUnique", strNonUnique);
        }

        [TestMethod]
        public void TestQ01_1_EmptyString()
        {
            Assert.IsTrue(q.AllUniqueChars1(strEmptyString), "strEmptyString", strEmptyString);
        }

        [TestMethod]
        public void TestQ01_1_WhiteSpace()
        {
            Assert.IsTrue(q.AllUniqueChars1(strWhiteSpace), "strWhiteSpace", strWhiteSpace);
        }

        [TestMethod]
        public void TestQ01_1_SingleChar()
        {
            Assert.IsTrue(q.AllUniqueChars1(strSingleChar), "strSingleChar", strSingleChar);
        }

        [TestMethod]
        public void TestQ01_1_Unicode()
        {
            try
            {
                q.AllUniqueChars1(strUnicode);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException ex)
            {
                //Success
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected IndexOutOfRangeException");
            }
        }

        [TestMethod]
        public void TestQ01_2_SucessUnique()
        {
            Assert.IsTrue(q.AllUniqueChars2(strUnique), "TestQ01_1_SucessUnique", strUnique);
        }

        [TestMethod]
        public void TestQ01_2_BadNonUnique()
        {
            Assert.IsFalse(q.AllUniqueChars2(strNonUnique), "TestQ01_1_BadNonUnique", strNonUnique);
        }

        [TestMethod]
        public void TestQ01_2_EmptyString()
        {
            Assert.IsTrue(q.AllUniqueChars2(strEmptyString), "strEmptyString", strEmptyString);
        }

        [TestMethod]
        public void TestQ01_2_WhiteSpace()
        {
            Assert.IsTrue(q.AllUniqueChars2(strWhiteSpace), "strWhiteSpace", strWhiteSpace);
        }

        [TestMethod]
        public void TestQ01_2_SingleChar()
        {
            Assert.IsTrue(q.AllUniqueChars2(strSingleChar), "strSingleChar", strSingleChar);
        }

        [TestMethod]
        public void TestQ01_2_Unicode()
        {
            try
            {
                q.AllUniqueChars2(strUnicode);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException ex)
            {
                //Success
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected IndexOutOfRangeException");
            }
        }

    }
}
