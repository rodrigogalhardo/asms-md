using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Data
{
    public static class DbUtil
    {
        public static IEnumerable<T> GetWhere<T>(object where, string cs) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from " + typeof(T).Name + "s where ".InjectFrom<WhereInjection>(where);
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

        public static int Delete<T>(int id, string cs)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from " + typeof (T).Name + "s where id=" + id;

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static int Insert(object o, string cs)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = (string)"".InjectFrom<InsertInjection>(o);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static int ExecuteNonQuerySp(object parameters, string cs, string sp)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sp;
                    cmd.InjectFrom<CommandInjection>(parameters);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static object InsertSp(object o, string cs)
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

        public static int CountSp(string name, string cs)
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

        public static int Count<T>(string cs)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select count(*) from " + typeof(T).Name + "s";
                    conn.Open();

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public static IEnumerable<T> GetAll<T>(string cs) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from " + typeof(T).Name + "s";
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

        public static IEnumerable<T> GetPage<T>(int page, int pageSize, string cs) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    var name = typeof(T).Name + "s";

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format(@"with result as(select *, ROW_NUMBER() over(order by id desc) nr
                            from {0}
                    )
                    select  * 
                    from    result
                    where   nr  between (({1} - 1) * {2} + 1)
                            and ({1} * {2}) ", name, page, pageSize);
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

        public static IEnumerable<T> GetPageSp<T>(int page, int pageSize, string name, string cs) where T : new()
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

        public static T Get<T>(long id, string cs) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from " + typeof(T).Name + "s where id = " + id;
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                            while (dr.Read())
                            {
                                var o = new T();
                                o.InjectFrom<ReaderInjection>(dr);
                                return o;
                            }
                    }
                }
            }
            return default(T);
        }


    }
}