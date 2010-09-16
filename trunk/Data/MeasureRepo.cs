using System;
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

        public IEnumerable<int> GetUsedIn(DateTime month)
        {
            return DbUtil.ExecuteReaderSpValueType<int>("getUsedMeasureIds", Cs, new {month});
        }

        public IEnumerable<Measure> GetAssigned(int measuresetId)
        {
            return DbUtil.ExecuteReaderSp<Measure>("getAssignedMeasures", new { measuresetId }, Cs);
        }

        public IEnumerable<Measure> GetUnassigned(int measuresetId)
        {
            return DbUtil.ExecuteReaderSp<Measure>("getUnassignedMeasures", new { measuresetId }, Cs);
        }

        public int Assign(int measureId, int measuresetId)
        {
            return DbUtil.ExecuteNonQuerySp("assignMeasure", new { measureId, measuresetId }, Cs);
        }

        public int Unassign(int measureId, int measuresetId)
        {
            return DbUtil.ExecuteNonQuerySp("unassignMeasure", new { measureId, measuresetId }, Cs);
        }

        public IEnumerable<Measure> GetActives()
        {
            return DbUtil.ExecuteReaderSp<Measure>("getMeasures", new object(), Cs);
        }
    }
}