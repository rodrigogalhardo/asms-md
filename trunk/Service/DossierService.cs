using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class DossierService : IDossierService
    {
        private readonly IDossierRepo repo;

        public DossierService(IDossierRepo repo)
        {
            this.repo = repo;
        }

        public long Insert(Dossier o)
        {
            return repo.Insert(o);
        }

        public Dossier Get(int id)
        {
            return repo.Get(id);
        }
    }
}