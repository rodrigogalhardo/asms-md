using System.Linq;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class ContractService : IContractService
    {
        private readonly IDossierRulesService rules;
        private readonly IRepo<Contract> contractRepo;

        public ContractService(IDossierRulesService rules, IRepo<Contract> contractRepo)
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

        public int Create(Contract o)
        {
            rules.MustBe(o.DossierId, DossierStates.Authorized);
            if (Exists(o.DossierId)) throw new AsmsEx("acest dosar deja are contract creat");
            return contractRepo.Insert(o);
        }

        public Contract Get(int id)
        {
            return contractRepo.Get(id);
        }

        public void Save(Contract o)
        {
            contractRepo.Update(o);
        }
    }

    public static class Extensions
    {
        public static string Display(this AddressInfo o)
        {
            var dist = o.District.Contains("Chi") ? "" : "r.";
            var r = string.Format("{2} {0} loc. {1}", o.District, o.Locality, dist);
            if (!string.IsNullOrWhiteSpace(o.Street)) r += " str. " + o.Street;
            if (!string.IsNullOrWhiteSpace(o.House)) r += " bl. " + o.House;
            if (!string.IsNullOrWhiteSpace(o.Apartment)) r += " ap. " + o.Apartment;
            return r;
        }
    }
}