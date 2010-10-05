using System.Web.Mvc;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Infra;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class Cruder<TEntity, TInput> : BaseController
        where TInput : new()
        where TEntity : new()
    {
        protected readonly IRepo<TEntity> Repo;
        private readonly IBuilder<TEntity, TInput> builder;


        public Cruder(IRepo<TEntity> repo, IBuilder<TEntity, TInput> builder)
        {
            Repo = repo;
            this.builder = builder;
        }

        public virtual ActionResult Index(int? page)
        {
            return View(Repo.GetPageable(page ?? 1, 10));
        }

        public ActionResult Create()
        {
            return View(builder.BuildInput(new TEntity()));
        }

        [HttpPost]
        public ActionResult Create(TInput o)
        {
            if (!ModelState.IsValid)
                return View(builder.RebuildInput(o));
            Repo.Insert(builder.BuildEntity(o));
            return Content("ok");
        }
    }
}