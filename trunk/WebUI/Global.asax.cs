using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace MRGSP.ASMS.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrapper.Bootstrap();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Uses WebForms code to apply "auto" culture to current thread and deal with
            // invalid culture requests automatically. Defaults to en-US when not specified.
            using (var fakePage = new Page())
            {
                var ignored = fakePage.Server; // Work around a WebForms quirk
                fakePage.Culture = "auto:ro-RO"; // Apply local formatting to this thread
                fakePage.UICulture = "auto:ro-RO"; // Apply local language to this thread
            }
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.User == null) return;
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return;
            if (!(HttpContext.Current.User.Identity is FormsIdentity)) return;

            var id = HttpContext.Current.User.Identity as FormsIdentity;
            var ticket = id.Ticket;
            var userData = ticket.UserData;
            var roles = userData.Split(new[] { ',' });

            HttpContext.Current.User = new GenericPrincipal(id, roles);
        }
    }
}