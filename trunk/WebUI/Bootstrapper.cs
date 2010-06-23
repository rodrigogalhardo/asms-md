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
            RouteConfigurator.RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IoC.Container));
            WindsorConfigurator.Configure();
        }
    }
}