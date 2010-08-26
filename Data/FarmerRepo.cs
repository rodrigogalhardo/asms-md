using System;
using System.Linq;
using System.Transactions;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class FarmerRepo: BaseRepo, IFarmerRepo
    {
        private readonly IRepo<FarmerVersion> farmerVersionRepo;
        private readonly IRepo<LandOwner> landOwnerRepo;
        private readonly IRepo<Organization> organizationRepo;
        private readonly IRepo<Farmer> farmerRepo;

        public FarmerRepo(IConnectionFactory connFactory, IRepo<FarmerVersion> farmerVersionRepo, IRepo<LandOwner> landOwnerRepo, IRepo<Organization> organizationRepo, IRepo<Farmer> farmerRepo) : base(connFactory)
        {
            this.farmerVersionRepo = farmerVersionRepo;
            this.farmerRepo = farmerRepo;
            this.organizationRepo = organizationRepo;
            this.landOwnerRepo = landOwnerRepo;
        }

        public LandOwner GetLandOwner(int farmerId)
        {
            var fv = farmerVersionRepo.GetWhere(new {farmerId, EndDate = DBNull.Value}).Single();
            return landOwnerRepo.GetWhere(new {farmerVersionId = fv.Id}).Single();
        }
        
        public Organization GetOrganization(int farmerId)
        {
            var fv = farmerVersionRepo.GetWhere(new {farmerId, EndDate = DBNull.Value}).Single();
            return organizationRepo.GetWhere(new {farmerVersionId = fv.Id}).Single();
        }

        public int CreateOrganization(Organization o)
        {
            using (var scope = new TransactionScope())
            {
                var farmerId = farmerRepo.Insert(new Farmer { FType = FarmerType.Organization });

                var v = new FarmerVersion
                            {
                                FarmerId = farmerId,
                                StartDate = DateTime.Now
                            };

                o.FarmerVersionId = farmerVersionRepo.Insert(v);

                organizationRepo.Insert(o);

                scope.Complete();
                return farmerId;
            }
        }

        public void AddLandOwner(LandOwner o, int farmerId)
        {
            using (var scope = new TransactionScope())
            {
                farmerVersionRepo.UpdateWhatWhere(new { EndDate = DateTime.Now }, new { farmerId, EndDate = DBNull.Value });

                o.FarmerVersionId = farmerVersionRepo.Insert(new FarmerVersion { FarmerId = farmerId, StartDate = DateTime.Now });
                landOwnerRepo.Insert(o);

                scope.Complete();
            }
        }

        public void AddOrganization(Organization o, int farmerId)
        {
            using (var scope = new TransactionScope())
            {
                farmerVersionRepo.UpdateWhatWhere(new { EndDate = DateTime.Now }, new { farmerId, EndDate = DBNull.Value });

                o.FarmerVersionId = farmerVersionRepo.Insert(new FarmerVersion { FarmerId = farmerId, StartDate = DateTime.Now });
                organizationRepo.Insert(o);

                scope.Complete();
            }
        }

        public int CreateLandOwner(LandOwner o)
        {
            using (var scope = new TransactionScope())
            {
                var farmerId = farmerRepo.Insert(new Farmer { FType = FarmerType.LandOwner });
                var v = new FarmerVersion
                            {
                                FarmerId = farmerId,
                                StartDate = DateTime.Now
                            };

                o.FarmerVersionId = farmerVersionRepo.Insert(v);

                landOwnerRepo.Insert(o);

                scope.Complete();
                return farmerId;
            }
        }
    }
    
}