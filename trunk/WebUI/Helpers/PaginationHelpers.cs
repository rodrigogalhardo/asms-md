using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace MRGSP.ASMS.WebUI.Helpers
{
    public static class PaginationHelpers
    {
        
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

        private static string RenderAjaxButton(int i, string func)
        {
            return string.Format("<a href='javascript:{0}({1})' class='ui-state-default'>{2}</a>",
                                 func, i, i);
        }
    }

    internal static class Extensionsz
    {
        public static string RemovePrefix(this string o, string prefix)
        {
            if (prefix == null) return o;
            return !o.StartsWith(prefix) ? o : o.Remove(0, prefix.Length);
        }

        public static string RemoveSuffix(this string o, string suffix)
        {
            if (suffix == null) return o;
            return !o.EndsWith(suffix) ? o : o.Remove(o.Length - suffix.Length, suffix.Length);
        }

        public static IEnumerable<object> GetValues<T>(this Expression<Action<T>> expression)
        {
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
                yield return value;
            }
        }

        public static string GetHtml(this object source)
        {
            var target = "";
            if (source == null) return target;
            var props = TypeDescriptor.GetProperties(source);
            for (var i = 0; i < props.Count; i++)
            {
                target += " " + props[i].Name + "= \"" + props[i].GetValue(source) + "\"";
            }
            return target;
        }

        public static void Add(this ModelMetadata data, bool condition, string key, object value)
        {
            if (condition) data.AdditionalValues.Add(key, value);
        }
    }
}