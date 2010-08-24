using System;
using System.Linq;
using MRGSP.ASMS.Data;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    [TestFixture]
    public class FarmerInfoRepoTests : BaseRepoTests
    {
        readonly FarmerInfoRepo r = new FarmerInfoRepo(new ConnectionFactory());

        [Test]
        public void Seek()
        {
            var x = r.Seek("", "123");
            Console.WriteLine(x.Count());
        }
    }
}