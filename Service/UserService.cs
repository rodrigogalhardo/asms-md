using System.Collections.Generic;
using System.Linq;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using Omu.Awesome.Core;
using Omu.Encrypto;

namespace MRGSP.ASMS.Service
{
    public class UserService : CrudService<User>, IUserService
    {
        private new readonly IUserRepo repo;
        private readonly IHasher hasher = new Hasher();

        public UserService(IUserRepo repo) : base(repo)
        {
            this.repo = repo;
        }

        public long Insert(User user)
        {
            return repo.Insert(user);
        }

        public override User Get(int id)
        {
            var o = repo.Get(id);
            o.Roles = repo.GetRoles(id);
            return o;
        }

        public User Get(string name)
        {
            return repo.GetWhere(new {name}).FirstOrDefault();
        }

        public bool Validate(string name, string password)
        {
            var u = Get(name);
            return u != null && hasher.CompareStringToHash(password, u.Password);
        }

        public User GetFull(int id)
        {
            var o = repo.Get(id);
            o.Roles = repo.GetRoles(id);
            return o;
        }

        public IPageable<User> GetPage(int page, int pageSize)
        {
            return new Pageable<User>
            {
                Page = repo.GetPage(page, pageSize),
                PageCount = ServiceUtils.GetPageCount(pageSize, repo.Count()),
                PageIndex = page,
            };
        }

        public override int Create(User user)
        {
            user.Password = hasher.Encrypt(user.Password);
            return base.Create(user);
        }

        public override void Save(User user)
        {
            repo.ChangeRoles(user);
        }

        public IEnumerable<string> GetRoles(int id)
        {
            return repo.GetRoles(id).Select(o => o.Name);
        }

        public IEnumerable<Role> GetRoles()
        {
            return repo.GetRoles();
        }

        public bool Exists(string name)
        {
            return repo.Count(name).Equals(1);
        }

        public bool ChangePassword(int id, string password)
        {
            return repo.UpdatePassword(id, hasher.Encrypt(password)) == 1;
        }
    }
}