using System.Linq;
using System.Web.Mvc;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.WebUI.Helpers
{
    public static class InputHelpers
    {
        public static MvcHtmlString Example(this HtmlHelper helper, string message)
        {
            return MvcHtmlString.Create(@"<div class='example'>" + message + "</div>");
        }

        public static MvcHtmlString Report(this HtmlHelper html, string report, params string[] ss)
        {
            var r = @"<script type='text/javascript'>function " + report + "(o){window.open('"
                    + new UrlHelper(html.ViewContext.RequestContext).Content("~\\Repor.aspx")
                    + "?report=" + report +"'";
            r = ss.Aggregate(r, (current, s) => current + ("+'&" + s + "='+o." + s));
            r += ",'newtaborsomething');}</script>";
            return MvcHtmlString.Create(r);
        }
    }

    public class ObjToStrParams : KnownTargetValueInjection<string>
    {
        protected override void Inject(object source, ref string target)
        {
            var p = source.GetProps();
            for (var i = 0; i < p.Count; i++)
                target += "&" + p[i].Name + "=" + p[i].GetValue(source);
        }
    }
}