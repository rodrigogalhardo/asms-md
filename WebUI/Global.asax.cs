using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;
using log4net;
using MRGSP.ASMS.WebUI.Controllers;

namespace MRGSP.ASMS.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        private readonly ILog log = LogManager.GetLogger(string.Empty);

        protected void Application_Start()
        {
            Bootstrapper.Bootstrap();
            DefaultModelBinder.ResourceClassKey = "Messages";

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Uses WebForms code to apply "auto" culture to current thread and deal with
            // invalid culture requests automatically. Defaults to en-US when not specified.
            using (var fakePage = new Page())
            {
                var ignored = fakePage.Server; // Work around a WebForms quirk
                fakePage.Culture = "ro-RO"; // Apply local formatting to this thread
                fakePage.UICulture = "ro-RO"; // Apply local language to this thread
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

        protected void Application_Error(object sender, EventArgs e)
        {
            return;
            var exception = Server.GetLastError();
            // Log the exception.
            log.Error(exception.Message, exception);
            Response.Clear();

            var httpException = exception as HttpException;

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Error");

            if (httpException == null)
            {
                routeData.Values.Add("action", "Index");
            }
            else //It's an Http Exception, Let's handle it.
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // Page not found.
                        routeData.Values.Add("action", "HttpError404");
                        break;
                    case 505:
                        // Server error.
                        routeData.Values.Add("action", "HttpError505");
                        break;

                        // Here you can handle Views to other error codes.
                        // I choose a General error template  
                    default:
                        routeData.Values.Add("action", "Index");
                        break;
                }
            }

            // Pass exception details to the target error View.
            routeData.Values.Add("error", exception);

            // Clear the error on server.
            Server.ClearError();

            // Call target Controller and pass the routeData.
            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(
                                        new HttpContextWrapper(Context), routeData));
        }
    }
}