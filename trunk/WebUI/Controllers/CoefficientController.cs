using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class CoefficientController : Controller
    {
        private readonly IFieldsetService fieldsetService;
        private readonly IBuilder<Coefficient, CoefficientInput> builder;
        private readonly IRepo<Coefficient> repo;

        public CoefficientController(IFieldsetService fieldsetService, IBuilder<Coefficient, CoefficientInput> builder, IRepo<Coefficient> repo)
        {
            this.fieldsetService = fieldsetService;
            this.repo = repo;
            this.builder = builder;
        }

        public ActionResult Index(int fieldsetId)
        {
            return View(builder.BuildInput(new Coefficient { FieldsetId = fieldsetId }));
        }

        [HttpPost]
        public ActionResult Index(CoefficientInput input)
        {
            if (!ModelState.IsValid)
            {
                return View(builder.RebuildInput(input));
            }
            fieldsetService.CreateCoefficient(builder.BuilEntity(input));
            return View(builder.BuildInput(new Coefficient { FieldsetId = input.FieldsetId }));
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
        public ActionResult Delete(int coefficientId, int fieldsetId)
        {
            fieldsetService.DeleteCoefficient(coefficientId);
            return RedirectToAction("index", new { fieldsetId });
        }
    }
}