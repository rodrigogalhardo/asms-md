using FakeItEasy;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;
using MRGSP.ASMS.WebUI.Controllers;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    public class CruderControllerTests
    {
        MeasureController c;

#pragma warning disable 649
        [Fake]
        IBuilder<Measure, MeasureInput> v;
        [Fake]
        ICrudService<Measure> s;
#pragma warning restore 649

        [SetUp]
        public void SetUp()
        {
            Fake.InitializeFixture(this);
            c = new MeasureController(s, v);
        }

        [Test]
        public void IndexShouldCallGetPageable()
        {
            c.Index(null);
            A.CallTo(() => s.GetPageable(1, 10)).MustHaveHappened();
        }

        [Test]
        public void CreateShouldBuildNewInput()
        {
            c.Create();
            A.CallTo(() => v.BuildInput(A<Measure>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void CreateShouldReturnViewForInvalidModelstate()
        {
            c.ModelState.AddModelError("", "");
            c.Create(A.Fake<MeasureInput>()).ShouldBeViewResult();
        }

        [Test]
        public void CreateShouldReturnContentOk()
        {
            c.Create(A.Fake<MeasureInput>()).ShouldBeContentOk();
        }

        [Test]
        public void EditShouldReturnCreateView()
        {
            A.CallTo(() => s.Get(1)).Returns(A.Fake<Measure>());
            c.Edit(1).ShouldBeViewResult().ShouldBeCreate();
            A.CallTo(() => s.Get(1)).MustHaveHappened();
        }

        [Test]
        public void EditShouldThrowException()
        {
            A.CallTo(() => s.Get(1)).Returns(null);
            Assert.Throws<AsmsEx>(() => c.Edit(1));
            A.CallTo(() => v.BuildInput(A<Measure>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void EditShouldReturnContentOk()
        {
            c.Edit(A.Fake<MeasureInput>()).ShouldBeContentOk();
            A.CallTo(() => v.BuildEntity(A<MeasureInput>.Ignored, A<int>.Ignored)).MustHaveHappened();
            A.CallTo(() => s.Save(A<Measure>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void EditShouldReturnViewForInvalidModelstate()
        {
            c.ModelState.AddModelError("", "");
            c.Edit(A.Fake<MeasureInput>()).ShouldBeViewResult().ShouldBeCreate();
            A.CallTo(() => v.RebuildInput(A<MeasureInput>.Ignored, A<int>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void EditShouldReturnContentOnError()
        {
            A.CallTo(() => v.BuildEntity(A<MeasureInput>.Ignored, A<int>.Ignored)).Throws(new AsmsEx("aa"));
            c.Edit(A.Fake<MeasureInput>()).ShouldBeContent().Content.ShouldEqual("aa");
        }

        [Test]
        public void DeleteShouldRedirectToIndex()
        {
            c.Delete(1).ShouldRedirectToAction("index");
            A.CallTo(() => s.Delete(1)).MustHaveHappened();
        }
    }
}