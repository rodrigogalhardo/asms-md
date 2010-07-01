using System.Collections.Generic;
using MRGSP.ASMS.Core.Model;

namespace MRGSP.ASMS.Core.Service
{
    public interface IUserService
    {
        long Insert(User user);
        User Get(long id);
        IPageable<User> GetPage(int page, int pageSize);
        long Create(User user);
        IEnumerable<string> GetRoles(long id);
        bool Exists(string name);
        IEnumerable<Role> GetRoles();
        bool ChangePassword(long id, string password);
        void Save(User user);
        User GetFull(long id);
        bool Exists(string name, string password);
        User Get(string name, string password);
    }

    public interface IBankService
    {
        long Create(Bank o);
        IPageable<Bank> GetPage(int page, int pageSize = 10, string name = null, string code = null);
        bool Exists(string code);
        string Delete(int id);
        Bank Get(long id);
    }

    public interface IFarmerService
    {
        long Create(Farmer o);
        IPageable<Farmer> GetPage(int page, int pageSize, string name = null, string code = null);
        bool Exists(string code);
        Farmer Get(long id);
    }

}