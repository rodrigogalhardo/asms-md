using FakeItEasy;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Service;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    public class CrudServiceTests
    {
        private FooSrv srv;
#pragma warning disable 649
        [Fake] private IRepo<Foo> r;
#pragma warning restore 649
        
        [SetUp]
        public void SetUp()
        {
            Fake.InitializeFixture(this);
            srv = new FooSrv(r);
        }

        [Test]
        public void GetPageableShouldCall()
        {
            srv.GetPageable(3, 9);
            A.CallTo(() => r.GetPageable(3,9)).MustHaveHappened();
        }

        [Test]
        public void GetShouldCall()
        {
            srv.Get(32);
            A.CallTo(() => r.Get(32)).MustHaveHappened();
        }

        [Test]
        public void CreateShouldCallInsert()
        {
            srv.Create(A<Foo>.Ignored);
            A.CallTo(() => r.Insert(A<Foo>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void SaveShouldCallUpdate()
        {
            srv.Save(A<Foo>.Ignored);
            A.CallTo(() => r.Update(A<Foo>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void DeleteShouldCall()
        {
            srv.Delete(54);
            A.CallTo(() => r.Delete(54)).MustHaveHappened();
        }

        public class Foo:Entity
        {}
        public class FooSrv : CrudService<Foo>{
            public FooSrv(IRepo<Foo> repo) : base(repo)
            {
            }
        }

    }
}