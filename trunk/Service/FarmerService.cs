using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class FarmerService : IFarmerService
    {
        private readonly IFarmerRepo repo;

        public FarmerService(IFarmerRepo repo)
        {
            this.repo = repo;
        }

        public long Create(Farmer o)
        {
            return repo.Insert(o);
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