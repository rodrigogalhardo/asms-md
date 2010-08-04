using System;
using System.Linq;
using Moq;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Service;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    [TestFixture]
    public class EcoCalcTests
    {
        private EcoCalc ec;
        private Mock<IDossierRepo> dRepo;
        private Mock<IRepo<Indicator>> iRepo;
        private Mock<IIndicatorValueRepo> ivRepo;
        private Mock<IRepo<Coefficient>> cRepo;

        [SetUp]
        public void SetUp()
        {
            dRepo = new Mock<IDossierRepo>();
            iRepo = new Mock<IRepo<Indicator>>();
            ivRepo = new Mock<IIndicatorValueRepo>();
            cRepo = new Mock<IRepo<Coefficient>>();

            ec = new EcoCalc(iRepo.Object, ivRepo.Object, cRepo.Object);
        }

        [Test]
        public void CalculateCoefficientValuesShouldReturnThem()
        {
            var dt = new DateTime(2010, 3, 1);

            ivRepo.Setup(o => o.GetBy(3, dt)).Returns(
                new[]
                    {
                        new IndicatorValue { DossierId = 1, IndicatorId = 1, Value = 7 },
                        new IndicatorValue { DossierId = 1, IndicatorId = 2, Value = 3 },
                        new IndicatorValue { DossierId = 1, IndicatorId = 3, Value = 4 },
                        new IndicatorValue { DossierId = 4, IndicatorId = 4, Value = 9 },
                        new IndicatorValue { DossierId = 4, IndicatorId = 5, Value = 1 },
                        new IndicatorValue { DossierId = 5, IndicatorId = 4, Value = 2 },
                        new IndicatorValue { DossierId = 5, IndicatorId = 5, Value = 3 },
                    });



            cRepo.Setup(o => o.GetWhere(
                It.Is<object>(x => (int)x.GetType().GetProperty("FieldsetId").GetValue(x, null) == 3)))
                .Returns(new[]
                            {
                                new Coefficient {Id = 1, Formula = "i1+i2"},
                                new Coefficient {Id = 2, Formula = "i1+i2+i3"},
                                new Coefficient {Id = 3, Formula = "i3*i2-i1"},
                            });
            
            cRepo.Setup(o => o.GetWhere(
                It.Is<object>(x => (int)x.GetType().GetProperty("FieldsetId").GetValue(x, null) == 7)))
                .Returns(new[]
                            {
                                new Coefficient {Id = 4, Formula = "i4*i5"},
                                new Coefficient {Id = 5, Formula = "i4+i5"},
                            });


            var result = ec.CalculateCoefficientValues(3, dt, new[]
                                                         {
                                                             new Dossier { Id = 1, FieldsetId = 3},
                                                             new Dossier { Id = 4, FieldsetId = 7},
                                                             new Dossier { Id = 5, FieldsetId = 7},
                                                         });

            result.Count().IsEqualTo(7);
            result.Where(o => o.CoefficientId == 1).Single().Value.IsEqualTo(10);
            result.Where(o => o.CoefficientId == 2).Single().Value.IsEqualTo(14);
            result.Where(o => o.CoefficientId == 3).Single().Value.IsEqualTo(5);
            result.Where(o => o.CoefficientId == 4 && o.DossierId == 4).Single().Value.IsEqualTo(9);
            result.Where(o => o.CoefficientId == 5 && o.DossierId == 4).Single().Value.IsEqualTo(10);
            result.Where(o => o.CoefficientId == 4 && o.DossierId == 5).Single().Value.IsEqualTo(6);
            result.Where(o => o.CoefficientId == 5 && o.DossierId == 5).Single().Value.IsEqualTo(5);

        }

        [Test]
        public void CalculateIndicatorValuesShouldCalculateIndicatorsFromFieldValues()
        {
            iRepo.Setup(o => o.GetWhere(It.IsAny<object>())).Returns(
                new[]
                    {
                        new Indicator{Id = 1, Formula = "c1 + c2"},
                        new Indicator{Id = 2, Formula = "c2 + c3"},
                        new Indicator{Id = 3, Formula = "c3 + c4"},
                    }).Verifiable();

            var result = ec.CalculateIndicatorValues(
                new[] {
                    new FieldValue { FieldId = 1, Value = 3},
                    new FieldValue { FieldId = 2, Value = 1},
                    new FieldValue { FieldId = 3, Value = 2},
                    new FieldValue { FieldId = 4, Value = 5},
                },
                new Dossier { Id = 3, FieldsetId = 7 }).ToList();

            result.Where(o => o.IndicatorId == 1).Single().Value.IsEqualTo(4);
            result.Where(o => o.IndicatorId == 2).Single().Value.IsEqualTo(3);
            result.Where(o => o.IndicatorId == 3).Single().Value.IsEqualTo(7);

            iRepo.VerifyAll();
        }

        [Test]
        public void EvalSumsShouldReplaceSumFunctionsWithItsValues()
        {
            var c = new Coefficient { FieldsetId = 1, Formula = "i3/suma(i3)+i7+suma(i3)+suma(i9)", Id = 1 };
            EcoCalc.EvalSums(c,
                new[]
                    {
                        new IndicatorValue {IndicatorId = 9, Value = 5},
                        new IndicatorValue {IndicatorId = 9, Value = 5},
                        new IndicatorValue {IndicatorId = 3, Value = 1},
                        new IndicatorValue {IndicatorId = 5, Value = 5},
                        new IndicatorValue {IndicatorId = 3, Value = 2},
                        new IndicatorValue {IndicatorId = 3, Value = 4},
                    });

            c.Formula.IsEqualTo("i3/7+i7+7+10");
        }
    }
}