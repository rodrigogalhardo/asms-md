using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class BaseRepository
    {
        protected readonly string Cs;

        public BaseRepository(IConnectionFactory connFactory)
        {
            Cs = connFactory.GetConnectionString();
        }

    }
}