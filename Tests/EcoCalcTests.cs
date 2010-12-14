using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Service;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    public class EcoCalcTests
    {
        private IEcoCalc c;

        [SetUp]
        public void SetUp()
        {
            c = new EcoCalc();
        }

        [Test]
        public void ShouldCalculateIndicatorValues()
        {
            var ivs = c.CalculateIndicatorValues(
                new Dossier { Id = 3 },
                new[]
                    {
                        new FieldValue {FieldId = 1, Value = 10},
                        new FieldValue {FieldId = 2, Value = 5}
                    },
                new[] {
                          new Indicator {Id = 4, Formula = "c1+c2"},
                          new Indicator{Id = 5, Formula = "c1*c2"}
                      }
                );
            ivs.Count().ShouldEqual(2);
            ivs.Where(o => o.IndicatorId == 4).Single().Value.ShouldEqual(15);
            ivs.Where(o => o.IndicatorId == 5).Single().Value.ShouldEqual(50);
        }

        [Test]
        public void ShouldCalculateCoefficientsValues()
        {
            var cvs = c.CalculateCoefficientValues(
                new[] { 
                    new IndicatorValue { DossierId = 1, IndicatorId = 11, Value = 3 },
                    new IndicatorValue { DossierId = 1, IndicatorId = 12, Value = 4 },
                    new IndicatorValue { DossierId = 1, IndicatorId = 13, Value = 5 },
                    new IndicatorValue { DossierId = 2, IndicatorId = 11, Value = 1 },
                    new IndicatorValue { DossierId = 2, IndicatorId = 12, Value = 2 },
                    new IndicatorValue { DossierId = 2, IndicatorId = 13, Value = 3 },
                },
                new[] {
                        new Dossier { Id = 1 },
                        new Dossier { Id = 2 },
                    },
                new[] { 
                    new Coefficient { Id = 1, Formula = "suma(i11)+i12+i13"}, 
                    new Coefficient { Id = 2, Formula = "suma(i13)-i13"}, 
                });

            cvs.Count().ShouldEqual(4);
            cvs.Where(o => o.CoefficientId == 1 && o.DossierId == 1).Single().Value.ShouldEqual(13);
            cvs.Where(o => o.CoefficientId == 2 && o.DossierId == 1).Single().Value.ShouldEqual(3);
            cvs.Where(o => o.CoefficientId == 1 && o.DossierId == 2).Single().Value.ShouldEqual(9);
            cvs.Where(o => o.CoefficientId == 2 && o.DossierId == 2).Single().Value.ShouldEqual(5);
        }

        [Test]
        public void ShouldNotAllowDossiersFromMultipleFieldsets()
        {
            Assert.Throws<AsmsEx>(() => c.CalculateCoefficientValues(A.Fake<IEnumerable<IndicatorValue>>(),
                                                               new[] {
                                                                   new Dossier {FieldsetId = 1},
                                                                   new Dossier {FieldsetId = 2},
                                                               },
                                                               A.Fake<IEnumerable<Coefficient>>()));
        } 
        
        [Test]
        public void ShouldNotAllowDossiersFromMultipleMeasuresets()
        {
            Assert.Throws<AsmsEx>(() => c.CalculateCoefficientValues(A.Fake<IEnumerable<IndicatorValue>>(),
                                                               new[] {
                                                                   new Dossier {MeasuresetId = 1},
                                                                   new Dossier {MeasuresetId = 2},
                                                               },
                                                               A.Fake<IEnumerable<Coefficient>>()));
        }
    }
}