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
        private readonly IDossierRepo dRepo;
        private readonly IFieldsetService fservice;
        private readonly IEcoCalc ecoCalc;
        private readonly IIndicatorValueRepo ivRepo;
        private readonly IMeasureRepo mRepo;
        private readonly IRepo<FieldValue> fvRepo;
        private readonly IRepo<CoefficientValue> cvRepo;

        public DossierService(
            IDossierRepo dRepo,
            IFieldsetService fservice,
            IMeasureRepo mRepo,
            IEcoCalc ecoCalc,
            IIndicatorValueRepo ivRepo, IRepo<FieldValue> fvRepo, IRepo<CoefficientValue> cvRepo)
        {
            this.dRepo = dRepo;
            this.cvRepo = cvRepo;
            this.fvRepo = fvRepo;
            this.ivRepo = ivRepo;
            this.ecoCalc = ecoCalc;
            this.mRepo = mRepo;
            this.fservice = fservice;
        }

        public void GoCoefficients()
        {
            using (var scope = new TransactionScope())
            {
                foreach (var mId in mRepo.GetUsedIn(DateTime.Now.AddMonths(-1)))
                    CalculateCoefficients(mId, DateTime.Now.AddMonths(-1));
                scope.Complete();
            }
        }

        private void CalculateCoefficients(int measureId, DateTime month)
        {
            var ds = dRepo.GetBy(measureId, month);
            var m = mRepo.Get(measureId);
            if (!m.NoContest)
            {
                var cvs = ecoCalc.CalculateCoefficientValues(measureId, month, ds);

                if (!cvs.All(o => cvRepo.InsertNoIdentity(o) == 1))
                    throw new AsmsEx("nu pot salva valorile coeficientilor");
            }

            if (!ds.All(o => dRepo.ChangeState(o.Id, (int)DossierStates.HasCoefficients) == 1))
                throw new AsmsEx("nu pot schimba starea dosarului");
        }

        public void GoIndicators(IEnumerable<FieldValue> fieldValues)
        {
            using (var scope = new TransactionScope())
            {
                var dossier = dRepo.Get(fieldValues.First().DossierId);
                if (dossier.StateId != (int)DossierStates.Registered) throw new AsmsEx("acest dosar deja are valori pentru campuri");

                if (!fieldValues.All(o => fvRepo.InsertNoIdentity(o) == 1)) throw new AsmsEx("nu pot salva valorile");

                var indicatorValues = ecoCalc.CalculateIndicatorValues(fieldValues, dossier);

                if (!indicatorValues.All(o => ivRepo.InsertNoIdentity(o) == 1))
                    throw new AsmsEx("nu pot salva valorile indicatorilor");
                if (dRepo.ChangeState(fieldValues.First().DossierId, (int)DossierStates.HasIndicators) != 1)
                    throw new AsmsEx("nu pot schimba starea dosarului");
                scope.Complete();
            }
        }

        public IPageable<Dossier> GetPageable(int page, int pageSize)
        {
            return dRepo.GetPageable(page, pageSize);
        }

        public int Create(Dossier o)
        {
            var fs = fservice.GetActive();
            if (fs == null) throw new AsmsEx("la moment nu exista nici un set de campuri activ");
            var measure = mRepo.Get(o.MeasureId);

            o.FieldsetId = fs.Id;
            o.StateId = (int)(measure.NoContest ? DossierStates.HasIndicators : DossierStates.Registered);
            return dRepo.Insert(o);
        }

        public bool IsNoContest(int id)
        {
            var d = dRepo.Get(id);
            return mRepo.Get(d.MeasureId).NoContest;
        }

        public Dossier Get(int id)
        {
            return dRepo.Get(id);
        }
    }
}