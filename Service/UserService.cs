using System.Collections.Generic;
using System.Linq;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepo repo;

        public UserService(IUserRepo repo)
        {
            this.repo = repo;
        }

        public long Insert(User user)
        {
            return repo.Insert(user);
        }

        public User Get(long id)
        {
            return repo.Get(id);
        }

        public User Get(string name)
        {
            return repo.GetWhere(new {name}).FirstOrDefault();
        }

        public User Get(string name, string password)
        {
            return repo.Get(name, password);
        }

        public User GetFull(long id)
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

        public long Create(User user)
        {
            return repo.Insert(user);
        }

        public void Save(User user)
        {
            repo.Update(user);
        }

        public IEnumerable<string> GetRoles(long id)
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

        public bool Exists(string name, string password)
        {
            return repo.Count(name, password) > 0;
        }

        public bool ChangePassword(long id, string password)
        {
            return repo.UpdatePassword(id, password) == 1;
        }
    }
}