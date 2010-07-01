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
        IEnumerable<T> GetPage(int page, int pageSize);
        int Count();
    }

    public interface IBankRepo
    {
        long Insert(Bank o);
        int Count(string code);
        string Delete(int id);
        int Count(string name, string code);
        IEnumerable<Bank> GetPage(int page, int pageSize, string name, string code);
        Bank Get(long id);
    }

    public interface IFarmerRepo
    {
        long Insert(Farmer o);
        Farmer Get(long id);
        int Count(string name, string code);
        IEnumerable<Farmer> GetPage(int page, int pageSize, string name, string code);
    }
}
