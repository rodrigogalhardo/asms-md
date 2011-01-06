using System;
using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using Omu.Awesome.Mvc;

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
        public ActionResult Search(string search)
        {
            //TODO: optimize
            return View(@"Awesome\LookupList", repo.GetAll().Where(o => o.Name.StartsWith(search, StringComparison.InvariantCultureIgnoreCase)));
        }

        public ActionResult Get(int id)
        {
            var o = repo.Get(id);
            return Content(o != null ? o.Name : "");
        }
    }
}