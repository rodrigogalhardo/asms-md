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
            return (decimal)DbUtil.ExecuteScalarSp("getAmountPayed", Cs, new { fpiId });
        }

    }
}