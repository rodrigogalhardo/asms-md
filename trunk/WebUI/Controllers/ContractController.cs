using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class ContractController : Cruders<Contract, ContractInput>
    {
        private readonly IContractService contractService;

        public ContractController(ICrudService<Contract> s, ICreateBuilder<Contract, ContractInput> builder, IEditBuilder<Contract, ContractInput> editBuilder, IContractService contractService) : base(s, builder, editBuilder)
        {
            this.contractService = contractService;
        }

        public ActionResult Index(int dossierId)
        {
            var c = contractService.GetByDossier(dossierId);
            return c != null ? View("view", c.Id) : View(dossierId);
        }

        public ActionResult Create(int dossierId)
        {
            return View(new ContractInput{DossierId = dossierId});
        }
    }
}