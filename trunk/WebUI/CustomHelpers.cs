using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MRGSP.ASMS.Infra.Dto;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.WebUI
{
    public static class MyHelpers
    {
        public static MvcHtmlString LookupFor<T,TReturn>(this HtmlHelper<T> html, Expression<Func<T, TReturn>> expression, string title = null)
        {
            return Lookup(html, ExpressionHelper.GetExpressionText(expression), title);
        }

        public static MvcHtmlString Lookup(this HtmlHelper html, string prop, string title = null, int height = 400, int width = 700, string chooseText = "Alege", string cancelText = "Anuleaza")
        {
            return html.Partial("lookup", new LookupInfo {For = prop, Title = title, Height = height, Width = width, CancelText = cancelText, ChooseText = chooseText});
        }

        public static MvcHtmlString PopupActionLink<T>(this HtmlHelper html, Expression<Action<T>> expression, string text, object htmlAttributes = null)
            where T : Controller
        {
            return
                MvcHtmlString.Create(
                string.Format("<a href='javascript:{0}' {2} >{1}</a>", PopupAction(new UrlHelper(html.ViewContext.RequestContext), expression),
                               text, "".InjectFrom<ToHtml>(htmlAttributes)));
        }

        static string GetValues<T>(Expression<Action<T>> expression)
        {
            var result = string.Empty;
            var call = expression.Body as MethodCallExpression;
            if (call == null)
            {
                throw new ArgumentException("Not a method call");
            }
            foreach (Expression argument in call.Arguments)
            {
                LambdaExpression lambda = Expression.Lambda(argument,
                                                            expression.Parameters);
                Delegate d = lambda.Compile();
                object value = d.DynamicInvoke(new object[1]);
                result += value + ",";
            }
            return result.RemoveSuffix(",");
        }

        public static MvcHtmlString PopupAction<T>(this UrlHelper url, Expression<Action<T>> expression) where T : Controller
        {
            var body = expression.Body as MethodCallExpression;
            if (body == null) return null;

            var action = body.Method.Name;

            var controller = typeof(T).Name.RemoveSuffix("Controller");
            return MvcHtmlString.Create("callDialog" + action + controller + "(" + GetValues(expression) + ")");
        }

        public static MvcHtmlString MakePopup<T>(
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

            return html.Partial("popup", new PopupInfo { Action = action, Controller = controller, Title = title, Height = height, Width = width, Parameters = parameters, RefreshOnSuccess = refresh, SaveText = "Salveaza", CancelText = "Anuleaza"});

        }

        public static MvcHtmlString Example(this HtmlHelper helper, string message)
        {
            return MvcHtmlString.Create(@"<div class='example'>" + message + "</div>");
        }

        public static MvcHtmlString Confirm(this HtmlHelper helper, string message, string cssClass = "confirm")
        {
            return helper.Partial("confirm", new ConfirmDto { Message = message, CssClass = cssClass });
        }
    }

    public class ToHtml : KnownTargetValueInjection<string>
    {
        protected override void Inject(object source, ref string target)
        {
            if (source == null) return;
            var props = source.GetProps();
            for (var i = 0; i < props.Count; i++)
            {
                target += " " + props[i].Name + "= \"" + props[i].GetValue(source) + "\"";
            }
        }
    }

    public class ConfirmDto
    {
        public string Message { get; set; }
        public string CssClass { get; set; }
    }
}
