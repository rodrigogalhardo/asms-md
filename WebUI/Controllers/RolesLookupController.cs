using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Service;
using Omu.Awesome.Mvc;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class RolesLookupController : LookupController
    {
        private readonly IUserService s;

        public RolesLookupController(IUserService s)
        {
            this.s = s;
        }

        public override ActionResult SearchForm()
        {
            return Content("");
        }

        [HttpPost]
        public ActionResult Search(IEnumerable<int> selected)
        {
            return View(@"Awesome\LookupList", s.GetRoles().Where(o => selected == null || !selected.Contains(o.Id)));
        }

        public ActionResult Selected(IEnumerable<int> ids)
        {
            return View(@"Awesome\LookupList", s.GetRoles().Where(o => ids != null && ids.Contains(o.Id)));
        }

        public ActionResult GetMultiple(IEnumerable<int> ids)
        {
            return Json(s.GetRoles().Where(o => ids.Contains(o.Id)).Select(v => new { Text = v.Name }));
        }
    }
}