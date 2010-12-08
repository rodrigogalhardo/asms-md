using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class RankController : BaseController
    {
        private readonly IFpiService fpiService;
        private readonly ICompetitorRepo competitorRepo;

        public RankController(IFpiService fpiService, IDossierService dossierService, ICompetitorRepo competitorRepo)
        {
            this.fpiService = fpiService;
            this.competitorRepo = competitorRepo;
        }

        public ActionResult Index(int fpiId)
        {
            var fpi = fpiService.Get(fpiId);
            return View(fpi);
        }

        [HttpPost]
        public ActionResult Recalculate(int fpiId)
        {
            fpiService.Recalculate(fpiId);
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