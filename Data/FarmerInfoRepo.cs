using System.Collections.Generic;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class FarmerInfoRepo : Repo<FarmerInfo>, IFarmerInfoRepo
    {
        public FarmerInfoRepo(IConnectionFactory connFactory) : base(connFactory)
        {
        }

        public IEnumerable<FarmerInfo> Seek(string name, string fiscalCode)
        {
            return DbUtil.ExecuteReader<FarmerInfo>(
            @"select top 5 * from farmerinfos where 
            (@name is null or name like @name+'%')
            and (@fiscalCode is null or fiscalCode = @fiscalCode)", 
                new {name = name.Value(), fiscalCode = fiscalCode.Value()}, Cs);
        }
    }
}