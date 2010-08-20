using System.Web.Mvc;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            
        }

        protected void PaintTables()
        {
            ViewData["painttables"] = true;
        }

        protected void SetError(string s)
        {
            if (s != string.Empty) ViewData["errmsg"] = s;
        }
    }
}