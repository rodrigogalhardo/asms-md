using System;
using System.Linq.Expressions;

namespace MRGSP.ASMS.WebUI.Helpers
{
    public static class Extensions
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
        public static string Display(this DateTime? d)
        {
            return !d.HasValue ? string.Empty : d.Value.ToShortDateString();
        }

        public static string Display(this DateTime d)
        {
            return d.ToShortDateString();
        }

        public static string Display(this Decimal d)
        {
            return d.ToString("N2");
        }

        public static string GetValues<T>(this Expression<Action<T>> expression)
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
    }
}