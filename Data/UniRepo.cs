using System.Collections.Generic;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Repository;
using Omu.Awesome.Core;

namespace MRGSP.ASMS.Data
{
    public class UniRepo : BaseRepo, IUniRepo
    {
        public UniRepo(IConnectionFactory connFactory) : base(connFactory)
        {
        }

        public IPageable<T> GetPageable<T>(int page, int pageSize) where T : new()
        {
            return new Pageable<T>
            {
                Page = DbUtil.GetPage<T>(page, pageSize, Cs),
                PageCount = Tools.GetPageCount(pageSize, DbUtil.Count<T>(Cs)),
                PageIndex = page,
            };
        }

        public IEnumerable<T> GetAll<T>() where T : new()
        {
            return DbUtil.GetAll<T>(Cs);
        }

        public T Get<T>(int id) where T : new()
        {
            return DbUtil.Get<T>(id, Cs);
        }        
        
        public int Delete<T>(int id) where T : new()
        {
            return DbUtil.Delete<T>(id, Cs);
        }

        public IEnumerable<T> GetWhere<T>(object where) where T : new()
        {
            return DbUtil.GetWhere<T>(where, Cs);
        }

        public int InsertNoIdentity(object o)
        {
            return DbUtil.InsertNoIdentity(o, Cs);
        }

        public int DeleteWhere<T>(object where)
        {
            return DbUtil.DeleteWhere<T>(where, Cs);
        }

        public int Insert(object o)
        {
            return DbUtil.Insert(o, Cs);
        }

        public int UpdateWhatWhere<T>(object what, object where)
        {
            return DbUtil.UpdateWhatWhere<T>(what, where, Cs);
        }

    }
}