using System.Web.Mvc;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Infra;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class Cruder<TEntity, TInput> : Controller
        where TInput : new()
        where TEntity : new()
    {
        protected readonly IRepo<TEntity> repo;
        private readonly IBuilder<TEntity, TInput> builder;


        public Cruder(IRepo<TEntity> repo, IBuilder<TEntity, TInput> builder)
        {
            this.repo = repo;
            this.builder = builder;
        }

        public virtual ActionResult Index(int? page)
        {
            return View(repo.GetPageable(page ?? 1, 5));
        }

        public ActionResult Create()
        {
            return View(builder.BuildInput(new TEntity()));
        }

        [HttpPost]
        public ActionResult Create(TInput o)
        {
            if (!ModelState.IsValid)
                return View(o);
            repo.Insert(builder.BuilEntity(o));
            return RedirectToAction("index");
        }
    }
}