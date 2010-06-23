using System.Text;
using System.Web.Mvc;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;

namespace MRGSP.ASMS.WebUI
{
    public static class MyHelpers
    {
        public static string Pagination(this HtmlHelper helper)
        {
            var c = helper.ViewContext.RouteData.Values["controller"].ToString();
            var a = helper.ViewContext.RouteData.Values["action"].ToString();
            var pageCount = (helper.ViewData.Model as IPageableInfo).PageCount;
            var pageIndex = (helper.ViewData.Model as IPageableInfo).PageIndex;
            return Pagination(helper, pageCount, pageIndex, c, a);
        }

        public static string Pagination(this HtmlHelper helper, int pageCount, int pageIndex, string controller, string action)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            var s = new StringBuilder();
            s.Append("<div class='pagination'>");
            for (var i = 0; i < pageCount; i++)
            {
                if (pageIndex != i + 1)
                    s.AppendFormat("<a href='{0}' class='ui-state-default'>{1}</a>",
                                   urlHelper.Action(action, controller, new { page = i + 1 }),
                                   i + 1);
                else
                    s.AppendFormat("<span class='ui-state-highlight current'>{0}</span>", i + 1);
            }
            s.Append("</div>");
            return s.ToString();
        }
    }
}
