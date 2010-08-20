using System.Web.Mvc;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class ContactInfoController : BaseController
    {
        public ActionResult Index(int farmerId)
        {
            ViewData["id"] = farmerId;
            return View();
        }
    }
}