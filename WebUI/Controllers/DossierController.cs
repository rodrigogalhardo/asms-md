using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class DossierController : BaseController
    {
        private readonly IBuilder<Dossier, DossierCreateInput> createBuilder;
        private readonly IDossierService dossierService;
        private readonly ISystemStateServcie systemStateServcie;
        private IRepo<DossierInfo> dir;

        public DossierController(
            IBuilder<Dossier, DossierCreateInput> createBuilder,
            IDossierService dossierService,
            ISystemStateServcie systemStateServcie, IRepo<DossierInfo> dir)
        {
            this.createBuilder = createBuilder;
            this.dir = dir;
            this.systemStateServcie = systemStateServcie;
            this.dossierService = dossierService;
        }

        public ActionResult Disqualify(int dossierId)
        {
            return View(new DisqualifyInput { DossierId = dossierId });
        }

        [HttpPost]
        public ActionResult Disqualify(DisqualifyInput input)
        {
            dossierService.Disqualify(input.DossierId, input.Reason);
            return RedirectToAction("Open", new { id = input.DossierId });
        }

        public ActionResult Index(int? page)
        {
            return View(dir.GetPageable(page ?? 1, 10));
        }

        public ActionResult Open(int id)
        {
            return View(dir.Get(id));
        }

        public ActionResult Create()
        {
            systemStateServcie.AssureAbilityToCreateDossier();
            return View(createBuilder.BuildInput(new Dossier()));
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