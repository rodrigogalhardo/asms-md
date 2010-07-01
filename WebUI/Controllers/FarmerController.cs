using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra.Dto;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class AdminController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
    public class FarmerController : BaseController
    {
        private readonly IFarmerService farmerService;

        public FarmerController(IFarmerService farmerService)
        {
            this.farmerService = farmerService;
        }

        [HttpPost]
        public ActionResult Get(long id)
        {
            return Content(farmerService.Get(id).Name);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Page(int? page, string name, string code)
        {
            return View(farmerService.GetPage(page ?? 1, 5, name, code));
        }

        public ActionResult Create()
        {
            return View(new FarmerCreateInput());
        }

        [HttpPost]
        public ActionResult Create(FarmerCreateInput input)
        {
            if (!ModelState.IsValid)
                return View(input);

            farmerService.Create((Farmer)new Farmer().InjectFrom(input));

            return Content("ok");
        }
    }
}