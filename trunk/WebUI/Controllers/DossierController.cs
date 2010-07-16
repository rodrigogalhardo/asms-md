using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class DossierController : Controller
    {
        private readonly IBuilder<Dossier, DossierCreateInput> createBuilder;

        public DossierController(IBuilder<Dossier, DossierCreateInput> createBuilder)
        {
            this.createBuilder = createBuilder;
        }

        public ActionResult Create()
        {
            return View(createBuilder.BuildInput(new Dossier()));
        }

        [HttpPost]
        public ActionResult Create(DossierCreateInput input)
        {
            if (!ModelState.IsValid)
            {
                return View(createBuilder.RebuildInput(input));
            }
            return Content("asdfa");
        }
    }

    public static class ModelStateExtensions
    {
        public static IEnumerable<ModelError> GetErrors(this ModelStateDictionary state)
        {
            return state.Values.SelectMany(o => o.Errors);
        }
    }
}