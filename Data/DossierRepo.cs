using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class DossierRepo : Repo<Dossier>, IDossierRepo
    {
        public DossierRepo(IConnectionFactory connFactory) : base(connFactory)
        {
        }

        public int ChangeState(int id, int stateId)
        {
            return DbUtil.ExecuteNonQuerySp("changeDossierState", Cs, new {id, stateId});
        }


    }
}