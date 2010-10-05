using System.Web.Mvc;

namespace MRGSP.ASMS.WebUI.Helpers
{
    public static class InputHelpers
    {
        public static MvcHtmlString Example(this HtmlHelper helper, string message)
        {
            return MvcHtmlString.Create(@"<div class='example'>" + message + "</div>");
        }
    }
}