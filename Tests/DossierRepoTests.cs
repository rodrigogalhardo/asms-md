using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Data;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    [TestFixture]
    public class DbUtilTestses : BaseRepoTests
    {
        [Test]
        public void CountWhereTest()
        {
            DbUtil.CountWhere<District>(new {Name = "Ialoveni"}, new ConnectionFactory().GetConnectionString());
        }
    }
}