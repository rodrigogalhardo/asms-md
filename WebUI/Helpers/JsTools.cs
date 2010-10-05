using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MRGSP.ASMS.WebUI.Helpers
{
    public static class JsTools
    {
        #region Public Methods


        public static string Call<T>(Expression<Action<T>> expression) where T : Controller
        {
            var action = ((MethodCallExpression)expression.Body).Method.Name;
            return string.Format("{0}();", action);
        }

        /// <returns>
        /// something like id, id1, id2
        /// </returns>
        public static string MakeParameters(string[] parameters)
        {
            var result = string.Empty;
            var i = 0;
            foreach (var param in parameters)
            {
                result += param;
                i++;
                if (i != parameters.Count())
                {
                    result += ",";
                }
            }

            return result;
        }
       
        /// <returns>
        /// , { key1 : value1, key2 : value2 } or string.Empty
        /// </returns>
        public static string JsonParam(string[] parameters)
        {
            if (parameters.Length == 0)
            {
                return string.Empty;
            }

            var result = " { ";
            var i = 0;
            foreach (var param in parameters)
            {
                result += string.Format("{0} : {1}", param, param);
                i++;
                if (i != parameters.Count())
                {
                    result += ", ";
                }
            }

            result += " }, ";

            return result;
        }

        #endregion
    }
}