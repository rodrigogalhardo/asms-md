using System.Linq;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Service
{
    public class DossierRulesService : IDossierRulesService
    {
        public IRepo<Dossier> repo;

        public DossierRulesService(IRepo<Dossier> repo)
        {
            this.repo = repo;
        }

        public void MustBe(int id, params DossierStates[] states)
        {
            var d = repo.Get(id);
            if (d == null) throw new AsmsEx("acest dosar nu exista");
            if (states.All(o => o != d.StateId)) throw new AsmsEx("acest dosar nu este in statutul necesar pentru aceasta actiune");
        }

        public void MustNotBe(int id, DossierStates state)
        {
            var d = repo.Get(id);
            if (d == null) throw new AsmsEx("acest dosar nu exista");
            if (d.StateId == state) throw new AsmsEx("acest dosar nu este in statutul necesar pentru aceasta actiune");
        }
    }
}