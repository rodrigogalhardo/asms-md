using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Data
{
    public class BankRepo : BaseRepo, IBankRepo
    {
        public BankRepo(IConnectionFactory connFactory)
            : base(connFactory)
        {
        }

        public long Insert(Bank o)
        {
            return Convert.ToInt64(DbUtil.InsertSp(o, Cs));
        }

        public Bank Get(long id)
        {
            return DbUtil.Get<Bank>(id, Cs);
        }

        public int Count(string name, string code)
        {
            using (var conn = new SqlConnection(Cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getBanksCount";
                    cmd.Parameters.Add("code", SqlDbType.NVarChar, 20).Value = code;
                    cmd.Parameters.Add("name", SqlDbType.NVarChar, 200).Value = name;
                    conn.Open();

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public IEnumerable<Bank> GetPage(int page, int pageSize, string name, string code)
        {
            using (var conn = new SqlConnection(Cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getBanksPage";
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
                                var o = new Bank();
                                o.InjectFrom<ReaderInjection>(dr);
                                yield return o;
                            }
                    }
                }
            }
        }

        public int Count(string code)
        {
            using (var conn = new SqlConnection(Cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getBanksCountByCode";
                    cmd.Parameters.Add("code", SqlDbType.NVarChar, 20).Value = code;
                    conn.Open();

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public string Delete(int id)
        {

            using (var conn = new SqlConnection(Cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "deleteBank";
                    cmd.Parameters.Add("id", SqlDbType.Int).Value = id;
                    conn.Open();

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return string.Empty;
                    }
                    catch (SqlException)
                    {
                        return "nu pot sterge acest element, deoarece este utilizat de alte element";
                    }
                }
            }
        }
    }
}