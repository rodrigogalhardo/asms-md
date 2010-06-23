using System.Collections.Generic;
using MRGSP.ASMS.Core.Model;

namespace MRGSP.ASMS.Core.Repository
{
    public interface IUserRepo : IPagedRepo<User>
    {
        long Insert(User o);
        IEnumerable<Role> GetRoles(long id);
        User Get(long id);
        int Count(string name, string password);
        int Count(string name);
        IEnumerable<Role> GetRoles();
        int UpdatePassword(long id, string password);
        void Update(User o);
        User Get(string name, string password);
    }

    public interface IPagedRepo<T>
    {
        int Count();
        IEnumerable<T> GetPage(int page, int pageSize);

    }

    public interface IBankRepo : IPagedRepo<Bank>
    {
        int Insert(Bank o);
        int Count(string code);
        string Delete(int id);
    }
}
