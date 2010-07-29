using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class MeasuresetRepo: Repo<Measureset>, IMeasuresetRepo
    {
        public MeasuresetRepo(IConnectionFactory connFactory) : base(connFactory)
        {
        }
        
        public int ChangeState(int id, int stateId)
        {
            return DbUtil.ExecuteNonQuerySp("changeMeasuresetState", Cs, new { id, stateId });
        }
        
        public int Activate(int id)
        {
            return DbUtil.ExecuteNonQuerySp("activateMeasureset", Cs, new { id });
        }
    }
}