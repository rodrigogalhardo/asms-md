using System.Web.Mvc;
using System.Web.Routing;
using Hyper.ComponentModel;
using MRGSP.ASMS.Infra;
using MvcContrib.Castle;
using Omu.ValueInjecter;

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
            PropertyInfosStorage.RegisterActionForEachType(HyperTypeDescriptionProvider.Add);
        }
    }
}