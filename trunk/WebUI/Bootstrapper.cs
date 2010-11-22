using System.Web.Mvc;
using System.Web.Routing;
using MRGSP.ASMS.Infra;
using MvcContrib.Castle;
using Omu.Awesome.Mvc;

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
            //PropertyInfosStorage.RegisterActionForEachType(HyperTypeDescriptionProvider.Add);

            ModelMetadataProviders.Current = new AwesomeModelMetadataProvider();

            Settings.PopupForm.RefreshOnSuccess = true;
            Settings.Confirm.Width = 700;
            Settings.PopupForm.OkText = "OK";
            Settings.PopupForm.CancelText = "Anuleaza";
            Settings.PopupForm.ClientSideValidation = true;
        }
    }
}