using System.Collections.Generic;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class CompetitorRepo : Repo<Competitor>, ICompetitorRepo
    {
        public CompetitorRepo(IConnectionFactory connFactory) : base(connFactory)
        {
        }

        public IEnumerable<Competitor> Losers(int fpiId)
        {
            return DbUtil.ExecuteReaderSp<Competitor>("getLosers", new {fpiId}, Cs);
        }
    }
}