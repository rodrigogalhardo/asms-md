using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Helpers
{
    public static class LookupHelpers
    {
        public static MvcHtmlString LookupFor<T,TReturn>(this HtmlHelper<T> html, Expression<Func<T, TReturn>> expression, object value = null, string title = null)
        {
            return Lookup(html, ExpressionHelper.GetExpressionText(expression), value, title);
        }

        public static MvcHtmlString Lookup(this HtmlHelper html, string prop, object value = null, string title = null, int height = 400, int width = 700, string chooseText = "Alege", string cancelText = "Anuleaza")
        {
            return html.Partial("lookup", new LookupInfo {For = prop, Title = title, Height = height, Width = width, CancelText = cancelText, ChooseText = chooseText, Value = value});
        }
    }
}
