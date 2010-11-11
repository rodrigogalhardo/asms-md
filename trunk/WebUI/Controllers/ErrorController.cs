using System;
using System.Web.Mvc;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(Exception error)
        {
            if (Request.IsAjaxRequest())
            {
                if (error is AsmsEx)
                    return PartialView("Expectedp", new ErrorDisplay { Message = error.Message });
                return PartialView("Errorp", new ErrorDisplay { Message = error.Message });
            }

            if (error is AsmsEx)
                return View("Expected", new ErrorDisplay { Message = error.Message });
            return View("Error");
        }

        public ActionResult HttpError404(Exception error)
        {
            return View();
        }

        public ActionResult HttpError505(Exception error)
        {
            return View();
        }
    }
}