using System.Web.Mvc;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class Cruders<TEntity, TInput> : BaseController
        where TInput : new()
        where TEntity : new()
    {
        protected readonly ICrudService<TEntity> s;
        private readonly ICreateBuilder<TEntity, TInput> builder;
        private readonly IEditBuilder<TEntity, TInput> editBuilder;


        public Cruders(ICrudService<TEntity> s, ICreateBuilder<TEntity, TInput> builder, IEditBuilder<TEntity, TInput> editBuilder)
        {
            this.s = s;
            this.builder = builder;
            this.editBuilder = editBuilder;
        }

        [HttpPost]
        public ActionResult Create(TInput o)
        {
            if (!ModelState.IsValid)
                return View(builder.RebuildInput(o));
            s.Create(builder.BuildEntity(o));
            return Content("ok");
        }

        public ActionResult Edit(int id)
        {
            return View("create", editBuilder.BuildInput(s.Get(id)));
        }

        [HttpPost]
        public ActionResult Edit(TInput input)
        {
            if (!ModelState.IsValid)
                return View("create", editBuilder.RebuildInput(input));
            s.Save(builder.BuildEntity(input));
            return Content("ok");
        }
    }
}