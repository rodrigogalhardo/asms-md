using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class MeasuresetController : Cruder<Measureset, MeasuresetInput>
    {
        private readonly IMeasuresetService service;
        public MeasuresetController(IRepo<Measureset> repo, IBuilder<Measureset, MeasuresetInput> builder, IMeasuresetService service)
            : base(repo, builder)
        {
            this.service = service;
        }

        public override ActionResult Index(int? page)
        {
            return View(service.GetPageable(page ?? 1, 5));
        }

        public ActionResult Open(int id)
        {
            return View(repo.Get(id));
        }

        public ActionResult AssignedListLite(int id)
        {
            return View(service.GetAssignedMeasures(id));
        }

        public ActionResult View(int id)
        {
            return View(repo.Get(id));
        }

        public ActionResult ManageMeasures(int measuresetId)
        {
            return View(repo.Get(measuresetId));
        }

        public ActionResult Assigned(int measuresetId)
        {
            ViewData["msi"] = measuresetId;
            return View(service.GetAssignedMeasures(measuresetId));
        }

        public ActionResult Unassigned(int measuresetId)
        {
            ViewData["msi"] = measuresetId;
            return View(service.GetUnassignedMeasures(measuresetId));
        }

        public ActionResult Assign(int measureId, int measuresetId)
        {
            service.Assign(measureId, measuresetId);
            return RedirectToAction("ManageMeasures", new { measuresetId });
        }

        public ActionResult Unassign(int measureId, int measuresetId)
        {
            service.Unassign(measureId, measuresetId);
            return RedirectToAction("ManageMeasures", new { measuresetId });
        }

        [HttpPost]
        public ActionResult Activate(int id)
        {
            service.Activate(id);
            return RedirectToAction("open", new{id});
        }

        [HttpPost]
        public ActionResult Deactivate(int id)
        {
            service.Deactivate(id);
            return RedirectToAction("open", new{id});
        }
    }
}