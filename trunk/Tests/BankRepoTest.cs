using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Data;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    [TestFixture]
    public class Regtest
    {
        [Test]
        public void Test()
        {
            var re = @"\d\d";

            var a = Regex.IsMatch("30.01.2009", @"\d\d.\d\d.\d\d\d\d");
            System.Console.Out.WriteLine(a);
        }
        
    }

    public class BankRepoTest : BaseRepoTest
    {
        readonly BankRepo repo = new BankRepo(new ConnectionFactory());

        [Test]
        public void Insert()
        {
            var id  = repo.Insert(new Bank { Code = "1234", Name = "name" });
            (id > 0).IsTrue();
        }

        [Test]
        public void Get()
        {
            var id = repo.Insert(new Bank { Code = "1234", Name = "name" });
            (id > 0).IsTrue();
            var w = new Stopwatch();
            w.Start();
            repo.Get(id).Code.IsEqualTo("1234");
            w.Stop();
            System.Console.Out.WriteLine(w.Elapsed);
            repo.Get(-1).IsNull();
        }

        [Test]
        public void Count()
        {
            repo.Count(null, null);
        }

        [Test]
        public void GetPage()
        {
            var result = repo.GetPage(1, 10, null, null).ToList();
            
        }

        [Test]
        public void SpeedTest()
        {
            var w = new Stopwatch();
            w.Start();
            var result = repo.GetPage(1, 10, null, null).ToList();
            w.Stop();
            System.Console.Out.WriteLine(w.Elapsed);
        }

    }
}