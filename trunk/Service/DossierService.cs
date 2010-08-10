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
        private readonly IFieldsetService fservice;
        private readonly IEcoCalc ecoCalc;
        private readonly IIndicatorValueRepo ivRepo;
        private readonly IMeasureRepo measureRepo;
        private readonly IRepo<FieldValue> fvRepo;
        private readonly IRepo<CoefficientValue> cvRepo;
        private readonly IMeasuresetService msService;
        private readonly IRepo<Fpi> fpiRepo;

        public DossierService(
            IDossierRepo dossierRepo,
            IFieldsetService fservice,
            IMeasureRepo measureRepo,
            IEcoCalc ecoCalc,
            IIndicatorValueRepo ivRepo,
            IRepo<FieldValue> fvRepo,
            IRepo<CoefficientValue> cvRepo,
            IMeasuresetService msService, IRepo<Fpi> fpiRepo)
        {
            this.dossierRepo = dossierRepo;
            this.fpiRepo = fpiRepo;
            this.msService = msService;
            this.cvRepo = cvRepo;
            this.fvRepo = fvRepo;
            this.ivRepo = ivRepo;
            this.ecoCalc = ecoCalc;
            this.measureRepo = measureRepo;
            this.fservice = fservice;
        }

        public IEnumerable<Dossier> GetForTop(int measuresetId, int measureId, int month)
        {
            return dossierRepo.GetBy(measuresetId, measureId, month, (int) DossierStates.Winner)
                .Union(dossierRepo.GetBy(measuresetId, measureId, month, (int)DossierStates.HasCoefficients))
                .OrderByDescending(o => o.Value);
        }

        public void Rank(int measuresetId, int measureId, int month)
        {
            var fpi = fpiRepo.GetWhere(new { measuresetId, measureId, month }).Single();
            var measureset = msService.Get(measuresetId);
            
            if (!fpi.Calculated && DateTime.Now.Month > month && DateTime.Now.Year >= measureset.Year)
                using (var scope = new TransactionScope())
                {
                    CalculateCoefficients(measuresetId, measureId, month);
                    fpi.Calculated = true;
                    fpiRepo.Update(fpi);
                  
                    var rds = dossierRepo.GetRankedDossiers(measuresetId, measureId, month)
                        .OrderByDescending(o => o.Value).ToArray();

                    foreach (var rd in rds)
                    {
                        fpi.Amount -= rd.AmountRequested;
                        
                        if(fpi.Amount >= 0) 
                        dossierRepo.UpdateWhatWhere(new {rd.Value, StateId = (int)DossierStates.Winner }, new {rd.Id});
                        else
                            dossierRepo.UpdateWhatWhere(new { rd.Value }, new { rd.Id });
                    }
                    
                    scope.Complete();
                }
        }

        public void CalculateCoefficients(int measuresetId, int measureId, int month)
        {
            var measureset = msService.Get(measuresetId);
            var dossiers = dossierRepo.GetBy(measuresetId, measureId, month, (int)DossierStates.HasIndicators).ToArray();
            var measure = measureRepo.Get(measureId);
            if (!measure.NoContest)
            {
                var cvs = ecoCalc.CalculateCoefficientValues(measureId, new DateTime(measureset.Year, month, 1), dossiers);

                if (!cvs.All(o => cvRepo.InsertNoIdentity(o) == 1))
                    throw new AsmsEx("nu pot salva valorile coeficientilor");
            }

            if (!dossiers.All(o => dossierRepo.ChangeState(o.Id, (int)DossierStates.HasCoefficients) == 1))
                throw new AsmsEx("nu pot schimba starea dosarului");
        }

        public void GoIndicators(IEnumerable<FieldValue> fieldValues)
        {
            using (var scope = new TransactionScope())
            {
                var dossier = dossierRepo.Get(fieldValues.First().DossierId);
                if (dossier.StateId != (int)DossierStates.Registered) throw new AsmsEx("acest dosar deja are valori pentru campuri");

                if (!fieldValues.All(o => fvRepo.InsertNoIdentity(o) == 1)) throw new AsmsEx("nu pot salva valorile");

                var indicatorValues = ecoCalc.CalculateIndicatorValues(fieldValues, dossier);

                if (!indicatorValues.All(o => ivRepo.InsertNoIdentity(o) == 1))
                    throw new AsmsEx("nu pot salva valorile indicatorilor");
                if (dossierRepo.ChangeState(fieldValues.First().DossierId, (int)DossierStates.HasIndicators) != 1)
                    throw new AsmsEx("nu pot schimba starea dosarului");
                scope.Complete();
            }
        }

        public IPageable<Dossier> GetPageable(int page, int pageSize)
        {
            return dossierRepo.GetPageable(page, pageSize);
        }

        public int Create(Dossier o)
        {
            var fs = fservice.GetActive();
            var ms = msService.GetActive();
            if (fs == null) throw new AsmsEx("la moment nu exista nici un set de campuri activ");
            if (ms == null) throw new AsmsEx("la moment nu exista nici un se de masuri activ");
            var measure = measureRepo.Get(o.MeasureId);

            o.FieldsetId = fs.Id;
            o.MeasuresetId = ms.Id;
            o.StateId = (int)(measure.NoContest ? DossierStates.HasIndicators : DossierStates.Registered);
            return dossierRepo.Insert(o);
        }

        public bool IsNoContest(int id)
        {
            var d = dossierRepo.Get(id);
            return measureRepo.Get(d.MeasureId).NoContest;
        }

        public Dossier Get(int id)
        {
            return dossierRepo.Get(id);
        }
    }
}