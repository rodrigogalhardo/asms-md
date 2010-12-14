using System;
using System.Web.Mvc;
using System.Web.Routing;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.WebUI.Controllers;
using MvcContrib.Castle;
using Omu.Awesome.Mvc;

namespace MRGSP.ASMS.WebUI
{
    public class Bootstrapper
    {
        public static void Bootstrap()
        {
            ModelBinders.Binders.Add(typeof(DateTime), new CurrentCultureDateTimeBinder());
            log4net.Config.XmlConfigurator.Configure();
            RouteConfigurator.RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IoC.Container));
            WindsorConfigurator.Configure();

            ModelMetadataProviders.Current = new AwesomeModelMetadataProvider();

            Settings.PopupForm.RefreshOnSuccess = true;
            Settings.Confirm.Width = 600;
            Settings.Confirm.YesText = "Da";
            Settings.Confirm.NoText = "Nu, nu sunt sigur";

            Settings.PopupForm.OkText = "OK";
            Settings.PopupForm.CancelText = "Anuleaza";
            Settings.PopupForm.ClientSideValidation = true;
            Settings.Confirm.Title = "Sunteti sigur ?";
        }
    }
}