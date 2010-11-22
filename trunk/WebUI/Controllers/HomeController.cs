using System.Web.Mvc;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IMeasuresetService measuresetService;

        public HomeController(IMeasuresetService measuresetService)
        {
            this.measuresetService = measuresetService;
        }

        public ActionResult Index()
        {
            var ms = measuresetService.GetActive();
            if (ms != null) ViewData["msid"] = ms.Id;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        
    }
}
