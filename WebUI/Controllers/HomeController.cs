using System.Web.Mvc;
using log4net;
using MRGSP.ASMS.Core;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class HomeController : BaseController
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
