using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public  class FarmerVersionLookupController : BaseController
    {
        private readonly IFarmerInfoRepo farmerInfoRepo;
        private readonly IRepo<FarmerVersionInfo> farmerVersionInfoRepo;

        public FarmerVersionLookupController(IFarmerInfoRepo farmerInfoRepo, IRepo<FarmerVersionInfo> farmerVersionInfoRepo)
        {
            this.farmerInfoRepo = farmerInfoRepo;
            this.farmerVersionInfoRepo = farmerVersionInfoRepo;
        }

        [HttpPost]
        public ActionResult Page(string name, string code)
        {
            if (code.Length == 13) 
            return View(farmerInfoRepo.Seek(null, code));

            if (name.Trim().Length > 2)
                return View(farmerInfoRepo.Seek(name, null));
            return View(Enumerable.Empty<FarmerInfo>());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Get(int id)
        {
            return Content(farmerVersionInfoRepo.Get(id).Name);
        }
    }
}