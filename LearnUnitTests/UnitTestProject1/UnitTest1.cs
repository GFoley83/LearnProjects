using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearnUnitTests;

namespace UnitTestProject1
{
    [TestClass]
    public class MyServiceUnitTest
    {
        [TestMethod]
        public void TestRunMethodGoodInputs()
        {
            LearnUnitTests.MyService service = new MyService();
            int x = service.Run("4");
            Assert.AreEqual(x, 4);
        }
        [TestMethod]
        public void TestRunMethodBadInputs()
        {
            LearnUnitTests.MyService service = new MyService();
            string four = "fr";
            try
            {
                int x = service.Run("frt");
                Assert.Fail("Added Non Int String", four);
            }
            catch(Exception e)
            {
                Assert.IsNotNull(e);
            }
        }
    }
}
