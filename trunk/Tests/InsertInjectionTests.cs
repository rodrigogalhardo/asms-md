using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Data;
using NUnit.Framework;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Tests
{
    [TestFixture]
    public class InsertInjectionTests
    {
        [Test]
        public void Test()
        {
            //var s = (string) "".InjectFrom<InsertInjection>(new Field() {Name = "name", Description = "good"});
            //System.Console.WriteLine(s);
            var o = new {Code = "asdf", Name = "asd"};
            var s = "insert " + o.GetType().Name + "s("
                    .InjectFrom(new FieldsBy().IgnoreFields("Id"), o) + ") values("
                    .InjectFrom(new FieldsBy().IgnoreFields("Id").SetFormat("@{0}"), o);
            System.Console.WriteLine(s);
        }

        [Test]
        public void WhereInjectionTest()
        {
            System.Console.WriteLine("select * from pros where ".InjectFrom<WhereInjection>(new { id = 5, name = "athene"}));
        }
    }

    
}