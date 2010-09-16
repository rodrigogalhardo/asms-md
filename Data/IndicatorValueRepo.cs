using System;
using System.Collections.Generic;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class IndicatorValueRepo : Repo<IndicatorValue>, IIndicatorValueRepo
    {
        public IndicatorValueRepo(IConnectionFactory connFactory)
            : base(connFactory)
        {
        }

        public IEnumerable<IndicatorValue> GetBy(int fpiId)
        {
            return DbUtil.ExecuteReaderSp<IndicatorValue>("getIndicatorValues", new { fpiId }, Cs);
        }
    }
}