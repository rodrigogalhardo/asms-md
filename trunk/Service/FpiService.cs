using System.Linq;
using System.Transactions;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Data;

namespace MRGSP.ASMS.Service
{
    public class FpiService : Repo<Fpi>, IFpiService
    {
        private readonly IDossierRepo dossierRepo;
        private readonly IFpiRepo r;
        private readonly IUniRepo u;
        private readonly IIndicatorValueRepo ivr;
        private readonly IEcoCalc ecoCalc;

        public FpiService(IConnectionFactory connFactory, IDossierRepo dossierRepo, IFpiRepo r, IUniRepo u, IIndicatorValueRepo ivr, IEcoCalc ecoCalc)
            : base(connFactory)
        {
            this.dossierRepo = dossierRepo;
            this.ecoCalc = ecoCalc;
            this.ivr = ivr;
            this.u = u;
            this.r = r;
        }

        public void ChangeAmount(int id, decimal amount, decimal amountm)
        {
            using (var t = new TransactionScope())
            {
                var payed = r.GetAmountPayed(id);
                (amount < payed).B("suma introdusa este mai mica decat suma deja autorizata spre plata");
                r.UpdateWhatWhere(new { amount, amountm }, new { id });
                Rerank(id);
                t.Complete();
            }
        }

        public void Create(Fpi o)
        {
            o.State = FpiState.Contest;
            r.InsertNoIdentity(o);
        }

        public void PreviousGoAgreement(int id)
        {
            var o = r.Get(id);
            var p = r.GetPrevious(o);
            if(p!=null) GoAgreement(p.Id);
        }

        public void GoAgreement(int id)
        {
            var o = r.Get(id);
            if (o.State != FpiState.Contest) return;

            using (var t = new TransactionScope())
            {
                r.UpdateWhatWhere(new {State = FpiState.Agreement}, new {o.Id});
                var p = r.GetPrevious(o);
                if(p != null && p.State != FpiState.Sealed) Seal(p.Id);
                t.Complete();
            }
        }

        public void Seal(int id)
        {
            var o = r.Get(id);
            if (o.State == FpiState.Sealed) return;

            var p = r.GetPrevious(o);

            using (var t = new TransactionScope())
            {
                r.UpdateWhatWhere(new {State = FpiState.Sealed}, new {o.Id});
                if (p != null && p.State != FpiState.Sealed) Seal(p.Id);
                t.Complete();
            }
        }

        public void Recalculate(int fpiId)
        {
            dossierRepo.RollbackToIndicators(fpiId);

            CalculateCoefficients(fpiId);

            Rank(fpiId);
        }

        /// <summary>
        /// calculate coefficients for a specific measureset/measure/month and change the state of the dossiers
        /// </summary>
        private void CalculateCoefficients(int fpiId)
        {
            var fpi = Get(fpiId);
            var dossiers = dossierRepo.GetBy(fpi.MeasuresetId, fpi.MeasureId, fpi.Month, (int)DossierStates.HasIndicators).ToArray();
            if (dossiers.Count() == 0) return;
            var measure = u.Get<Measure>(fpi.MeasureId);

            using (var t = new TransactionScope())
            {
                if (!measure.NoContest)
                {
                    var indicatorValues = ivr.GetBy(fpi.Id).ToArray();
                    var coefficients = u.GetWhere<Coefficient>(new { dossiers.First().FieldsetId }).ToList();
                    var coefficientValues = ecoCalc.CalculateCoefficientValues(indicatorValues, dossiers, coefficients);
                    coefficientValues.All(o => u.InsertNoIdentity(o) == 1);
                }

                dossiers.All(o => dossierRepo.ChangeState(o.Id, DossierStates.HasCoefficients) == 1);
                t.Complete();
            }
        }

        public void Rerank(int fpiId)
        {
            dossierRepo.RollbackWinners(fpiId);

            Rank(fpiId);
        }

        /// <summary>
        /// closes previous fpis and moves their eligible (state = has_coeffiecients) dossiers to this one
        /// ranks the dossiers according to their value and changes the state to the winners
        /// </summary>
        /// <param name="fpiId">the fpi</param>
        public void Rank(int fpiId)
        {
            var fpi = Get(fpiId);
            fpi.Amount -= r.GetAmountPayed(fpiId);

            using (var t = new TransactionScope())
            {
                PreviousGoAgreement(fpi.Id);

                if (fpi.State == FpiState.Contest)
                    dossierRepo.MoveToFpi(fpiId);

                var dossiers = dossierRepo.GetForRanking(fpi.MeasuresetId, fpi.MeasureId, fpi.Month)
                    .OrderByDescending(o => o.Value).ToArray();

                foreach (var d in dossiers)
                {
                    fpi.Amount -= d.AmountPayed;
                    if (fpi.Amount < 0) break;
                    dossierRepo.UpdateWhatWhere(new { d.Value, StateId = DossierStates.Winner }, new { d.Id });
                }

                t.Complete();
            }
        }
    }
}