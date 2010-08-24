using System.Linq;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Data;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    [Ignore]
    public class FarmerRepoTests: BaseRepoTests
    {
        readonly FarmerRepo repo = new FarmerRepo(new ConnectionFactory());

        [Test]
        public void Count()
        {
            repo.Count(null, null);
        }

        [Test]
        public void GetPage()
        {
            repo.GetPage(1, 10, null, null).ToList();

        }
    }
}