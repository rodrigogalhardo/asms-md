using System;
using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using Omu.Awesome.Mvc;
using Omu.Awesome.Mvc.Helpers;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class DistrictIdLookupController : LookupController
    {
        private readonly IRepo<District> repo;

        public DistrictIdLookupController(IRepo<District> repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public ActionResult LookupList(string search)
        {
            ViewData["structure"] = new LookupListInfo
            {
                Key = "Id",
                Columns = new[] { "Name", "Code" }
            };
            return View(@"Awesome\LookupList", repo.GetAll().Where(o => o.Name.StartsWith(search, StringComparison.InvariantCultureIgnoreCase)));
        }

        public ActionResult Get(int id)
        {
            var o = repo.Get(id);
            return Content(o != null ? o.Name : "");
        }
    }
}