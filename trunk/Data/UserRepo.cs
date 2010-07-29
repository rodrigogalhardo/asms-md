using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Data
{
    public class UserRepo : Repo<User>, IUserRepo
    {
        public UserRepo(IConnectionFactory connFactory)
            : base(connFactory)
        {

        }
            
        public override int Insert(User o)
        {
            using (var scope = new TransactionScope())
            {
                using (var conn = new SqlConnection(Cs))
                {
                    int userId;
                    conn.Open();
                    //insert user
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "insertUser";
                        cmd.Parameters.Add("name", SqlDbType.NVarChar, 20).Value = o.Name;
                        cmd.Parameters.Add("password", SqlDbType.NVarChar, 20).Value = o.Password;

                        userId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    //assign roles
                    foreach (var role in o.Roles)
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "assignRole";
                            cmd.Parameters.Add("userId", SqlDbType.NVarChar, 20).Value = userId;
                            cmd.Parameters.Add("roleId", SqlDbType.NVarChar, 20).Value = role.Id;

                            cmd.ExecuteNonQuery();
                        }
                    }
                    scope.Complete();
                    return userId;
                }
            }
        }

        public void Update(User o)
        {
            using (var scope = new TransactionScope())
            {
                using (var conn = new SqlConnection(Cs))
                {
                    conn.Open();
                    //clear roles
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "clearRoles";
                        cmd.Parameters.Add("id", SqlDbType.BigInt).Value = o.Id;
                        cmd.ExecuteNonQuery();
                    }

                    //assign roles
                    foreach (var role in o.Roles)
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "assignRole";
                            cmd.Parameters.Add("userId", SqlDbType.NVarChar, 20).Value = o.Id;
                            cmd.Parameters.Add("roleId", SqlDbType.NVarChar, 20).Value = role.Id;

                            cmd.ExecuteNonQuery();
                        }
                    }
                    scope.Complete();
                }
            }
        }

        //public IEnumerable<User> GetPage(int page, int pageSize)
        //{
        //    return DbUtil.GetPageSp<User>(page, pageSize, "User", Cs);
        //}

        //public int Count()
        //{
        //    return DbUtil.CountSp("User", Cs);
        //}

        public IEnumerable<Role> GetRoles(long id)
        {
            using (var conn = new SqlConnection(Cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getRolesByUserId";
                    cmd.Parameters.Add("id", SqlDbType.BigInt).Value = id;
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var o = new Role();
                            o.Id = dr.GetInt32(0);
                            o.Name = dr.GetString(1);
                            yield return o;
                        }
                    }
                }
            }
        }

        //public User Get(long id)
        //{
        //    using (var conn = new SqlConnection(Cs))
        //    {
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "getUser";
        //            cmd.Parameters.Add("id", SqlDbType.BigInt).Value = id;
        //            conn.Open();

        //            using (var dr = cmd.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                {
        //                    var o = new User();
        //                    o.InjectFrom<ReaderInjection>(dr);
        //                    return o;
        //                }
        //            }
        //        }
        //    }
        //    return null;
        //}

        public User Get(string name, string password)
        {
            using (var conn = new SqlConnection(Cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getUserByNamePass";
                    cmd.Parameters.Add("name", SqlDbType.NVarChar).Value = name;
                    cmd.Parameters.Add("password", SqlDbType.NVarChar).Value = password;
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var o = new User();
                            o.InjectFrom<ReaderInjection>(dr);
                            return o;
                        }
                    }
                }
            }
            return null;
        }

        public int Count(string name, string password)
        {
            using (var conn = new SqlConnection(Cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getUsersCountByNamePassword";
                    cmd.Parameters.Add("name", SqlDbType.NVarChar, 20).Value = name;
                    cmd.Parameters.Add("password", SqlDbType.NVarChar, 20).Value = password;
                    conn.Open();

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public int Count(string name)
        {
            using (var conn = new SqlConnection(Cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getUsersCountByName";
                    cmd.Parameters.Add("name", SqlDbType.NVarChar, 20).Value = name;
                    conn.Open();

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            using (var conn = new SqlConnection(Cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getRoles";
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var o = new Role();
                            o.Id = dr.GetInt32(0);
                            o.Name = dr.GetString(1);
                            yield return o;
                        }
                    }
                }
            }
        }

        public int UpdatePassword(long id, string password)
        {
            using (var conn = new SqlConnection(Cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "updatePassword";
                    cmd.Parameters.Add("id", SqlDbType.BigInt).Value = id;
                    cmd.Parameters.Add("password", SqlDbType.NVarChar, 20).Value = password;
                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

    }
}