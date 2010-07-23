using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class MeasuresetRepo: UberRepo<Measureset>, IMeasuresetRepo
    {
        public MeasuresetRepo(IConnectionFactory connFactory) : base(connFactory)
        {
        }
        
        public int ChangeState(int id, int stateId)
        {
            return DbUtil.ExecuteNonQuerySp(new { id, stateId }, Cs, "changeMeasuresetState");
        }
        
        public int Activate(int id)
        {
            return DbUtil.ExecuteNonQuerySp(new { id }, Cs, "activateMeasureset");
        }
    }
}