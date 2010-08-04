using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra.Dto;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FpiController : BaseController
    {
        private readonly IRepo<Fpi> repo;
        private readonly IMeasuresetService mss;

        public FpiController(IRepo<Fpi> repo, IMeasuresetService mss)
        {
            this.repo = repo;
            this.mss = mss;
        }

        public ActionResult Index(int measuresetId)
        {
            ViewData["measuresetId"] = measuresetId;
            ViewData["measures"] = mss.GetAssignedMeasures(measuresetId);
            return View(repo.GetWhere(new { measuresetId }));
        }

        public ActionResult Create(int measuresetId, int measureId, int month)
        {
            return View(new FpiInput { MeasureId = measureId, MeasuresetId = measuresetId, Month = month });
        }

        [HttpPost]
        public ActionResult Create(FpiInput input)
        {
            if (!ModelState.IsValid) return View(input);
            repo.InsertNoIdentity((Fpi) new Fpi().InjectFrom(input));
            return RedirectToAction("index", new{input.MeasuresetId});
        }
    }
}