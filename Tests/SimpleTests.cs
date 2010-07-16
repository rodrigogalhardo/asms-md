using System;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    [TestFixture]
    public class SimpleTests
    {
        [Test]
        public void Test()
        {
            Console.WriteLine(DateTime.Now.Date.ToShortDateString());
        }

        [Test]
        public void TestTrim()
        {
            Console.WriteLine("he,llo,".TrimEnd(new char[] { ',' }));
        }

        [Test]
        public void Dynamic()
        {
            dynamic x = 3;
            x = x + x;
            TestingTools.IsEqualTo(x, 6);
        }

        [Test]
        public void Enumint()
        {
            Assert.IsTrue((int)Haiza.Hasfields == 2);
        }
    }

    public enum Haiza
    {
        Registered = 1,
        Hasfields,
        Hascoefficients,
    }
}
