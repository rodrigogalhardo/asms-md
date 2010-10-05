using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MRGSP.ASMS.WebUI.Helpers
{
    public static class ConfirmHelpers
    {
        public static MvcHtmlString Confirm(this HtmlHelper helper, string message, string cssClass = "confirm")
        {
            return helper.Partial("confirm", new ConfirmInfo { Message = message, CssClass = cssClass });
        }
    }
}