using System;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class BankService : IBankService
    {
        private readonly IBankRepo repo;

        public BankService(IBankRepo repo)
        {
            this.repo = repo;
        }

        public int Create(Bank o)
        {
            return repo.Insert(o);
        }

        public string Delete(int id)
        {
            return repo.Delete(id);
        }

        public IPageable<Bank> GetPage(int page, int pageSize)
        {
            return ServiceUtils.GetPage(page, pageSize, repo);
        }

        public bool Exists(string code)
        {
            return repo.Count(code) != 0;
        }
    }
}