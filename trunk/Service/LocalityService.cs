using System.Linq;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Data;

namespace MRGSP.ASMS.Service
{
    public class LocalityService : Repo<Locality>, ILocalityService 
    {
        private readonly IRepo<Locality> repo;

        public LocalityService(IConnectionFactory connFactory, IRepo<Locality> repo) : base(connFactory)
        {
            this.repo = repo;
        }

        public int? Resolve(int? id, string name)
        {
            if (id != null && id != 0)
            {
                var o = repo.Get(id.Value);
                if (o == null) return null;
                return o.Id;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                var r = repo.GetWhere(new {Name = name}).FirstOrDefault();
                if (r != null) return r.Id;

                var o = repo.Insert(new Locality { Name = name });
                return o;
            }

            return null;
        }
    }

}