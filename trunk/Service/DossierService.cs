using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using Omu.Awesome.Core;

namespace MRGSP.ASMS.Service
{
    public class DossierService : IDossierService
    {
        private readonly IDossierRepo dossierRepo;
        private readonly IFieldsetService fieldsetService;
        private readonly IEcoCalc ecoCalc;
        private readonly IMeasuresetService measuresetService;
        private readonly IFpiRepo fpiRepo;
        private readonly IDossierRulesService rules;
        private readonly IFpiService fpiService;
        private readonly IUniRepo u;

        public DossierService(IDossierRepo dossierRepo, IFieldsetService fieldsetService, IEcoCalc ecoCalc, IMeasuresetService measuresetService, IFpiRepo fpiRepo, IDossierRulesService rules, IFpiService fpiService, IUniRepo u)
        {
            this.dossierRepo = dossierRepo;
            this.u = u;
            this.fpiService = fpiService;
            this.fieldsetService = fieldsetService;
            this.ecoCalc = ecoCalc;
            this.measuresetService = measuresetService;
            this.fpiRepo = fpiRepo;
            this.rules = rules;
        }

        public void Disqualify(int id, string reason)
        {
            rules.MustNotBe(id, DossierStates.Authorized);

            using (var scope = new TransactionScope())
            {
                u.Insert(new Disqualifier { DossierId = id, Reason = reason });
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
            rules.MustBe(id, DossierStates.Winner, DossierStates.HasCoefficients);
            var dossier = dossierRepo.Get(id);

            using (var scope = new TransactionScope())
            {
                dossierRepo.UpdateWhatWhere(new { AmountPayed = amountPayed }, new { id });

                var fpi = fpiRepo.GetWhere(new { id = dossier.FpiId }).Single();
                fpiService.Rerank(fpi.Id);
                scope.Complete();
            }
        }

        public void Authorize(int dossierId)
        {
            rules.MustBe(dossierId, DossierStates.Winner);

            var dossier = dossierRepo.Get(dossierId);
            var fpi = u.Get<Fpi>(dossier.FpiId);

            using (var scope = new TransactionScope())
            {
                if ((fpi.Amount - (fpiRepo.GetAmountPayed(fpi.Id) + dossier.AmountPayed)) < 0)
                    throw new AsmsEx("nu au mai ramas bani in aceasta luna pentru acest dosar");

                dossierRepo.ChangeState(dossierId, DossierStates.Authorized);
                dossierRepo.UpdateWhatWhere(new { FpiId = fpi.Id }, new { dossier.Id });
                scope.Complete();
            }
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

                u.DeleteWhere<FieldValue>(new { dossierId });

                SaveFieldValues(fieldValues, dossierId);

                u.DeleteWhere<IndicatorValue>(new { dossierId });
                SaveIndicators(dossierId);

                fpiService.Recalculate(d.FpiId);

                scope.Complete();
            }
        }


        private void SaveFieldValues(IEnumerable<FieldValue> fieldValues, int dossierId)
        {
            (!fieldValues.All(o => u.InsertNoIdentity(o) == 1)).B("nu pot salva valorile");
            dossierRepo.ChangeState(dossierId, DossierStates.HasFieldValues);
        }
        
        /// <summary>
        /// calculate indicator values
        /// save indicator values
        /// change the state
        /// </summary>
        private void SaveIndicators(int dossierId)
        {
            var dossier = dossierRepo.Get(dossierId);
            var fieldValues = u.GetWhere<FieldValue>(new { dossierId }).ToList();
            using (var scope = new TransactionScope())
            {
                var indicators = u.GetWhere<Indicator>(new { dossier.FieldsetId }).ToList();
                var indicatorValues = ecoCalc.CalculateIndicatorValues(dossier, fieldValues, indicators);

                if (!indicatorValues.All(o => u.InsertNoIdentity(o) == 1))
                    throw new AsmsEx("nu pot salva valorile indicatorilor");

                dossierRepo.ChangeState(dossierId, DossierStates.HasIndicators);
                scope.Complete();
            }
        }

        /// <summary>
        /// create a new dossier, if it can be created
        /// </summary>
        /// <param name="o">the dossier to be created</param>
        /// <returns>the id of the new dossier</returns>
        public int Create(Dossier o)
        {
            var fs = fieldsetService.GetActive();
            var ms = measuresetService.GetActive();
            (fs == null).B("la moment nu exista nici un set de campuri activ");
            (ms == null).B("la moment nu exista nici un se de masuri activ");
            var measure = u.Get<Measure>(o.MeasureId);

            o.FieldsetId = fs.Id;
            o.MeasuresetId = ms.Id;
            o.AmountPayed = o.AmountRequested;
            o.StateId = measure.NoContest ? DossierStates.HasIndicators : DossierStates.Registered;
            o.CreatedDate = DateTime.Now;


            var fpis = fpiRepo.GetWhere(new { o.MeasureId, o.MeasuresetId, o.CreatedDate.Month });
           (fpis.Count() == 0).B("la moment in planul financiar pentru aceasta luna si masura nu a fost stabilita nici o suma");
            var fpi = fpis.Single();
            o.FpiId = fpi.Id;

            int id;
            using (var scope = new TransactionScope())
            {
                id = dossierRepo.Insert(o);
                var d = dossierRepo.Get(id);

                d.Code = d.CreatedDate.AddYears(-2000).Year + d.CreatedDate.Month.ToString("00") + u.Get<District>(d.DistrictId.Value).Code + d.Id;
                dossierRepo.UpdateWhatWhere(new { d.Code }, new { d.Id });
                scope.Complete();
            }
            return id;
        }

        /// <summary>
        /// determines whether the dossier whith the specified id participates in contest
        /// </summary>
        public bool IsNoContest(int id)
        {
            var d = dossierRepo.Get(id);
            return u.Get<Measure>(d.MeasureId).NoContest;
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