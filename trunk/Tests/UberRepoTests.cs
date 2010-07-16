using System.Linq;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Data;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    [TestFixture]
    public class UberRepoTests : BaseRepoTest
    {
        readonly UberRepo<District> drepo = new UberRepo<District>(new ConnectionFactory());
        readonly UberRepo<Field> frepo = new UberRepo<Field>(new ConnectionFactory());

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

        [Test]
        public void InsertAndGetPage()
        {
            frepo.Insert(new Field() { Name = "asdf" });
            frepo.Insert(new Field() { Name = "zxcvz" });

            frepo.GetPage(1, 2).Count().IsEqualTo(2);
        }
    }
}