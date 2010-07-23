using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using ILCalc;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class DossierService : IDossierService
    {
        private readonly IDossierRepo repo;
        private readonly IFieldsetService fservice;
        private readonly IUberRepo<FieldValue> fvRepo;
        private readonly IUberRepo<Indicator> iRepo;
        private readonly IUberRepo<IndicatorValue> ivRepo;

        public DossierService(IDossierRepo repo, IFieldsetService fservice, IUberRepo<FieldValue> fvRepo, IUberRepo<Indicator> iRepo, IUberRepo<IndicatorValue> ivRepo)
        {
            this.repo = repo;
            this.ivRepo = ivRepo;
            this.iRepo = iRepo;
            this.fvRepo = fvRepo;
            this.fservice = fservice;

        }

        public IPageable<Dossier> GetPageable(int page, int pageSize)
        {
            return repo.GetPageable(page, pageSize);
        }

        public int Create(Dossier o)
        {
            var fs = fservice.GetActive();
            if (fs == null) throw new AsmsEx("la moment nu exista nici un set de campuri activ");
            o.FieldsetId = fs.Id;
            o.StateId = (int)DossierStates.Registered;
            return repo.Insert(o);
        }

        public Dossier Get(int id)
        {
            return repo.Get(id);
        }

        public void Go(IEnumerable<FieldValue> fieldValues)
        {
            using (var scope = new TransactionScope())
            {
                var dossier = repo.Get(fieldValues.First().DossierId);
                if (dossier.StateId != (int)DossierStates.Registered) throw new AsmsEx("acest dosar deja are valori pentru campuri");

                if (!fieldValues.All(o => fvRepo.InsertNoIdentity(o) == 1)) throw new AsmsEx("nu pot salva valorile");

                var indicators = iRepo.GetWhere(new { dossier.FieldsetId }).ToList();

                var indicatorValues = indicators.Select(o =>
                    new IndicatorValue
                        {
                            DossierId = dossier.Id,
                            IndicatorId = o.Id
                        }).ToList();

                var calc = new CalcContext<decimal>();

                foreach (var v in fieldValues)
                {
                    calc.Constants.Add("c" + v.FieldId, v.Value);
                }

                foreach (var iv in indicatorValues)
                {
                    var iv1 = iv;
                    iv.Value =
                        calc.Evaluate(indicators.Where(o => o.Id == iv1.IndicatorId).Single().Formula);
                }

                if (!indicatorValues.All(o => ivRepo.InsertNoIdentity(o) == 1)) throw new AsmsEx("nu pot salva indicatorii");
                if (repo.ChangeState(dossier.Id, (int)DossierStates.HasIndicators) != 1) throw new AsmsEx("nu pot schimba starea dosarului");
                scope.Complete();
            }
        }
    }
}