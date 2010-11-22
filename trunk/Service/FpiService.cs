using System.Transactions;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class FpiService : IFpiService
    {
        private readonly IDossierService dossierService;
        private readonly IFpiRepo fpiRepo;

        public FpiService(IFpiRepo fpiRepo, IDossierService dossierService)
        {
            this.fpiRepo = fpiRepo;
            this.dossierService = dossierService;
        }

        public Fpi Get(int id)
        {
            return fpiRepo.Get(id);
        }

        public void ChangeAmount(int id, decimal amount, decimal amountm)
        {
            using (var scope = new TransactionScope())
            {
                var payed = fpiRepo.GetAmountPayed(id);
                if (amount < payed)
                    throw new AsmsEx("suma introdusa este mai mica decat suma deja autorizata spre plata");
                fpiRepo.UpdateWhatWhere(new { amount, amountm }, new { id });
                dossierService.Rerank(id);
                scope.Complete();
            }
        }
    }
}