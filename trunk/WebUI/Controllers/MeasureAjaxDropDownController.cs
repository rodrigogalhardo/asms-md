using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class OrganizationFormIdAjaxDropdownController : Controller
    {
        private readonly IRepo<OrganizationForm> r;

        public OrganizationFormIdAjaxDropdownController(IRepo<OrganizationForm> r)
        {
            this.r = r;
        }

        public ActionResult GetItems(int? key)
        {
            var list = new List<SelectListItem> { new SelectListItem { Text = "nu este selectat", Value = "" } };

            list.AddRange(r.GetAll().Select(o => new SelectListItem
            {
                Text = o.Name,
                Value = o.Id.ToString(),
                Selected = o.Id == key
            }));
            return Json(list);
        }
    }

    public class MeasureAjaxDropdownController : Controller
    {
        private readonly IMeasureRepo repo;

        public MeasureAjaxDropdownController(IMeasureRepo repo)
        {
            this.repo = repo;
        }

        public ActionResult GetItems(int? key)
        {
            var list = new List<SelectListItem> { new SelectListItem { Text = "nu este selectat", Value = "" } };
           
            list.AddRange(repo.GetActives().Select(o => new SelectListItem
                                                            {
                                                                Text = o.Name + " " + o.Description,
                                                                Value = o.Id.ToString(),
                                                                Selected = o.Id == key
                                                            }));
            return Json(list);
        }


    }
}