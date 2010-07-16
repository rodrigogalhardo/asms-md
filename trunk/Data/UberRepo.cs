using System.Collections.Generic;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class UberRepo<T> : BaseRepo, IUberRepo<T> where T : new()
    {
        public UberRepo(IConnectionFactory connFactory) : base(connFactory)
        {
        }

        public T Get(long id) 
        {
            return DbUtil.Get<T>(id, Cs);
        }

        public IEnumerable<T> GetAll()
        {
            return DbUtil.GetAll<T>(Cs);
        }

        public int Insert(T o)
        {
            return DbUtil.Insert(o, Cs);
        }

        public IEnumerable<T> GetPage(int page, int pageSize)
        {
            return DbUtil.GetPage<T>(page, pageSize, Cs);
        }

        public int Count()
        {
            return DbUtil.Count<T>(Cs);
        }

        public IPageable<T> GetPageable(int page, int pageSize)
        {
            return new Pageable<T>
            {
                Page = GetPage(page, pageSize),
                PageCount = Tools.GetPageCount(pageSize, Count()),
                PageIndex = page,
            };
        }

        public IEnumerable<T> GetWhere(object where)
        {
            return DbUtil.GetWhere<T>(where, Cs);
        }

        public int Delete(int id)
        {
            return DbUtil.Delete<T>(id, Cs);
        }
    }
}