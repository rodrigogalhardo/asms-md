using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Data
{
    public static class DbUtil
    {
        public static IEnumerable<T> GetWhereStartsWith<T>(string prop, string with, int max, string cs) where T: new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select top "+ max +" * from "
                        + TableConvention.Resolve(typeof(T)) + string.Format(" where {0} like '{1}%' ", prop, with);
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

        public static IEnumerable<T> GetWhere<T>(object where, string cs) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from "
                        + TableConvention.Resolve(typeof(T)) + " where "
                                                        .InjectFrom(new FieldsBy()
                                                        .SetFormat("{0}=@{0}")
                                                        .SetNullFormat("{0} is null")
                                                        .SetGlue("and"),
                                                            where);
                    cmd.InjectFrom<SetParamsValues>(where);
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

        public static int DeleteWhere<T>(object where, string cs)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "delete from " + TableConvention.Resolve(typeof(T)) + " where "
                        .InjectFrom(new FieldsBy()
                        .SetFormat("{0}=@{0}")
                        .SetNullFormat("{0} is null")
                        .SetGlue("and"),
                        where);
                    cmd.InjectFrom<SetParamsValues>(where);
                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static int CountWhere<T>(object where, string cs) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select count(*) from " + TableConvention.Resolve(typeof(T)) + " where "
                        .InjectFrom(new FieldsBy()
                        .SetFormat("{0}=@{0}")
                        .SetNullFormat("{0} is null")
                        .SetGlue("and"),
                        where);
                    cmd.InjectFrom<SetParamsValues>(where);
                    conn.Open();

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public static int Delete<T>(int id, string cs)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from " + TableConvention.Resolve(typeof(T)) + " where id=" + id;

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        ///<returns> the id of the inserted object </returns>
        public static int Insert(object o, string cs, string[] ignore = null)
        {
            ignore = ignore ?? new[] { "Id" };
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert " + TableConvention.Resolve(o) + " ("
                    .InjectFrom(new FieldsBy().IgnoreFields(ignore), o) + ") values("
                    .InjectFrom(new FieldsBy().IgnoreFields(ignore).SetFormat("@{0}"), o)
                    + ") select @@identity";

                cmd.InjectFrom(new SetParamsValues().IgnoreFields(ignore), o);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static int Update(object o, string cs)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update " + TableConvention.Resolve(o) + " set "
                    .InjectFrom(new FieldsBy().IgnoreFields("Id").SetFormat("{0}=@{0}"), o)
                    + " where Id = @Id";

                cmd.InjectFrom<SetParamsValues>(o);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static int UpdateWhatWhere<T>(object what, object where, string cs)
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update " + TableConvention.Resolve(typeof(T)) + " set "
                    .InjectFrom(new FieldsBy().SetFormat("{0}=@{0}"), what)
                    + " where "
                    .InjectFrom(new FieldsBy()
                    .SetFormat("{0}=@wp{0}")
                    .SetNullFormat("{0} is null")
                    .SetGlue("and"),
                    where);

                cmd.InjectFrom<SetParamsValues>(what);
                cmd.InjectFrom(new SetParamsValues().Prefix("wp"), where);

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
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert " + TableConvention.Resolve(o) + " ("
                    .InjectFrom(new FieldsBy().IgnoreFields("Id"), o) + ") values("
                    .InjectFrom(new FieldsBy().IgnoreFields("Id").SetFormat("@{0}"), o) + ")";

                cmd.InjectFrom<SetParamsValues>(o);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <returns>rows affected</returns>
        public static int ExecuteNonQuerySp(string sp, object parameters, string cs)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sp;
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static object ExecuteScalarSp(string sp, object parameters, string cs)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sp;
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        public static IEnumerable<T> ExecuteReader<T>(string query, object parameters, string cs) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.InjectFrom<SetParamsValues>(parameters);
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

        public static IEnumerable<T> ExecuteReaderSp<T>(string sp, object parameters, string cs) where T : new()
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sp;
                    cmd.InjectFrom<SetParamsValues>(parameters);
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
                    cmd.InjectFrom<SetParamsValues>(parameters);
                    conn.Open();
                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                        {
                            yield return (T)dr.GetValue(0);
                        }
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
                    cmd.CommandText = "select count(*) from " + TableConvention.Resolve(typeof(T));
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
                    cmd.CommandText = "select * from " + TableConvention.Resolve(typeof(T));
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
                    var name = TableConvention.Resolve(typeof(T));

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
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from " + TableConvention.Resolve(typeof(T)) + " where id = " + id;
                conn.Open();

                using (var dr = cmd.ExecuteReader())
                    while (dr.Read())
                    {
                        var o = new T();
                        o.InjectFrom<ReaderInjection>(dr);
                        return o;
                    }
            }
            return default(T);
        }
    }
}