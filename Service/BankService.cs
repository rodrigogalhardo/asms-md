using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using Omu.Awesome.Core;

namespace MRGSP.ASMS.Service
{
    public class BankService : IBankService
    {
        private readonly IBankRepo repo;

        public BankService(IBankRepo repo)
        {
            this.repo = repo;
        }

        public long Create(Bank o)
        {
            return repo.Insert(o);
        }

        public string Delete(int id)
        {
            return repo.Delete(id);
        }

        public IPageable<Bank> GetPage(int page, int pageSize = 10, string name = null, string code = null)
        {
            return new Pageable<Bank>
            {
                Page = repo.GetPage(page, pageSize, name, code),
                PageCount = ServiceUtils.GetPageCount(pageSize, repo.Count(name, code)),
                PageIndex = page,
            };
        }

        public bool Exists(string code)
        {
            return repo.Count(code) != 0;
        }

        public Bank Get(long id)
        {
            return repo.Get(id);
        }
        
    }
}