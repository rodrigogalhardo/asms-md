using System.Web.Mvc;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View("Error");
        }
    }
}