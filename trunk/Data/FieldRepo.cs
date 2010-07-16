using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Data
{
    public class FieldRepo : BaseRepo, IFieldRepo
    {
        public FieldRepo(IConnectionFactory connFactory)
            : base(connFactory)
        {
        }

        public IEnumerable<Field> GetAssigned(int id)
        {
            using (var conn = new SqlConnection(Cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getFieldsByFieldsetId";
                    cmd.Parameters.Add("id", SqlDbType.Int).Value = id;
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                        {
                            var o = new Field();
                            o.InjectFrom<ReaderInjection>(dr);
                            yield return o;
                        }
                }
            }
        }

        public IEnumerable<Field> GetUnassigned(int id)
        {
            using (var conn = new SqlConnection(Cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getUnassignedFieldsByFieldsetId";
                    cmd.Parameters.Add("id", SqlDbType.Int).Value = id;
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                        {
                            var o = new Field();
                            o.InjectFrom<ReaderInjection>(dr);
                            yield return o;
                        }
                }
            }
        }

        public int AssignField(int fieldId, int fieldsetId)
        {
            using (var conn = new SqlConnection(Cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "assignField";
                    cmd.Parameters.Add("fieldId", SqlDbType.Int).Value = fieldId;
                    cmd.Parameters.Add("fieldsetId", SqlDbType.Int).Value = fieldsetId;
                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int UnassignField(int fieldId, int fieldsetId)
        {
            using (var conn = new SqlConnection(Cs))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "unassignField";
                    cmd.Parameters.Add("fieldId", SqlDbType.Int).Value = fieldId;
                    cmd.Parameters.Add("fieldsetId", SqlDbType.Int).Value = fieldsetId;
                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}