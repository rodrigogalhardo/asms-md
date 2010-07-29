using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class IndicatorController : Controller
    {
        private readonly IFieldsetService fieldsetService;
        private readonly IBuilder<Indicator, IndicatorInput> builder;
        private readonly IRepo<Indicator> repo;

        public IndicatorController(IFieldsetService fieldsetService, IBuilder<Indicator, IndicatorInput> builder, IRepo<Indicator> repo)
        {
            this.fieldsetService = fieldsetService;
            this.repo = repo;
            this.builder = builder;
        }

        public ActionResult Index(int fieldsetId)
        {
            return View(builder.BuildInput(new Indicator(){FieldsetId = fieldsetId}));
        }

        [HttpPost]
        public ActionResult Index(IndicatorInput input)
        {   
            if (!ModelState.IsValid)
            {
                return View(builder.RebuildInput(input));
            }
            fieldsetService.CreateIndicator(builder.BuilEntity(input));
            return View(builder.BuildInput(new Indicator {FieldsetId = input.FieldsetId}));
        }

        public ActionResult List(int fieldsetId)
        {
            return View(repo.GetWhere(new { fieldsetId }));
        }

        public ActionResult ListLite(int fieldsetId)
        {
            return View(repo.GetWhere(new {fieldsetId}));
        }

        [HttpPost]
        public ActionResult Delete(int indicatorId, int fieldsetId)
        {
            fieldsetService.DeleteIndicator(indicatorId);
            return RedirectToAction("index", new {fieldsetId});
        }
    }
}