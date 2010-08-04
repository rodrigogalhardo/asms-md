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
                    cmd.InjectFrom<CommandInjection>(where);
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
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
                cmd.CommandText = "delete from " + typeof(T).Name + "s where id=" + id;

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        ///<returns> the id of the inserted object </returns>
        public static int Insert(object o, string cs)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = (string)"".InjectFrom<InsertInjection>(o) + " select @@identity";
                cmd.InjectFrom(new CommandInjection().IgnoreFields("Id"), o);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static int InsertNoIdentity(object o, string cs)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = (string)"".InjectFrom<InsertInjection>(o);
                cmd.InjectFrom(new CommandInjection().IgnoreFields("Id"), o);


                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }


        /// <returns>rows affected</returns>
        public static int ExecuteNonQuerySp(string sp, string cs, object parameters)
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

        public static IEnumerable<T> ExecuteReaderSp<T>(string sp, string cs, object parameters) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sp;
                    cmd.InjectFrom<CommandInjection>(parameters);
                    conn.Open();
                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                        {
                            var o = new T();
                            o.InjectFrom<ReaderInjection>(dr);
                            yield return o;
                        }
                }
            }
        }

        public static IEnumerable<T> ExecuteReaderSpValueType<T>(string sp, string cs, object parameters)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sp;
                    cmd.InjectFrom<CommandInjection>(parameters);
                    conn.Open();
                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                        {
                            yield return (T)dr.GetValue(0);
                        }
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