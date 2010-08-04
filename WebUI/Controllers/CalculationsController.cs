using System.Web.Mvc;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class CalculationsController : BaseController
    {
        private readonly IDossierService ds;

        public CalculationsController(IDossierService ds)
        {
            this.ds = ds;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Calculate()
        {
            ds.GoCoefficients();
            return View("success");
        }

    }
}