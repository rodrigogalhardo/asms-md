using System.Collections.Generic;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class MeasureRepo: UberRepo<Measure>, IMeasureRepo
    {
        public MeasureRepo(IConnectionFactory connFactory) : base(connFactory)
        {
        }

        public IEnumerable<Measure> GetAssigned(int measuresetId)
        {
            return DbUtil.ExecuteReaderSp<Measure>(new { measuresetId }, Cs, "getAssignedMeasures");
        }

        public IEnumerable<Measure> GetUnassigned(int measuresetId)
        {
            return DbUtil.ExecuteReaderSp<Measure>(new { measuresetId }, Cs, "getUnassignedMeasures");
        }

        public int Assign(int measureId, int measuresetId)
        {
            return DbUtil.ExecuteNonQuerySp(new { measureId, measuresetId }, Cs, "assignMeasure");
        }

        public int Unassign(int measureId, int measuresetId)
        {
            return DbUtil.ExecuteNonQuerySp(new { measureId, measuresetId }, Cs, "unassignMeasure");
        }

        public IEnumerable<Measure> GetActives()
        {
            return DbUtil.ExecuteReaderSp<Measure>(new object(), Cs, "getMeasures");
        }
    }
}