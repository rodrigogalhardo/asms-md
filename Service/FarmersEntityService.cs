using System;
using System.Collections.Generic;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class FarmersEntityService<T> : IFarmersEntityService<T> where T : FarmersEntity, new()
    {
        private readonly IRepo<T> repo;

        public FarmersEntityService(IRepo<T> repo)
        {
            this.repo = repo;
        }

        public IEnumerable<T> GetByFarmerId(int farmerId)
        {
            return repo.GetWhere(new {farmerId});
        }

        public int Create(T o)
        {
            o.StartDate = DateTime.Now;
            return repo.Insert(o);
        }

        public void Deactivate(int id)
        {
            if(repo.Get(id).EndDate.HasValue) throw new AsmsEx("acest element este deja inactiv");
            repo.UpdateWhatWhere(new {EndDate = DateTime.Now}, new {id});
        }
    }
}