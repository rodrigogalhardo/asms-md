using System.Text;
using System.Web.Mvc;
using MRGSP.ASMS.Core;

namespace MRGSP.ASMS.WebUI
{
    public static class PaginationHelpers
    {
        public static MvcHtmlString Pagination(this HtmlHelper helper)
        {
            var c = helper.ViewContext.RouteData.Values["controller"].ToString();
            var a = helper.ViewContext.RouteData.Values["action"].ToString();
            var pageCount = (helper.ViewData.Model as IPageableInfo).PageCount;
            var pageIndex = (helper.ViewData.Model as IPageableInfo).PageIndex;
            return Pagination(helper, pageCount, pageIndex, c, a);
        }

        public static MvcHtmlString Pagination(this HtmlHelper helper, int pageCount, int pageIndex, string controller, string action)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            var sb = new StringBuilder();
            sb.Append("<div class='pagination'>");

            if (pageCount < 8)
                sb.Append(RenderButtons(1, pageCount, pageIndex, urlHelper, controller, action));
            else if (pageIndex < 5)
                sb.AppendFormat("{0} ... {1}", RenderButtons(1, 5, pageIndex, urlHelper, controller, action), RenderButton(pageCount, pageIndex, urlHelper, controller, action));
            else if (pageIndex > pageCount - 5)
                sb.AppendFormat("{0} ... {1}", RenderButton(1, pageIndex, urlHelper, controller, action),
                                RenderButtons(pageCount - 5, pageCount, pageIndex, urlHelper, controller, action));
            else
                sb.AppendFormat("{0} ... {1} ... {2}",
                                RenderButton(1, pageIndex, urlHelper, controller, action),
                                RenderButtons(pageIndex - 2, pageIndex + 2, pageIndex, urlHelper, controller, action),
                                RenderButton(pageCount, pageIndex, urlHelper, controller, action));

            sb.Append("</div>");

            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString AjaxPagination(this HtmlHelper htmlHelper, int pageCount, int pageIndex, string func)
        {
            var sb = new StringBuilder();

            sb.Append("<div class='pagination'>");

            if (pageCount < 8)
                sb.Append(RenderAjaxButtons(1, pageCount, pageIndex, func));
            else if (pageIndex < 5)
                sb.AppendFormat("{0} ... {1}", RenderAjaxButtons(1, 5, pageIndex, func), RenderAjaxButton(pageCount, func));
            else if (pageIndex > pageCount - 5)
                sb.AppendFormat("{0} ... {1}", RenderAjaxButton(1, func),
                                RenderAjaxButtons(pageCount - 5, pageCount, pageIndex, func));
            else
                sb.AppendFormat("{0} ... {1} ... {2}",
                                RenderAjaxButton(1, func),
                                RenderAjaxButtons(pageIndex - 2, pageIndex + 2, pageIndex, func),
                                RenderAjaxButton(pageCount, func));

            sb.Append("</div>");

            return MvcHtmlString.Create(sb.ToString());
        }

        private static string RenderAjaxButtons(int from, int to, int index, string func)
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

        private static string RenderButtons(int from, int to, int index, UrlHelper urlHelper, string controller, string action)
        {
            var s = new StringBuilder();
            for (var i = from; i <= to; i++)
            {
                s.Append(RenderButton(i, index, urlHelper, controller, action));
            }
            return s.ToString();
        }

        private static string RenderButton(int number, int index, UrlHelper urlHelper, string controller, string action)
        {
            if (index != number)
                return string.Format("<a href='{0}'>{1}</a>",
                                     urlHelper.Action(action, controller, new { page = number }), number);

            return string.Format("<span class='current'>{0}</span>", number);
        }

        private static string RenderAjaxButton(int i, string func)
        {
            return string.Format("<a href='javascript:{0}({1})' class='ui-state-default'>{2}</a>",
                                 func, i, i);
        }
    }
}