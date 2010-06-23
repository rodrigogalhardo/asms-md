using System.Configuration;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["cs"].ToString();
        }
    }
}