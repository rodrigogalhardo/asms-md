using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class DossierController : Controller
    {
        private readonly IBuilder<Dossier, DossierCreateInput> createBuilder;
        private readonly IDossierService dossierService;
        private readonly ISystemStateServcie systemStateServcie;

        public DossierController(
            IBuilder<Dossier, DossierCreateInput> createBuilder, 
            IDossierService dossierService, 
            ISystemStateServcie systemStateServcie)
        {
            this.createBuilder = createBuilder;
            this.systemStateServcie = systemStateServcie;
            this.dossierService = dossierService;
        }

        public ActionResult Index(int? page)
        {
            return View(dossierService.GetPageable(page ?? 1, 10));
        }

        public ActionResult Open(int id)
        {
            return View(dossierService.Get(id));
        }

        public ActionResult Create()
        {
            systemStateServcie.AssureAbilityToCreateDossier();
            return View(createBuilder.BuildInput((Dossier)new Dossier().InjectFrom<FillObject>()));
        }

        [HttpPost]
        public ActionResult Create(DossierCreateInput input)
        {
            if (!ModelState.IsValid)
                return View(createBuilder.RebuildInput(input));

            var id = dossierService.Create(createBuilder.BuilEntity(input));

            return dossierService.IsNoContest(id) ? 
                RedirectToAction("Index") : 
                RedirectToAction("Index", "FillFields", new { id });
        }
    }
}