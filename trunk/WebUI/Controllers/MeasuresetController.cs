using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class MeasuresetController : Crudere<Measureset, MeasuresetInput, MeasuresetEditInput>
    {
        private new readonly IMeasuresetService s;

        public MeasuresetController(IMeasuresetService s, IBuilder<Measureset, MeasuresetInput> v, IBuilder<Measureset,MeasuresetEditInput> ve)
            : base(s, v, ve)
        {
            this.s = s;
        }

        public override ActionResult Index(int? page)
        {
            return View(s.GetDisplayPageable(page ?? 1, 5));
        }

        public ActionResult Open(int id)
        {
            return View(s.Get(id));
        }

        public ActionResult AssignedListLite(int id)
        {
            return View(s.GetAssignedMeasures(id));
        }

        public ActionResult View(int id)
        {
            return View(s.Get(id));
        }

        public ActionResult ManageMeasures(int measuresetId)
        {
            return View(s.Get(measuresetId));
        }

        public ActionResult Assigned(int measuresetId)
        {
            ViewData["msi"] = measuresetId;
            return View(s.GetAssignedMeasures(measuresetId));
        }

        public ActionResult Unassigned(int measuresetId)
        {
            ViewData["msi"] = measuresetId;
            return View(s.GetUnassignedMeasures(measuresetId));
        }

        public ActionResult Assign(int measureId, int measuresetId)
        {
            s.Assign(measureId, measuresetId);
            return RedirectToAction("ManageMeasures", new { measuresetId });
        }

        public ActionResult Unassign(int measureId, int measuresetId)
        {
            s.Unassign(measureId, measuresetId);
            return RedirectToAction("ManageMeasures", new { measuresetId });
        }

        [HttpPost]
        public ActionResult Activate(int id)
        {
            s.Activate(id);
            return RedirectToAction("open", new{id});
        }

        [HttpPost]
        public ActionResult Deactivate(int id)
        {
            s.Deactivate(id);
            return RedirectToAction("open", new{id});
        }
    }
}