using System.Linq;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Data;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    [TestFixture]
    public class UberRepoTestses : BaseRepoTests
    {
        readonly Repo<District> drepo = new Repo<District>(new ConnectionFactory());
        readonly Repo<Field> frepo = new Repo<Field>(new ConnectionFactory());

        [Test]
        public void GetAll()
        {
            drepo.GetAll();
        }

        [Test]
        public void Get()
        {
            drepo.Get(0).ShouldEqual(null);
        }

        [Test]
        public void InsertAndGetPage()
        {
            frepo.Insert(new Field() { Name = "asdf" });
            frepo.Insert(new Field() { Name = "zxcvz" });

            frepo.GetPage(1, 2).Count().ShouldEqual(2);
        }
    }
}