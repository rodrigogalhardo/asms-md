using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class FpiService : IFpiService
    {
        private readonly IFpiRepo fpiRepo;

        public FpiService(IFpiRepo fpiRepo)
        {
            this.fpiRepo = fpiRepo;
        }

        public Fpi Get(int id)
        {
            return fpiRepo.Get(id);
        }

        public void ChangeAmount(int id, decimal amount)
        {
            var payed = fpiRepo.GetAmountPayed(id);
            if (amount < payed) throw new AsmsEx("suma introdusa este mai mica decat suma deja autorizata spre plata");
            fpiRepo.UpdateWhatWhere(new { amount }, new { id });
        }
    }
}