using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class DistrictAjaxDropdownController : Controller
    {
        private readonly IRepo<District> repo;

        public DistrictAjaxDropdownController(IRepo<District> repo)
        {
            this.repo = repo;
        }

        public ActionResult GetItems(int? key)
        {
            var list = new List<SelectListItem> { new SelectListItem { Text = "not selected", Value = "" } };

            list.AddRange(repo.GetAll().Select(o => new SelectListItem
                                                        {
                                                            Text = o.Name,
                                                            Value = o.Id.ToString(),
                                                            Selected = o.Id == key
                                                        }).OrderBy(o => o.Text));
            return Json(list);
        }
    }
}