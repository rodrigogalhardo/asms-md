using System;
using System.Collections.Generic;
using System.Linq;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class AgreementService : CrudService<Agreement>, IAgreementService
    {
        private readonly IFpiRepo fpiRepo;
        private readonly IUniRepo u;


        public AgreementService(IRepo<Agreement> repo, IFpiRepo fpiRepo, IUniRepo u)
            : base(repo)
        {
            this.fpiRepo = fpiRepo;
            this.u = u;
        }

        public IEnumerable<Agreement> GetByContractId(int contractId)
        {
            return u.GetWhere<Agreement>(new { contractId });
        }

        public override int Create(Agreement o)
        {
            var contract = u.Get<Contract>(o.ContractId);
            var dossier = u.Get<Dossier>(contract.DossierId);
            (dossier.CreatedDate.Year != DateTime.Now.Year).B("acest dosar a fost creat in alt an");

            var fpi = u.GetWhere<Fpi>(new { dossier.MeasureId, dossier.MeasuresetId, State = FpiState.Agreement }).SingleOrDefault();

            fpi.IsNull().B("la moment nu este posibil crearea unui acord aditional");

            var payed = fpiRepo.GetAmountPayed(fpi.Id);

            ((fpi.Amount - payed - o.Amount) < 0).B("la moment nu sunt bani deajuns pentru acest acord");

            o.FpiId = fpi.Id;
            var ags = u.GetWhere<Agreement>(new { o.ContractId });
            o.Nr = (byte)(ags.Count() == 0 ? 1 : 1 + ags.Max(a => a.Nr));

            return u.Insert(o);
        }
    }
}