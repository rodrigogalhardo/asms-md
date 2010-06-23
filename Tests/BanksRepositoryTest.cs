using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Data;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    public class BanksRepositoryTest : BaseRepoTest
    {
        readonly BankRepo repo = new BankRepo(new ConnectionFactory());

        [Test]
        public void Insert()
        {
            var id  = repo.Insert(new Bank { Code = "1234", Name = "name" });
            (id > 0).IsTrue();
        }

        [Test]
        public void Count()
        {
            repo.Count();
        }
    }
}