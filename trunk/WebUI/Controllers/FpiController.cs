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
        private readonly IDossierService dss;

        public FpiController(IRepo<Fpi> repo, IMeasuresetService mss, IDossierService dss)
        {
            this.repo = repo;
            this.dss = dss;
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

        public ActionResult Rank(int fpiId)
        {
            PaintTables();
            var fpi = repo.Get(fpiId);
            
            var msg = "clasament pe luna " + fpi.Month + (fpi.Calculated ? " calculat" : "necalculat");
            ViewData["msg"] = msg;
            dss.Rank(fpi.MeasuresetId, fpi.MeasureId, fpi.Month);
            return View(dss.GetForTop(fpi.MeasuresetId, fpi.MeasureId, fpi.Month));
        }
    }
}