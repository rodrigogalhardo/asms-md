using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.WebUI.Helpers
{
    public static class PopupHelpers
    {
        public static MvcHtmlString PopupFormActionLink<T>(this HtmlHelper html, Expression<Action<T>> expression, string text, object htmlAttributes = null)
            where T : Controller
        {
            return
                MvcHtmlString.Create(
                    string.Format("<a href='javascript:{0}' {2} >{1}</a>", PopupFormAction(new UrlHelper(html.ViewContext.RequestContext), expression),
                                  text, "".InjectFrom<ToHtml>(htmlAttributes)));
        }

        public static MvcHtmlString PopupFormAction<T>(this UrlHelper url, Expression<Action<T>> expression) where T : Controller
        {
            var body = expression.Body as MethodCallExpression;
            if (body == null) return null;

            var action = body.Method.Name;

            var controller = typeof(T).Name.RemoveSuffix("Controller");
            return MvcHtmlString.Create("callDialog" + action + controller + "(" + expression.GetValues() + ")");
        }

        public static MvcHtmlString MakePopupForm<T>(
            this HtmlHelper html,
            Expression<Action<T>> expression,
            string title = null,
            int height = 300,
            int width = 700,
            bool refresh = true
            ) where T : Controller
        {
            var action = ((MethodCallExpression)expression.Body).Method.Name;
            var parameters = ((MethodCallExpression)expression.Body).Method.GetParameters().Select(o => o.Name).ToArray();
            var controller = typeof(T).Name.RemoveSuffix("Controller");

            return html.Partial("popup", new PopupInfo { Action = action, Controller = controller, Title = title, Height = height, Width = width, Parameters = parameters, RefreshOnSuccess = refresh, SaveText = "Salveaza", CancelText = "Anuleaza" });

        }
    }
}