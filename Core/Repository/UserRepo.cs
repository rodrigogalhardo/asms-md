using System.Collections.Generic;
using MRGSP.ASMS.Core.Model;

namespace MRGSP.ASMS.Core.Repository
{
    public interface IMeasuresetRepo : IRepo<Measureset>
    {
        int ChangeState(int id, int stateId);
        int Activate(int id);
    }

    public interface IFieldsetRepo : IRepo<Fieldset>
    {
        int ChangeState(int id, int stateId);
        int Activate(int id);
    }
    public interface IRepo<T> where T : new()
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        int Insert(T o);
        IEnumerable<T> GetPage(int page, int pageSize);
        int Count();
        IPageable<T> GetPageable(int page, int pageSize);
        IEnumerable<T> GetWhere(object where);
        int Delete(int id);
        int InsertNoIdentity(T o);
    }

    public interface IMeasureRepo : IRepo<Measure>
    {
        IEnumerable<Measure> GetAssigned(int measuresetId);
        IEnumerable<Measure> GetUnassigned(int measuresetId);
        int Assign(int measureId, int measuresetId);
        int Unassign(int measureId, int measuresetId);
        IEnumerable<Measure> GetActives();
    }

    public interface IFieldRepo
    {
        IEnumerable<Field> GetAssigned(int id);
        IEnumerable<Field> GetUnassigned(int id);
        int AssignField(int fieldId, int fieldsetId);
        int UnassignField(int fieldId, int fieldsetId);
    }

    public interface IUserRepo : IRepo<User>
    {
        IEnumerable<Role> GetRoles(long id);
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

    public interface IDossierRepo : IRepo<Dossier>
    {
        int ChangeState(int id, int stateId);
    }
}
