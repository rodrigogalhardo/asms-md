using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class FarmerService : IFarmerService
    {
        private readonly IFarmerRepo repo;
        private readonly IRepo<Farmer> farmerRepo;
        private readonly IRepo<Organization> organizationRepo;
        private readonly IRepo<LandOwner> landOwnerRepo;
        private readonly IRepo<FarmerInfo> farmerInfoRepo;
        private readonly IRepo<OrganizationDisplay> organizationDisplayRepo;

        public FarmerService(IFarmerRepo repo, IRepo<Organization> organizationRepo, IRepo<Farmer> farmerRepo, IRepo<LandOwner> landOwnerRepo, IRepo<FarmerInfo> farmerInfoRepo, IRepo<OrganizationDisplay> organizationDisplayRepo)
        {
            this.repo = repo;
            this.organizationDisplayRepo = organizationDisplayRepo;
            this.farmerInfoRepo = farmerInfoRepo;
            this.landOwnerRepo = landOwnerRepo;
            this.farmerRepo = farmerRepo;
            this.organizationRepo = organizationRepo;
        }

        public IEnumerable<LandOwner> GetLandOwners(int farmerId)
        {
            return landOwnerRepo.GetWhere(new {farmerId}).OrderByDescending(o => o.Id);
        } 
        
        public IEnumerable<OrganizationDisplay> GetOrganizations(int farmerId)
        {
            return organizationDisplayRepo.GetWhere(new {farmerId}).OrderByDescending(o => o.Id);
        }

        public LandOwner GetLandOwner(int farmerId)
        {
            return landOwnerRepo.GetWhere(new{farmerId, EndDate = DBNull.Value}).Single();
        }

        public Organization GetOrganization(int farmerId)
        {
            return organizationRepo.GetWhere(new{farmerId, EndDate = DBNull.Value}).Single();
        }

        public FarmerInfo GetInfo(int id)
        {
            return farmerInfoRepo.GetWhere(new { id }).Single();
        }

        public IPageable<FarmerInfo> GetPageableInfo(int page, int pageSize)
        {
            return farmerInfoRepo.GetPageable(page, pageSize);
        }

        public int CreateOrganization(Organization o)
        {
            using (var scope = new TransactionScope())
            {
                var farmerId = farmerRepo.Insert(new Farmer { FType = FarmerType.Organization });

                o.FarmerId = farmerId;
                o.StartDate = DateTime.Now;
                organizationRepo.Insert(o);

                scope.Complete();
                return farmerId;
            }
        }

        public void AddLandOwner(LandOwner o)
        {
            using (var scope = new TransactionScope())
            {
                landOwnerRepo.UpdateWhatWhere(new { EndDate = DateTime.Now }, new { o.FarmerId, EndDate = DBNull.Value });
                o.StartDate = DateTime.Now;
                landOwnerRepo.Insert(o);

                scope.Complete();
            }
        }
        
        public void AddOrganization(Organization o)
        {
            using (var scope = new TransactionScope())
            {
                organizationRepo.UpdateWhatWhere(new { EndDate = DateTime.Now }, new { o.FarmerId, EndDate = DBNull.Value });
                o.StartDate = DateTime.Now;
                organizationRepo.Insert(o);

                scope.Complete();
            }
        }

        public int CreateLandOwner(LandOwner o)
        {
            using (var scope = new TransactionScope())
            {
                var farmerId = farmerRepo.Insert(new Farmer { FType = FarmerType.LandOwner });

                o.FarmerId = farmerId;
                o.StartDate = DateTime.Now;
                landOwnerRepo.Insert(o);

                scope.Complete();
                return farmerId;
            }
        }

        public IPageable<Farmer> GetPage(int page, int pageSize, string name = null, string code = null)
        {
            return new Pageable<Farmer>
                       {
                           Page = repo.GetPage(page, pageSize, name, code),
                           PageCount = ServiceUtils.GetPageCount(pageSize, repo.Count(name, code)),
                           PageIndex = page,
                       };
        }

        public bool Exists(string code)
        {
            return repo.Count(null, code) != 0;
        }

        public Farmer Get(long id)
        {
            return repo.Get(id);
        }
    }
}