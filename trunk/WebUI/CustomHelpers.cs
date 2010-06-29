using System.Text;
using System.Web;
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

        public static MvcHtmlString AjaxPagination(this HtmlHelper htmlHelper, int pageCount, int pageIndex, string func)
        {
            var sb = new StringBuilder();

            sb.Append("<div class='pagination'>");

            if (pageCount < 8)
                sb.Append(RenderButtons(1, pageCount, pageIndex, func));
            else if (pageIndex < 5)
                sb.AppendFormat("{0} ... {1}", RenderButtons(1, 5, pageIndex, func), RenderButton(pageCount, func));
            else if (pageIndex > pageCount - 5)
                sb.AppendFormat("{0} ... {1}", RenderButton(1, func),
                                RenderButtons(pageCount - 5, pageCount, pageIndex, func));
            else
                sb.AppendFormat("{0} ... {1} ... {2}",
                                RenderButton(1, func),
                                RenderButtons(pageIndex - 2, pageIndex + 2, pageIndex, func),
                                RenderButton(pageCount, func));

            sb.Append("</div>");

            return MvcHtmlString.Create(sb.ToString());
        }

        private static string RenderButtons(int from, int to, int index, string func)
        {
            var s = new StringBuilder();
            for (var i = from; i <= to; i++)
            {
                if (index != i)
                    s.AppendFormat("<a href='javascript:{0}({1})' class='ui-state-default'>{2}</a>",
                                   func, i, i);
                else
                    s.AppendFormat("<span class='ui-state-highlight current'>{0}</span>", i);
            }
            return s.ToString();
        }

        private static string RenderButton(int i, string func)
        {
            return string.Format("<a href='javascript:{0}({1})' class='ui-state-default'>{2}</a>",
                                      func, i, i);
        }
    }
}
