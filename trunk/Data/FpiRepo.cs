using System.Linq;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class FpiRepo : Repo<Fpi>, IFpiRepo
    {
        public FpiRepo(IConnectionFactory connFactory) : base(connFactory)
        {
        }

        public decimal GetAmountPayed(int fpiId)
        {
            return (decimal)DbUtil.ExecuteScalarSp("getAmountPayed", new { fpiId }, Cs);
        }

        public Fpi GetPrevious(Fpi o)
        {
            return DbUtil.ExecuteReaderSp<Fpi>("getPreviousFpi", new {o.MeasuresetId, o.MeasureId, o.Month}, Cs).SingleOrDefault();
        }

    }
}