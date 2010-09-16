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

        public int ChangeState(int id, DossierStates stateId)
        {
            return DbUtil.ExecuteNonQuerySp("changeDossierState", new { id, stateId }, Cs);
        }

        public IEnumerable<Dossier> GetBy(int measuresetId, int measureId, int month, int? stateId = null)
        {
            return DbUtil.ExecuteReaderSp<Dossier>("getDossiers", new { measuresetId, measureId, month, stateId }, Cs);
        }

        public IEnumerable<RankedDossier> GetForRanking(int measuresetId, int measureId, int month)
        {
            return DbUtil.ExecuteReaderSp<RankedDossier>("getDossiersForRanking", new { measuresetId, measureId, month }, Cs);
        }

        

        public int RollbackWinners(int fpiId)
        {
            return DbUtil.ExecuteNonQuerySp("rollbackWinners", new {fpiId}, Cs);
        }

        public void RollbackToIndicators(int fpiId)
        {
            DbUtil.ExecuteNonQuerySp("rollbackToIndicators", new {fpiId}, Cs);
        }

        public void UpdateToFpi(int fpiId)
        {
            DbUtil.ExecuteNonQuerySp("updateToFpi", new {fpiId}, Cs);
        }

        public void CloseFpis(int fpiId)
        {
            DbUtil.ExecuteNonQuerySp("closeFpis", new {fpiId}, Cs);
        }
    }
}