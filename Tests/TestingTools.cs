using MRGSP.ASMS.Core.Model;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    public static class TestingTools
    {
        public static void IsEqualTo(this object o, object to)
        {
            Assert.AreEqual(to, o);
        }

        public static void IsTrue(this bool o)
        {
            Assert.IsTrue(o);
        }

        public static User AsUser(this string name)
        {
            return new User()
                       {
                           Name = name,
                           Password = name + "pass",
                           Roles = new[]
                                       {
                                           new Role(){Id = 1},
                                           new Role(){Id = 2},
                                       }
                       };
        }

        public static void IsNotNull(this object o)
        {
            Assert.IsNotNull(o);
        }
    }
}