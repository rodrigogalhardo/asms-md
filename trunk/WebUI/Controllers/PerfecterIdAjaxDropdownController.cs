using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class PerfecterIdAjaxDropdownController : Controller
    {
        private readonly IRepo<Perfecter> repo;

        public PerfecterIdAjaxDropdownController(IRepo<Perfecter> repo)
        {
            this.repo = repo;
        }

        public ActionResult GetItems(int? key)
        {
            var list = new List<SelectListItem> { new SelectListItem { Text = "nu este selectat", Value = "" } };

            list.AddRange(repo.GetAll().Select(o => new SelectListItem
                                                        {
                                                            Text = o.Name,
                                                            Value = o.Id.ToString(),
                                                            Selected = o.Id == key
                                                        }));
            return Json(list);
        }
    }
}