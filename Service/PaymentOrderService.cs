using System.Transactions;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class PaymentOrderService : CrudService<PaymentOrder>, IPaymentOrderService
    {
        private readonly IUniRepo u;

        public PaymentOrderService(IRepo<PaymentOrder> repo, IUniRepo u)
            : base(repo)
        {
            this.u = u;
        }

        public void CreateForContract(PaymentOrder o, int id)
        {
            o.State = PoState.Registered;
            using (var t = new TransactionScope())
            {
                var pid = u.Insert(o);
                u.UpdateWhatWhere<Contract>(new { paymentOrderId = pid }, new { id });
                t.Complete();
            }
        }

        public void CreateForAgreement(PaymentOrder o, int id)
        {
            o.State = PoState.Registered;
            using (var t = new TransactionScope())
            {
                var pid = u.Insert(o);
                u.UpdateWhatWhere<Agreement>(new { paymentOrderId = pid }, new { id });
                t.Complete();
            }
        }
    }
}