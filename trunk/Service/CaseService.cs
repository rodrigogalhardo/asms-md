using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepo repo;

        public CaseService(ICaseRepo repo)
        {
            this.repo = repo;
        }

        public long Insert(Case o)
        {
            return repo.Insert(o);
        }

        public Case Get(long id)
        {
            return repo.Get(id);
        }
    }
}