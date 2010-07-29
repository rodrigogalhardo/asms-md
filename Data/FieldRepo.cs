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
            return DbUtil.ExecuteReaderSp<Field>("getFieldsByFieldsetId", Cs, new {id});
        }

        public IEnumerable<Field> GetUnassigned(int id)
        {
            return DbUtil.ExecuteReaderSp<Field>("getUnassignedFieldsByFieldsetId", Cs, new { id });
        }

        public int AssignField(int fieldId, int fieldsetId)
        {
            return DbUtil.ExecuteNonQuerySp("assignField", Cs, new { fieldId, fieldsetId });
        }

        public int UnassignField(int fieldId, int fieldsetId)
        {
            return DbUtil.ExecuteNonQuerySp("unassignField", Cs, new { fieldId, fieldsetId });
        }
    }

    
}