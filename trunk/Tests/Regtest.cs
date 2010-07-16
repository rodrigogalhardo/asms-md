using System.Text.RegularExpressions;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    [TestFixture]
    public class Regtest
    {
        [Test]
        public void Test()
        {
            
            var a = Regex.IsMatch("30.01.2009", @"\d\d.\d\d.\d\d\d\d");
            System.Console.Out.WriteLine(a);
        }
        
    }
}