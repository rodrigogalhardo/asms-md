using System.Web.Mvc;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            PaintTables(true);
        }

        protected void PaintTables(bool doit)
        {
            ViewData["painttables"] = doit;
        }

        protected void SetError(string s)
        {
            if (s != string.Empty) ViewData["errmsg"] = s;
        }
    }
}