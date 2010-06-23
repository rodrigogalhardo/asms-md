using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class BankRepo : BaseRepository, IBankRepo
    {
        public BankRepo(IConnectionFactory connFactory) : base(connFactory)
        {
        }

        public int Insert(Bank o)
        {
            return Convert.ToInt32(DbUtil.Insert(o, Cs));
        }

        public int Count()
        {
            return DbUtil.Count("Bank", Cs);
        }

        public IEnumerable<Bank> GetPage(int page, int pageSize)
        {
            return DbUtil.GetPage<Bank>(page, pageSize, "Bank", Cs);
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