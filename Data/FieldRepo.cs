using System;
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
            return DbUtil.ExecuteReaderSp<Field>("getFieldsByFieldsetId", new {id}, Cs);
        }

        public IEnumerable<Field> GetUnassigned(int id)
        {
            return DbUtil.ExecuteReaderSp<Field>("getUnassignedFieldsByFieldsetId", new { id }, Cs);
        }

        public int AssignField(int fieldId, int fieldsetId)
        {
            return DbUtil.ExecuteNonQuerySp("assignField", new { fieldId, fieldsetId }, Cs);
        }

        public int UnassignField(int fieldId, int fieldsetId)
        {
            return DbUtil.ExecuteNonQuerySp("unassignField", new { fieldId, fieldsetId }, Cs);
        }
    }

    
}