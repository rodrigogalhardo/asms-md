﻿using System.Collections.Generic;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Repository;
using Omu.Awesome.Core;

namespace MRGSP.ASMS.Data
{
    public class Repo<T> : IRepo<T> where T : new()
    {
        protected string Cs;

        public Repo(IConnectionFactory connFactory)
        {
            Cs = connFactory.GetConnectionString();
        }

        public IEnumerable<T> GetWhereStartsWith(string prop, string with, int max)
        {
            return DbUtil.GetWhereStartsWith<T>(prop, with, max, Cs);
        }

        public T Get(int id)
        {
            return DbUtil.Get<T>(id, Cs);
        }

        public IEnumerable<T> GetAll()
        {
            return DbUtil.GetAll<T>(Cs);
        }

        //returns the new autogenerated id
        public virtual int Insert(T o)
        {
            return DbUtil.Insert(o, Cs);
        }

        public virtual int Update(T o)
        {
            return DbUtil.Update(o, Cs);
        }

        public virtual int UpdateWhatWhere(object what, object where)
        {
            return DbUtil.UpdateWhatWhere<T>(what, where, Cs);
        }

        //returns rows affected
        public virtual int InsertNoIdentity(T o)
        {
            return DbUtil.InsertNoIdentity(o, Cs);
        }

        public IEnumerable<T> GetPage(int page, int pageSize)
        {
            return DbUtil.GetPage<T>(page, pageSize, Cs);
        }

        public int Count()
        {
            return DbUtil.Count<T>(Cs);
        }

        public IPageable<T> GetPageable(int page, int pageSize)
        {
            return new Pageable<T>
            {
                Page = GetPage(page, pageSize),
                PageCount = Tools.GetPageCount(pageSize, Count()),
                PageIndex = page,
            };
        }

        // example: repo.GetWhere(new { FirstName ="jhon", LastName="philips"});
        public IEnumerable<T> GetWhere(object where)
        {
            return DbUtil.GetWhere<T>(where, Cs);
        }

        public int Delete(int id)
        {
            return DbUtil.Delete<T>(id, Cs);
        }

        public int DeleteWhere(object where)
        {
            return DbUtil.DeleteWhere<T>(where, Cs);
        }
    }

}