using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FarmerController : BaseController
    {
        private readonly ICreateBuilder<Organization, OrganizationInput> organizationBuilder;
        private readonly ICreateBuilder<LandOwner, LandOwnerInput> landOwnerBuilder;
        private readonly IFarmerService farmerService;
        private readonly IFarmerRepo farmerRepo;

        public FarmerController(ICreateBuilder<Organization, OrganizationInput> organizationBuilder, ICreateBuilder<LandOwner, LandOwnerInput> landOwnerBuilder, IFarmerService farmerService, IFarmerRepo farmerRepo)
        {
            this.organizationBuilder = organizationBuilder;
            this.landOwnerBuilder = landOwnerBuilder;
            this.farmerService = farmerService;
            this.farmerRepo = farmerRepo;
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
            return View("CreateOrganization", organizationBuilder.BuildInput(farmerRepo.GetOrganization(farmerId)));
        }

        [HttpPost]
        public ActionResult EditOrganization(OrganizationInput input)
        {
            if (!ModelState.IsValid) return View("CreateOrganization", organizationBuilder.RebuildInput(input));
            farmerRepo.AddOrganization(organizationBuilder.BuildEntity(input), input.FarmerId);
            return RedirectToAction("open", new { id = input.FarmerId });
        }

        public ActionResult EditLandOwner(int farmerId)
        {
            return View("CreateLandOwner", landOwnerBuilder.BuildInput(farmerRepo.GetLandOwner(farmerId)));
        }

        [HttpPost]
        public ActionResult EditLandOwner(LandOwnerInput input)
        {
            if (!ModelState.IsValid) return View("CreateLandOwner", landOwnerBuilder.RebuildInput(input));
            farmerRepo.AddLandOwner(landOwnerBuilder.BuildEntity(input), input.FarmerId);
            return RedirectToAction("open", new {id = input.FarmerId});
        }

        public ActionResult CreateOrganization()
        {
            return View(organizationBuilder.BuildInput(new Organization()));
        }

        public ActionResult CreateLandOwner()
        {
            return View(landOwnerBuilder.BuildInput(new LandOwner()));
        }

        [HttpPost]
        public ActionResult CreateLandOwner(LandOwnerInput input)
        {
            return !ModelState.IsValid
                       ? (ActionResult)View(landOwnerBuilder.RebuildInput(input))
                       : RedirectToAction("open",
                                          new { id = farmerRepo.CreateLandOwner(landOwnerBuilder.BuildEntity(input)) });
        }

        [HttpPost]
        public ActionResult CreateOrganization(OrganizationInput input)
        {
            return !ModelState.IsValid
                       ? (ActionResult)View(organizationBuilder.RebuildInput(input))
                       : RedirectToAction("open",
                                          new { id = farmerRepo.CreateOrganization(organizationBuilder.BuildEntity(input)) });
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