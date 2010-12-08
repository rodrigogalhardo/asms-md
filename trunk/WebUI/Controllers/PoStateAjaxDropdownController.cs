using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class PoStateAjaxDropdownController : Controller
    {
        public ActionResult GetItems(string key)
        {
            var list = new List<SelectListItem>
                           {
                               new SelectListItem{ Text = "Inregistrat", Value = "1"},
                               new SelectListItem{ Text = "In asteptare", Value = "2"},
                               new SelectListItem{ Text = "Platit", Value = "3"},
                           };

            return Json(list.Select(o => new SelectListItem { Text = o.Text, Value = o.Value, Selected = key == o.Value }));
        }
    }

    public class PoStatesAjaxDropdownController : Controller
    {
        public ActionResult GetItems(string key)
        {
            var list = new List<SelectListItem>
                           {
                               new SelectListItem{ Text = "Toate", Value= ""},
                               new SelectListItem{ Text = "Inregistrat", Value = "1"},
                               new SelectListItem{ Text = "In asteptare", Value = "2"},
                               new SelectListItem{ Text = "Platit", Value = "3"},
                           };

            return Json(list.Select(o => new SelectListItem { Text = o.Text, Value = o.Value, Selected = key == o.Value }));
        }
    }
}