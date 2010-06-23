using System.Linq;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Data;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    [TestFixture]
    public class UserRepositoryTest : BaseRepoTest
    {
        readonly UserRepo repo = new UserRepo(new ConnectionFactory());

        [Test]
        public void GetRolesTest()
        {
            (repo.GetRoles().Count() > 0).IsTrue();
        }

        [Test]
        public void Insert()
        {
            var id = repo.Insert("jora".AsUser());
            repo.Get(id).Name.IsEqualTo("jora");
            repo.GetRoles(id).Count().IsEqualTo(2);
        }

        [Test]
        public void Update()
        {
            var id = repo.Insert("jora".AsUser());
            repo.GetRoles(id).Count().IsEqualTo(2);
            var user = repo.Get(id);
            user.Roles = new[] { new Role() { Id = 3 } };
            repo.Update(user);
            repo.GetRoles(id).Count().IsEqualTo(1);
        }

        [Test]
        public void GetPageAndCount()
        {
            repo.Insert(new User { Name = "j", Password = "a" });
            repo.Insert(new User { Name = "j1", Password = "a" });
            repo.Insert(new User { Name = "j2", Password = "a" });
            repo.GetPage(1, 3).Count().IsEqualTo(3);
            (repo.Count() > 3).IsTrue();
        }

        [Test]
        public void GetRolesByUser()
        {
            var roles = repo.GetRoles(1).ToArray();
            roles.Count().IsEqualTo(2);
            roles[0].Name.IsEqualTo("admin");
            roles[1].Name.IsEqualTo("superuser");
        }

        [Test]
        public void GetUser()
        {
            var id = repo.Insert("j".AsUser());

            var user = repo.Get(id);
            user.IsNotNull();
            user.Name.IsEqualTo("j");
        }

        [Test]
        public void CountByNamePassword()
        {
            repo.Insert("uber".AsUser());
            repo.Count("uber", "uberpass").IsEqualTo(1);
        }

        [Test]
        public void UpdatePassword()
        {
            var uid = repo.Insert("user".AsUser());
            repo.UpdatePassword(uid, "aa");
            repo.Get(uid).Password.IsEqualTo("aa");
        }

        [Test]
        public void GetUserByNamePass()
        {
            repo.Insert("uber".AsUser());
            var user = repo.Get("uber", "uberpass");
            user.Name.IsEqualTo("uber");
            user.Password.IsEqualTo("uberpass");
        }

        [Test]
        public void GetUnexistentUser()
        {
            var x = repo.Get("asdfasdf", "afadf");

            x.IsEqualTo(null);

        }
    }
}