using System.Linq;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class ContractService : CrudService<Contract>, IContractService
    {
        private readonly IDossierRulesService rules;
        private readonly IRepo<Contract> contractRepo;

        public ContractService(IRepo<Contract> repo, IDossierRulesService rules, IRepo<Contract> contractRepo) : base(repo)
        {
            this.rules = rules;
            this.contractRepo = contractRepo;
        }

        public Contract GetByDossier(int dossierId)
        {
            return contractRepo.GetWhere(new { dossierId }).SingleOrDefault();
        }

        public bool Exists(int dossierId)
        {
            return contractRepo.GetWhere(new { dossierId }).Count() > 0;
        }

        public override int Create(Contract o)
        {
            rules.MustBe(o.DossierId, DossierStates.Authorized);
            if (Exists(o.DossierId)) throw new AsmsEx("acest dosar deja are contract creat");
            return contractRepo.Insert(o);
        }
    }
}