using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Data;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    [TestFixture]
    public class UberRepoTests
    {
        readonly UberRepo<District> drepo = new UberRepo<District>(new ConnectionFactory());

        [Test]
        public void GetAll()
        {
            drepo.GetAll();
        }

        [Test]
        public void Get()
        {
            drepo.Get(0).IsEqualTo(null);
        }

    }
}