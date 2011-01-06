using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using Omu.Awesome.Core;

namespace MRGSP.ASMS.Service
{
    public class CrudService<T> : ICrudService<T> where T : Entity, new()
    {
        protected IRepo<T> repo;

        public CrudService(IRepo<T> repo)
        {
            this.repo = repo;
        }

        public IPageable<T> GetPageable(int page, int pageSize)
        {
            return repo.GetPageable(page, pageSize);
        }

        public virtual T Get(int id)
        {
            return repo.Get(id);
        }

        public virtual int Create(T e)
        {
            return repo.Insert(e);
        }

        public virtual void Save(T e)
        {
            repo.Update(e);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public int Count()
        {
            return repo.Count();
        }
    }
}