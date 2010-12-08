using System.Web.Mvc;
using Omu.Awesome.Mvc;

namespace MRGSP.ASMS.WebUI.Controllers
{
    [WhiteSpaceFilter]
    public class BaseController : Controller
    {
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