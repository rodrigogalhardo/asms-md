using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Data
{
    public class FarmerRepo: BaseRepo, IFarmerRepo
    {
        public FarmerRepo(IConnectionFactory connFactory) : base(connFactory)
        {
        }

        public long Insert(Farmer o) 
        {
            return Convert.ToInt64(DbUtil.InsertSp(o, Cs));
        }

        public Farmer Get(long id)
        {
            return DbUtil.Get<Farmer>(id, Cs);
        }

        public int Count(string name, string code)
        {
            using (var conn = new SqlConnection(Cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getFarmersCount";
                    cmd.Parameters.Add("code", SqlDbType.NVarChar, 20).Value = code;
                    cmd.Parameters.Add("name", SqlDbType.NVarChar, 200).Value = name;
                    conn.Open();

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public IEnumerable<Farmer> GetPage(int page, int pageSize, string name, string code)
        {
            using (var conn = new SqlConnection(Cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getFarmersPage";
                    cmd.Parameters.Add("pageSize", SqlDbType.Int).Value = pageSize;
                    cmd.Parameters.Add("page", SqlDbType.Int).Value = page;
                    cmd.Parameters.Add("name", SqlDbType.NVarChar, 200).Value = name;
                    cmd.Parameters.Add("code", SqlDbType.NVarChar, 20).Value = code;
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                            while (dr.Read())
                            {
                                var o = new Farmer();
                                o.InjectFrom<ReaderInjection>(dr);
                                yield return o;
                            }
                    }
                }
            }
        }


    }
}