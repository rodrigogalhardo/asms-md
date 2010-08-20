using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FarmerController : BaseController
    {
        private readonly IBuilder<Organization, OrganizationInput> organizationBuilder;
        private readonly IBuilder<LandOwner, LandOwnerInput> landOwnerBuilder;
        private readonly IFarmerService farmerService;

        public FarmerController(
            IBuilder<Organization, OrganizationInput> organizationBuilder,
            IBuilder<LandOwner, LandOwnerInput> landOwnerBuilder,
            IFarmerService farmerService)
        {
            this.organizationBuilder = organizationBuilder;
            this.farmerService = farmerService;
            this.landOwnerBuilder = landOwnerBuilder;
        }

        public ActionResult Index(int? page)
        {
            return View(farmerService.GetPageableInfo(page ?? 1, 10));
        }

        [ChildActionOnly]
        public ActionResult DisplayOrganization(int farmerId)
        {
            return View(farmerService.GetOrganizations(farmerId));
        }
        
        [ChildActionOnly]
        public ActionResult DisplayLandOwner(int farmerId)
        {
            return View(farmerService.GetLandOwners(farmerId));
        }

        public ActionResult EditOrganization(int farmerId)
        {
            return View("CreateOrganization", organizationBuilder.BuildInput(farmerService.GetOrganization(farmerId)));
        }

        [HttpPost]
        public ActionResult EditOrganization(OrganizationInput input)
        {
            if (!ModelState.IsValid) return View("CreateOrganization", organizationBuilder.RebuildInput(input));
            farmerService.AddOrganization(organizationBuilder.BuilEntity(input));
            return RedirectToAction("open", new { id = input.FarmerId });
        }

        public ActionResult EditLandOwner(int farmerId)
        {
            return View("CreateLandOwner", landOwnerBuilder.BuildInput(farmerService.GetLandOwner(farmerId)));
        }

        [HttpPost]
        public ActionResult EditLandOwner(LandOwnerInput input)
        {
            if (!ModelState.IsValid) return View("CreateLandOwner", landOwnerBuilder.RebuildInput(input));
            farmerService.AddLandOwner(landOwnerBuilder.BuilEntity(input));
            return RedirectToAction("open", new {id = input.FarmerId});
        }

        public ActionResult CreateLandOwner()
        {
            return View(landOwnerBuilder.BuildInput(new LandOwner()));
        }

        public ActionResult CreateOrganization()
        {
            return View(organizationBuilder.BuildInput(new Organization()));
        }

        [HttpPost]
        public ActionResult CreateLandOwner(LandOwnerInput input)
        {
            return !ModelState.IsValid
                       ? (ActionResult)View(landOwnerBuilder.RebuildInput(input))
                       : RedirectToAction("open",
                                          new { id = farmerService.CreateLandOwner(landOwnerBuilder.BuilEntity(input)) });
        }

        [HttpPost]
        public ActionResult CreateOrganization(OrganizationInput input)
        {
            return !ModelState.IsValid
                       ? (ActionResult)View(organizationBuilder.RebuildInput(input))
                       : RedirectToAction("open",
                                          new { id = farmerService.CreateOrganization(organizationBuilder.BuilEntity(input)) });
        }

        public ActionResult Open(int id)
        {
            return View(farmerService.GetInfo(id));
        }

        public ActionResult Info(int id)
        {
            return View(farmerService.GetInfo(id));
        }
       
    }
}