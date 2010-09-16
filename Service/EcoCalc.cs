using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using ILCalc;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class EcoCalc : IEcoCalc
    {
        public IEnumerable<CoefficientValue> CalculateCoefficientValues(IEnumerable<IndicatorValue> indicatorValues, IEnumerable<Dossier> dossiers, IEnumerable<Coefficient> coefficients)
        {
            var coefficientValues = new List<CoefficientValue>();

            if (dossiers.Select(o => o.FieldsetId).Distinct().Count() > 1)
                throw new AsmsEx("in luna respectiva sunt dosare inregistrate la diferite seturi de campuri");

            if (dossiers.Select(o => o.MeasuresetId).Distinct().Count() > 1)
                throw new AsmsEx("in luna respectiva sunt dosare inregistrate la diferite seturi de masuri");

            //calculate each coefficient for all dossiers
            foreach (var coefficient in coefficients)
            {
                EvalSums(coefficient, indicatorValues);

                var calc = new CalcContext<decimal>();
                foreach (var dossier in dossiers)
                {
                    calc.Constants.Clear();
                    foreach (var indicatorValue in indicatorValues.Where(o => o.DossierId == dossier.Id))
                    {
                        calc.Constants.Add("i" + indicatorValue.IndicatorId, indicatorValue.Value);
                    }

                    coefficientValues.Add(new CoefficientValue
                                {
                                    CoefficientId = coefficient.Id,
                                    DossierId = dossier.Id,
                                    Value = Zero(() => calc.Evaluate(coefficient.Formula)),
                                });
                }
            }

            return coefficientValues;
        }

        public IEnumerable<IndicatorValue> CalculateIndicatorValues(Dossier dossier, IEnumerable<FieldValue> fieldValues, IEnumerable<Indicator> indicators)
        {
            var calc = new CalcContext<decimal>();

            foreach (var o in fieldValues)
            {
                calc.Constants.Add("c" + o.FieldId, o.Value);
            }

            var indicatorValues = indicators
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
                var x = f();
                if (x < 0) x = 0;
                return x;
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