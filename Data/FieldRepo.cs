using System.Collections.Generic;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

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
            return DbUtil.ExecuteReaderSp<Field>(new {id}, Cs, "getFieldsByFieldsetId");
        }

        public IEnumerable<Field> GetUnassigned(int id)
        {
            return DbUtil.ExecuteReaderSp<Field>(new { id }, Cs, "getUnassignedFieldsByFieldsetId");
        }

        public int AssignField(int fieldId, int fieldsetId)
        {
            return DbUtil.ExecuteNonQuerySp(new { fieldId, fieldsetId }, Cs, "assignField");
        }

        public int UnassignField(int fieldId, int fieldsetId)
        {
            return DbUtil.ExecuteNonQuerySp(new { fieldId, fieldsetId }, Cs, "unassignField");
        }
    }

    
}