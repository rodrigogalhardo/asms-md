using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class DossierService : IDossierService
    {
        private readonly IDossierRepo dossierRepo;
        private readonly IFieldsetService fieldsetService;
        private readonly IEcoCalc ecoCalc;
        private readonly IIndicatorValueRepo ivRepo;
        private readonly IMeasureRepo measureRepo;
        private readonly IRepo<FieldValue> fvRepo;
        private readonly IRepo<CoefficientValue> cvRepo;
        private readonly IMeasuresetService measuresetService;
        private readonly IFpiRepo fpiRepo;
        private readonly IRepo<Disqualifier> disqualifierRepo;
        private readonly IRepo<Coefficient> coefficientRepo;
        private readonly IRepo<Indicator> indicatorRepo;

        public DossierService(
            IDossierRepo dossierRepo,
            IFieldsetService fieldsetService,
            IMeasureRepo measureRepo,
            IEcoCalc ecoCalc,
            IIndicatorValueRepo ivRepo,
            IRepo<FieldValue> fvRepo,
            IRepo<CoefficientValue> cvRepo,
            IMeasuresetService measuresetService,
            IFpiRepo fpiRepo,
            IRepo<Disqualifier> disqualifierRepo,
            IRepo<Coefficient> coefficientRepo,
            IRepo<Indicator> indicatorRepo)
        {
            this.dossierRepo = dossierRepo;
            this.indicatorRepo = indicatorRepo;
            this.coefficientRepo = coefficientRepo;
            this.disqualifierRepo = disqualifierRepo;
            this.fpiRepo = fpiRepo;
            this.measuresetService = measuresetService;
            this.cvRepo = cvRepo;
            this.fvRepo = fvRepo;
            this.ivRepo = ivRepo;
            this.ecoCalc = ecoCalc;
            this.measureRepo = measureRepo;
            this.fieldsetService = fieldsetService;
        }

        public void Disqualify(int id, string reason)
        {
            using (var scope = new TransactionScope())
            {
                disqualifierRepo.Insert(new Disqualifier { DossierId = id, Reason = reason });
                dossierRepo.UpdateWhatWhere(new { Disqualified = true }, new { Id = id });
                scope.Complete();
            }
        }

        public IEnumerable<Dossier> GetForTop(int measuresetId, int measureId, int month)
        {
            return dossierRepo.GetBy(measuresetId, measureId, month, (int)DossierStates.Winner)
                .Union(dossierRepo.GetBy(measuresetId, measureId, month, (int)DossierStates.HasCoefficients))
                .OrderByDescending(o => o.Value);
        }

        public void ChangeAmountPayed(int id, decimal amountPayed)
        {
            var dossier = dossierRepo.Get(id);
            if (dossier.StateId != DossierStates.Winner)
                throw new AsmsEx("la moment pentru acest dosar nu poate fi schimbata suma platita, el trebuie sa fie in stare 'castigator'");

            using (var scope = new TransactionScope())
            {
                dossierRepo.UpdateWhatWhere(new { AmountPayed = amountPayed }, new { id });

                var fpi = fpiRepo.GetWhere(new { id = dossier.FpiId }).Single();
                dossierRepo.RollbackWinners(fpi.Id);

                Rank(fpi.Id);
                scope.Complete();
            }
        }

        public void Authorize(int dossierId)
        {
            var dossier = dossierRepo.Get(dossierId);
            if (dossier.StateId != DossierStates.Winner)
                throw new AsmsEx("acest dosar nu este castigator");

            var fpi = fpiRepo.Get(dossier.FpiId);

            using (var scope = new TransactionScope())
            {
                if ((fpi.Amount - (fpiRepo.GetAmountPayed(fpi.Id) + dossier.AmountPayed)) < 0)
                    throw new AsmsEx("nu au mai ramas bani in aceasta luna pentru acest dosar");

                dossierRepo.ChangeState(dossierId, DossierStates.Authorized);
                dossierRepo.UpdateWhatWhere(new { FpiId = fpi.Id }, new { dossier.Id });
                scope.Complete();
            }
        }

        public void Recalculate(int fpiId)
        {
            dossierRepo.RollbackToIndicators(fpiId);

            CalculateCoefficients(fpiId);

            Rank(fpiId);
        }

        public void Init(IEnumerable<FieldValue> fieldValues, int dossierId)
        {
            using (var scope = new TransactionScope())
            {
                SaveFieldValues(fieldValues, dossierId);
                SaveIndicators(dossierId);
                scope.Complete();
            }
        }

        public void ChangeFieldValues(IEnumerable<FieldValue> fieldValues, int dossierId)
        {
            using (var scope = new TransactionScope())
            {
                var d = dossierRepo.Get(dossierId);

                fvRepo.DeleteWhere(new { dossierId });

                SaveFieldValues(fieldValues, dossierId);

                ivRepo.DeleteWhere(new { dossierId });
                SaveIndicators(dossierId);

                Recalculate(d.FpiId);

                scope.Complete();
            }
        }

        private void Rank(int fpiId)
        {
            var fpi = fpiRepo.GetWhere(new { id = fpiId }).Single();
            fpi.Amount -= fpiRepo.GetAmountPayed(fpi.Id);

            using (var scope = new TransactionScope())
            {
                dossierRepo.CloseFpis(fpi.Id);
                if (!fpi.Closed)
                    dossierRepo.UpdateToFpi(fpi.Id);

                var dossiers = dossierRepo.GetForRanking(fpi.MeasuresetId, fpi.MeasureId, fpi.Month)
                    .OrderByDescending(o => o.Value).ToArray();

                foreach (var d in dossiers)
                {
                    fpi.Amount -= d.AmountPayed;
                    if (fpi.Amount <= 0) break;
                    dossierRepo.UpdateWhatWhere(new { d.Value, StateId = DossierStates.Winner }, new { d.Id });
                }

                scope.Complete();
            }
        }

        private void SaveFieldValues(IEnumerable<FieldValue> fieldValues, int dossierId)
        {
            if (!fieldValues.All(o => fvRepo.InsertNoIdentity(o) == 1)) throw new AsmsEx("nu pot salva valorile");
            dossierRepo.ChangeState(dossierId, DossierStates.HasFieldValues);
        }

        /// <summary>
        /// calculate coefficients for a specific measureset/measure/month
        /// </summary>
        private void CalculateCoefficients(int fpiId)
        {
            var fpi = fpiRepo.Get(fpiId);
            var dossiers = dossierRepo.GetBy(fpi.MeasuresetId, fpi.MeasureId, fpi.Month, (int)DossierStates.HasIndicators).ToArray();
            if (dossiers.Count() == 0) return;
            var measure = measureRepo.Get(fpi.MeasureId);

            using (var scope = new TransactionScope())
            {
                if (!measure.NoContest)
                {
                    var indicatorValues = ivRepo.GetBy(fpi.Id).ToArray();
                    var coefficients = coefficientRepo.GetWhere(new { dossiers.First().FieldsetId }).ToList();
                    var coefficientValues = ecoCalc.CalculateCoefficientValues(indicatorValues, dossiers, coefficients);
                    coefficientValues.All(o => cvRepo.InsertNoIdentity(o) == 1);
                }

                dossiers.All(o => dossierRepo.ChangeState(o.Id, DossierStates.HasCoefficients) == 1);
                scope.Complete();
            }
        }

        /// <summary>
        /// calculate indicator values
        /// save indicator values
        /// change the state
        /// </summary>
        private void SaveIndicators(int dossierId)
        {
            var dossier = dossierRepo.Get(dossierId);
            var fieldValues = fvRepo.GetWhere(new { dossierId }).ToList();
            using (var scope = new TransactionScope())
            {
                var indicators = indicatorRepo.GetWhere(new { dossier.FieldsetId }).ToList();
                var indicatorValues = ecoCalc.CalculateIndicatorValues(dossier, fieldValues, indicators);

                if (!indicatorValues.All(o => ivRepo.InsertNoIdentity(o) == 1))
                    throw new AsmsEx("nu pot salva valorile indicatorilor");

                dossierRepo.ChangeState(dossierId, DossierStates.HasIndicators);
                scope.Complete();
            }
        }

        public int Create(Dossier o)
        {
            var fs = fieldsetService.GetActive();
            var ms = measuresetService.GetActive();
            if (fs == null) throw new AsmsEx("la moment nu exista nici un set de campuri activ");
            if (ms == null) throw new AsmsEx("la moment nu exista nici un se de masuri activ");
            var measure = measureRepo.Get(o.MeasureId);

            o.FieldsetId = fs.Id;
            o.MeasuresetId = ms.Id;
            o.AmountPayed = o.AmountRequested;
            o.StateId = measure.NoContest ? DossierStates.HasIndicators : DossierStates.Registered;
            o.CreatedDate = DateTime.Now;


            var fpis = fpiRepo.GetWhere(new { o.MeasureId, o.MeasuresetId, o.CreatedDate.Month });
            if (fpis.Count() == 0) throw new AsmsEx("la moment in planul financiar pentru aceasta luna si masura nu a fost stabilita nici o suma");
            var fpi = fpis.Single();
            o.FpiId = fpi.Id;

            return dossierRepo.Insert(o);
        }

        /// <summary>
        /// determines wheter the dossier whith the specified id participates in contest
        /// </summary>
        public bool IsNoContest(int id)
        {
            var d = dossierRepo.Get(id);
            return measureRepo.Get(d.MeasureId).NoContest;
        }

        public IPageable<Dossier> GetPageable(int page, int pageSize)
        {
            return dossierRepo.GetPageable(page, pageSize);
        }

        public Dossier Get(int id)
        {
            return dossierRepo.Get(id);
        }
    }
}