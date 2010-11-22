using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FpiController : BaseController
    {
        private readonly IRepo<Fpi> repo;
        private readonly IMeasuresetService mss;
        private readonly IBuilder<Fpi, FpiInput> v;
        public FpiController(IRepo<Fpi> repo, IMeasuresetService mss, IBuilder<Fpi, FpiInput> v)
        {
            this.repo = repo;
            this.v = v;
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
            return View(v.BuildInput(new Fpi { MeasureId = measureId, MeasuresetId = measuresetId, Month = month }));
        }

        [HttpPost]
        public ActionResult Create(FpiInput input)
        {
            if (!ModelState.IsValid) return View(v.RebuildInput(input));
            repo.InsertNoIdentity(v.BuildEntity(input));
            return Content("ok");
        }
    }
}