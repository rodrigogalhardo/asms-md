using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class FieldsetRepo : UberRepo<Fieldset>, IFieldsetRepo
    {
        public FieldsetRepo(IConnectionFactory connFactory)
            : base(connFactory)
        {
        }

        public int ChangeState(int id, int stateId)
        {
            return DbUtil.ExecuteNonQuerySp(new { id, stateId }, Cs, "changeFieldsetState");
        }

        public int Activate(int id)
        {
            return DbUtil.ExecuteNonQuerySp(new { id }, Cs, "activateFieldset");
        }

    }
}