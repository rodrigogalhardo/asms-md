using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public  class FarmerVersionIdLookupController : BaseController
    {
        private readonly IFarmerInfoRepo farmerInfoRepo;
        private readonly IRepo<FarmerVersionInfo> farmerVersionInfoRepo;

        public FarmerVersionIdLookupController(IFarmerInfoRepo farmerInfoRepo, IRepo<FarmerVersionInfo> farmerVersionInfoRepo)
        {
            this.farmerInfoRepo = farmerInfoRepo;
            this.farmerVersionInfoRepo = farmerVersionInfoRepo;
        }

        [HttpPost]
        public ActionResult List(string name, string code)
        {
            if (code.Length == 13) 
            return View(farmerInfoRepo.Seek(null, code));

            if (name.Trim().Length > 2)
                return View(farmerInfoRepo.Seek(name, null));
            return View(Enumerable.Empty<FarmerInfo>());
        }

        public ActionResult Index()
        {
            return View("lookupPopup");
        }

        public ActionResult Get(int id)
        {
            return Content(farmerVersionInfoRepo.Get(id).Name);
        }
    }
}