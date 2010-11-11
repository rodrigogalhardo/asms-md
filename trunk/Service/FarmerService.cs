using System.Collections.Generic;
using System.Linq;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using Omu.Awesome.Core;

namespace MRGSP.ASMS.Service
{
    
    public class FarmerService : IFarmerService
    {
        private readonly IRepo<FarmerInfo> farmerInfoRepo;
        private readonly IRepo<OrganizationInfo> organizationDisplayRepo;
        private readonly IRepo<LandOwnerInfo> landOwnerInfoRepo;

        public FarmerService(IRepo<FarmerInfo> farmerInfoRepo, IRepo<OrganizationInfo> organizationDisplayRepo, IRepo<LandOwnerInfo> landOwnerInfoRepo)
        {
            this.farmerInfoRepo = farmerInfoRepo;
            this.organizationDisplayRepo = organizationDisplayRepo;
            this.landOwnerInfoRepo = landOwnerInfoRepo;
        }

        public IEnumerable<LandOwnerInfo> GetLandOwners(int farmerId)
        {
            return landOwnerInfoRepo.GetWhere(new { farmerId }).OrderByDescending(o => o.Id);
        }

        public IEnumerable<OrganizationInfo> GetOrganizations(int farmerId)
        {
            return organizationDisplayRepo.GetWhere(new { farmerId }).OrderByDescending(o => o.Id);
        }

        public FarmerInfo GetInfo(int id)
        {
            return farmerInfoRepo.GetWhere(new { id }).Single();
        }

        public IPageable<FarmerInfo> GetPageableInfo(int page, int pageSize)
        {
            return farmerInfoRepo.GetPageable(page, pageSize);
        }

    }
}