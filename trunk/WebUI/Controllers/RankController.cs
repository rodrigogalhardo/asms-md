using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra.Dto;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class RankController : BaseController
    {
        private readonly IFpiService fpiService;
        private readonly IDossierService dossierService;
        private readonly ICompetitorRepo competitorRepo;

        public RankController(IFpiService fpiService, IDossierService dossierService, ICompetitorRepo competitorRepo)
        {
            this.fpiService = fpiService;
            this.dossierService = dossierService;
            this.competitorRepo = competitorRepo;
        }

        public ActionResult ChangeAmount(int fpiId)
        {
            var fpi = fpiService.Get(fpiId);
            return View(new ChangeAmountInput().InjectFrom(fpi));
        }

        [HttpPost]
        public ActionResult ChangeAmount(ChangeAmountInput input)
        {
            if (!ModelState.IsValid)
                return View(input);
            try
            {
                fpiService.ChangeAmount(input.Id, input.Amount);
            }
            catch(AsmsEx e)
            {
                ModelState.AddModelError("amount", e.Message);
                return View(input);
            }
            return Content("ok");
        }

        public ActionResult Index(int fpiId)
        {
            var fpi = fpiService.Get(fpiId);
            return View(fpi);
        }

        [HttpPost]
        public ActionResult Recalculate(int fpiId)
        {
            dossierService.Recalculate(fpiId);
            return RedirectToAction("index", new { fpiId });
        }

        public ActionResult Authorized(int fpiId)
        {
            return View("comp", competitorRepo.GetWhere(new { fpiId, StateId = DossierStates.Authorized, Disqualified = false }));
        }

        public ActionResult Winners(int fpiId)
        {
            return View("comp", competitorRepo
                .GetWhere(new { fpiId, StateId = DossierStates.Winner, Disqualified = false })
                .OrderByDescending(o => o.Value));
        }

        public ActionResult Losers(int fpiId)
        {
            return View("comp", competitorRepo.Losers(fpiId)
                .OrderByDescending(o => o.Value));
        }

        public ActionResult Disqualified(int fpiId)
        {
            return View("comp", competitorRepo.GetWhere(new { fpiId, Disqualified = true }));
        }
    }
}