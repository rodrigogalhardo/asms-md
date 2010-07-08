using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class BaseRepo
    {
        protected readonly string Cs;

        public BaseRepo(IConnectionFactory connFactory)
        {
            Cs = connFactory.GetConnectionString();
        }

    }
}