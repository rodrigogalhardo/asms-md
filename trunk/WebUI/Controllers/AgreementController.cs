using System.Web.Mvc;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class AgreementController : Cruders<Agreement, AgreementInput>
    {
        private new readonly IAgreementService s;
        public AgreementController(IAgreementService s, IBuilder<Agreement, AgreementInput> v) : base(s, v)
        {
            this.s = s;
        }

        public ActionResult Create(int contractId)
        {
            return View(v.BuildInput(new Agreement {ContractId = contractId}));
        }

        public ActionResult ForContract(int contractId)
        {
            ViewData["contractId"] = contractId;
            return View(s.GetByContractId(contractId));
        }
    }
}