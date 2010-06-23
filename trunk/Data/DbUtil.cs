using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Data
{
    public static class DbUtil
    {
        public static object Insert(object o, string cs)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "insert" + o.GetType().Name;
                    cmd.InjectFrom<CommandInjection>(o);
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        public static int Count(string name, string cs)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "get" + name + "sCount";
                    conn.Open();

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public static IEnumerable<T> GetPage<T>(int page, int pageSize, string name, string cs) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "get" + name + "sPage";
                    cmd.Parameters.Add("pageSize", SqlDbType.Int).Value = pageSize;
                    cmd.Parameters.Add("page", SqlDbType.Int).Value = page;
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                            while (dr.Read())
                            {
                                var o = new T();
                                o.InjectFrom<ReaderInjection>(dr);
                                yield return o;
                            }
                    }
                }
            }
        }
    }
}