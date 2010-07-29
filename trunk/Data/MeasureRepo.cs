using System.Collections.Generic;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class MeasureRepo: Repo<Measure>, IMeasureRepo
    {
        public MeasureRepo(IConnectionFactory connFactory) : base(connFactory)
        {
        }

        public IEnumerable<Measure> GetAssigned(int measuresetId)
        {
            return DbUtil.ExecuteReaderSp<Measure>("getAssignedMeasures", Cs, new { measuresetId });
        }

        public IEnumerable<Measure> GetUnassigned(int measuresetId)
        {
            return DbUtil.ExecuteReaderSp<Measure>("getUnassignedMeasures", Cs, new { measuresetId });
        }

        public int Assign(int measureId, int measuresetId)
        {
            return DbUtil.ExecuteNonQuerySp("assignMeasure", Cs, new { measureId, measuresetId });
        }

        public int Unassign(int measureId, int measuresetId)
        {
            return DbUtil.ExecuteNonQuerySp("unassignMeasure", Cs, new { measureId, measuresetId });
        }

        public IEnumerable<Measure> GetActives()
        {
            return DbUtil.ExecuteReaderSp<Measure>("getMeasures", Cs, new object());
        }
    }
}