using System;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class PaymentOrderController : BaseController
    {
        private readonly IPaymentOrderService s;
        private readonly IBuilder<PaymentOrder, PaymentOrderCreateInput> v;
        private IBuilder<PaymentOrder, PaymentOrderEditInput> ve;

        public PaymentOrderController(IPaymentOrderService s, IBuilder<PaymentOrder, PaymentOrderCreateInput> v, IBuilder<PaymentOrder, PaymentOrderEditInput> ve)
        {
            this.s = s;
            this.v = v;
            this.ve = ve;
        }

        public ActionResult CreateForContract(int contractId)
        {
            return View("create", new PaymentOrderCreateInput { ContractId = contractId, Date = DateTime.Now});
        }

        public ActionResult CreateForAgreement(int contractId, int agreementId)
        {
            return View("create", new PaymentOrderCreateInput { ContractId = contractId, AgreementId = agreementId, Date = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Create(PaymentOrderCreateInput input)
        {
            if (!ModelState.IsValid) return View(input);
            if (input.AgreementId.HasValue)
            {
                s.CreateForAgreement(v.BuildEntity(input), input.AgreementId.Value);
                return Json(new{Id = input.AgreementId.Value, type = 'a' });
            }
            if (input.ContractId.HasValue) {
                s.CreateForContract(v.BuildEntity(input), input.ContractId.Value);
                return Json(new {Id = input.ContractId.Value, type = 'c'});
            }
            throw new InvalidOperationException("imposibil de creat ordin de plata");
        }

        public ActionResult Edit(int? id)
        {
            return View(ve.BuildInput(s.Get(id.Value)));
        }

        [HttpPost]
        public ActionResult Edit(PaymentOrderEditInput input)
        {
            if (!ModelState.IsValid) return View(input);
            s.Save(ve.BuildEntity(input, input.Id));
            return Json(new{input.Id});
        }
    }
}