using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public  class FarmerLookupController : BaseController
    {
        private readonly IFarmerInfoRepo r;

        public FarmerLookupController(IFarmerInfoRepo r)
        {
            this.r = r;
        }

        [HttpPost]
        public ActionResult Page(string name, string code)
        {
            if (code.Length == 13) 
            return View(r.Seek(null, code));

            if (name.Trim().Length > 4)
                return View(r.Seek(name, null));
            return View(Enumerable.Empty<FarmerInfo>());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Get(int id)
        {
            return Content(r.Get(id).Name);
        }
    }
}