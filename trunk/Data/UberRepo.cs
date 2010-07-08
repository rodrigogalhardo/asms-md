using System.Collections.Generic;
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
    }
}