using System.Web.Mvc;
using log4net;

namespace MRGSP.ASMS.WebUI.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
