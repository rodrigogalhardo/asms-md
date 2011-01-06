using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using Omu.Awesome.Mvc;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FarmerVersionIdLookupController : LookupController
    {
        private readonly IFarmerInfoRepo farmerInfoRepo;
        private readonly IRepo<FarmerVersionInfo> farmerVersionInfoRepo;

        public FarmerVersionIdLookupController(IFarmerInfoRepo farmerInfoRepo, IRepo<FarmerVersionInfo> farmerVersionInfoRepo)
        {
            this.farmerInfoRepo = farmerInfoRepo;
            this.farmerVersionInfoRepo = farmerVersionInfoRepo;
        }

        public override ActionResult SearchForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string name, string code)
        {
            if (code.Length == 13)
                return View(@"Awesome\LookupList", farmerInfoRepo.Seek(null, code));

            if (name.Trim().Length > 2)
                return View(@"Awesome\LookupList", farmerInfoRepo.Seek(name, null));
            return View(@"Awesome\LookupList", Enumerable.Empty<FarmerInfo>());
        }

        public ActionResult Get(int id)
        {
            return Content(id == 0 ? "" : farmerVersionInfoRepo.Get(id).Name);
        }
    }
}