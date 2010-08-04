using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using ILCalc;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class EcoCalc : IEcoCalc
    {
        private readonly IRepo<Indicator> iRepo;
        private readonly IIndicatorValueRepo ivRepo;
        private readonly IRepo<Coefficient> cRepo;

        public EcoCalc(IRepo<Indicator> iRepo, IIndicatorValueRepo ivRepo, IRepo<Coefficient> cRepo)
        {
            this.iRepo = iRepo;
            this.ivRepo = ivRepo;
            this.cRepo = cRepo;
        }

        public IEnumerable<CoefficientValue> CalculateCoefficientValues(int measureId, DateTime month, IEnumerable<Dossier> dossiers)
        {
            var ivs = ivRepo.GetBy(measureId, month);
            var cvs = new List<CoefficientValue>();

            foreach (var fsid in dossiers.Select(o => o.FieldsetId).Distinct())
            {
                var cc = cRepo.GetWhere(new { FieldsetId = fsid });

                //calculate each coefficient for all dossiers
                foreach (var c in cc)
                {
                    EvalSums(c, ivs);

                    var calc = new CalcContext<decimal>();
                    foreach (var d in dossiers.Where(o => o.FieldsetId == fsid))
                    {
                        calc.Constants.Clear();
                        foreach (var iv in ivs.Where(o => o.DossierId == d.Id))
                        {
                            calc.Constants.Add("i" + iv.IndicatorId, iv.Value);
                        }

                        cvs.Add(new CoefficientValue
                                    {
                                        CoefficientId = c.Id,
                                        DossierId = d.Id,
                                        Value = Zero(() => calc.Evaluate(c.Formula)),
                                    });
                    }
                }
            }

            return cvs;
        }

        public IEnumerable<IndicatorValue> CalculateIndicatorValues(IEnumerable<FieldValue> fieldValues, Dossier dossier)
        {
            var calc = new CalcContext<decimal>();

            fieldValues.AsParallel().ForAll(o => calc.Constants.Add("c" + o.FieldId, o.Value));

            var indicators = iRepo.GetWhere(new { dossier.FieldsetId }).ToList();

            var indicatorValues = indicators.AsParallel()
                .Select(
                indicator =>
                        new IndicatorValue
                            {
                                DossierId = dossier.Id,
                                IndicatorId = indicator.Id,
                                Value = Zero(() =>
                                    calc.Evaluate(
                                    indicators.Where(i => i.Id == indicator.Id).Single().Formula))
                            }).ToList();

            return indicatorValues;
        }

        private static decimal Zero(Func<decimal> f)
        {
            try
            {
                return f();
            }
            catch (DivideByZeroException)
            {
                return 0;
            }
        }

        public static void EvalSums(Coefficient c, IEnumerable<IndicatorValue> ivs)
        {
            var re = new Regex(@"suma\(i\d+\)");
            var mc = re.Matches(c.Formula);

            foreach (Match mt in mc)
            {
                var sum = Convert.ToInt32(mt.Value.Replace("suma(i", string.Empty).Replace(")", string.Empty));

                c.Formula = c.Formula.Replace(mt.Value,
                                              ivs.Where(o => o.IndicatorId == sum)
                                                  .Sum(o => o.Value).ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}