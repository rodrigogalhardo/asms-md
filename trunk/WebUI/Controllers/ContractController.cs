using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class ContractController : Cruders<Contract, ContractInput>
    {
        private new readonly IContractService s;

        public ContractController(IBuilder<Contract, ContractInput> v, IContractService s) : base(s, v)
        {
            this.s = s;
        }

        public ActionResult Index(int dossierId)
        {
            var c = s.GetByDossier(dossierId);
            return c != null ? View("view", c.Id) : View(dossierId);
        }

        public ActionResult Create(int dossierId)
        {
            return View(new ContractInput{DossierId = dossierId});
        }
    }
}