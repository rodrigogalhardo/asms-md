using System;
using System.Collections.Generic;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class UberRepo : BaseRepo, IUberRepo
    {
        public UberRepo(IConnectionFactory connFactory)
            : base(connFactory)
        {
        }

        public IEnumerable<CrossDistrictMeasure> GetCrossDistrictMeasure(DateTime date, int measuresetId)
        {
            return DbUtil.ExecuteReaderSp<CrossDistrictMeasure>("crossDistricMeasure", new { date, measuresetId }, Cs);
        }  
        
        public IEnumerable<CrossDistrictMeasureAmountPayed> GetCrossDistrictMeasureAmountPayed(DateTime date, int measuresetId)
        {
            return DbUtil.ExecuteReaderSp<CrossDistrictMeasureAmountPayed>("crossDistricMeasureAmountPayed", new { date, measuresetId }, Cs);
        }

        public IEnumerable<DossiersByDistrictReport> DossiersByDistrictReport(int year, int districtId)
        {
            return DbUtil.ExecuteReaderSp<DossiersByDistrictReport>("DossiersByDistrictReport", new { year, districtId }, Cs);
        }
    }
}