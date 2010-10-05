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
        private readonly IRepo<DossierInfo> dossierInfoRepo;
        private readonly IRepo<FieldValueInfo> fieldValueInfoRepo;
        private readonly IRepo<IndicatorValueInfo> indicatorValueInfoRepo;
        private readonly IRepo<CoefficientValueInfo> coefficientValueInfoRepo;

        public DossierController(
            IBuilder<Dossier, DossierCreateInput> createBuilder,
            IDossierService dossierService,
            ISystemStateServcie systemStateServcie,
            IRepo<DossierInfo> dossierInfoRepo, IRepo<FieldValueInfo> fieldValueInfoRepo, IRepo<IndicatorValueInfo> indicatorValueInfoRepo, IRepo<CoefficientValueInfo> coefficientValueInfoRepo)
        {
            this.createBuilder = createBuilder;
            this.coefficientValueInfoRepo = coefficientValueInfoRepo;
            this.indicatorValueInfoRepo = indicatorValueInfoRepo;
            this.fieldValueInfoRepo = fieldValueInfoRepo;
            this.dossierInfoRepo = dossierInfoRepo;
            this.systemStateServcie = systemStateServcie;
            this.dossierService = dossierService;
        }

        public ActionResult Values(int id)
        {
            return View(dossierService.Get(id));
        }

        public ActionResult FieldValues(int id)
        {
            return View("calcItems",fieldValueInfoRepo.GetWhere(new {dossierId = id}));
        }
        
        public ActionResult IndicatorValues(int id)
        {
            return View("calcItems", indicatorValueInfoRepo.GetWhere(new { dossierId = id}));
        }
        
        public ActionResult CoefficientValues(int id)
        {
            return View(coefficientValueInfoRepo.GetWhere(new { dossierId = id}));
        }

        public ActionResult Disqualify(int dossierId)
        {
            return View(new DisqualifyInput { DossierId = dossierId });
        }

        [HttpPost]
        public ActionResult Disqualify(DisqualifyInput input)
        {
            if (!ModelState.IsValid) return View(input);
            dossierService.Disqualify(input.DossierId, input.Reason);
            return Content("ok");
        }

        public ActionResult Index(int? page)
        {
            return View(dossierInfoRepo.GetPageable(page ?? 1, 10));
        }

        public ActionResult Open(int id)
        {
            return View(dossierInfoRepo.Get(id));
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

            var id = dossierService.Create(createBuilder.BuildEntity(input));

            return dossierService.IsNoContest(id) ?
                RedirectToAction("Index") :
                RedirectToAction("Index", "FillFields", new { id });
        }

        [HttpPost]
        public ActionResult Authorize(int id)
        {
            dossierService.Authorize(id);
            return RedirectToAction("open", new { id });
        }
        
        public ActionResult ChangeAmountPayed(int id)
        {
            return View(new ChangeAmountPayedInput { Amount = dossierService.Get(id).AmountPayed, Id = id });
        }

        [HttpPost]
        public ActionResult ChangeAmountPayed(ChangeAmountPayedInput input)
        {
            if (!ModelState.IsValid) return View(input);

            dossierService.ChangeAmountPayed(input.Id, input.Amount);
            return Content("ok");
        }

    }
}