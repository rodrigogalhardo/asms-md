using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class DossierRepo : UberRepo<Dossier>, IDossierRepo
    {
        public DossierRepo(IConnectionFactory connFactory) : base(connFactory)
        {
        }

        public int ChangeState(int id, int stateId)
        {
            return DbUtil.ExecuteNonQuerySp(new {id, stateId}, Cs, "changeDossierState");
        }


    }
}