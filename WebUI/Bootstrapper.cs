using System;
using System.Web.Mvc;
using System.Web.Routing;
using MRGSP.ASMS.Infra;
using MvcContrib.Castle;

namespace MRGSP.ASMS.WebUI
{
    public class Bootstrapper
    {
        public static void Bootstrap()
        {
            log4net.Config.XmlConfigurator.Configure();
            RouteConfigurator.RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IoC.Container));
            WindsorConfigurator.Configure();
        }
    }

    public static class Extensions
    {
        public static string Display(this DateTime? d)
        {
            return !d.HasValue ? string.Empty : d.Value.ToShortDateString();
        }

        public static string Display(this DateTime d)
        {
            return d.ToShortDateString();
        }
    }
}