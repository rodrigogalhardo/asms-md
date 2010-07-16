using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FarmerController : BaseController
    {
        private readonly IFarmerService farmerService;
        private readonly IBuilder<Farmer, FarmerCreateInput> createBuilder;

        public FarmerController(IFarmerService farmerService, IBuilder<Farmer, FarmerCreateInput> createBuilder)
        {
            this.farmerService = farmerService;
            this.createBuilder = createBuilder;
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
            return View(createBuilder.BuildInput(new Farmer()));
        }

        [HttpPost]
        public ActionResult Create(FarmerCreateInput input)
        {
            if (!ModelState.IsValid)
                return View(createBuilder.RebuildInput(input));

            farmerService.Create(createBuilder.BuilEntity(input));

            return Content("ok");
        }
    }
}