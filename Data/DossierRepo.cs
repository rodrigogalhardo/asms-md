using System.Collections.Generic;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class DossierRepo : Repo<Dossier>, IDossierRepo
    {
        public DossierRepo(IConnectionFactory connFactory)
            : base(connFactory)
        {
        }

        public int ChangeState(int id, int stateId)
        {
            return DbUtil.ExecuteNonQuerySp("changeDossierState", Cs, new { id, stateId });
        }

        public IEnumerable<Dossier> GetBy(int measuresetId, int measureId, int month, int? stateId = null)
        {
            return DbUtil.ExecuteReaderSp<Dossier>("getDossiers", Cs, new { measuresetId, measureId, month, stateId });
        }

        public IEnumerable<RankedDossier> GetRankedDossiers(int measuresetId, int measureId, int month)
        {
            return DbUtil.ExecuteReaderSp<RankedDossier>("getRankedDossiers", Cs, new { measuresetId, measureId, month });
        }
    }
}