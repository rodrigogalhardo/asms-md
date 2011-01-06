using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;
using Omu.Awesome.Mvc;

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

        public override ActionResult Search(int page = 1, int ps = 5)
        {
            var src = s.GetDisplayPageable(page, ps).Page;

            var rows = this.RenderView("rows", src);

            return Json(new { rows, more = s.Count() > page * ps });
        }

        public override ActionResult Row(int id)
        {
            return View("rows", new[] { s.GetDisplay(id) });
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