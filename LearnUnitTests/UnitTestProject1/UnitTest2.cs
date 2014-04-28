using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearnUnitTests;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            LearnUnitTests.MyService service = new MyService();
            int x = service.Run("55555");
            Assert.AreEqual(x, 55555);
            Assert.IsNotNull(service);
        }
    }
}
