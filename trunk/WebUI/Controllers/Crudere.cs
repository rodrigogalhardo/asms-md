using System.Web.Mvc;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class Crudere<TEntity, TCreateInput, TEditInput> : Controller
        where TCreateInput : new()
        where TEditInput : IdInput, new()
        where TEntity : Entity, new()
    {
        protected readonly ICrudService<TEntity> s;
        protected readonly IBuilder<TEntity, TCreateInput> v;
        private readonly IBuilder<TEntity, TEditInput> ve;

        public Crudere(ICrudService<TEntity> s, IBuilder<TEntity, TCreateInput> v, IBuilder<TEntity, TEditInput> ve)
        {
            this.s = s;
            this.v = v;
            this.ve = ve;
        }

        public virtual ActionResult Index(int? page)
        {
            return View(s.GetPageable(page ?? 1, 5));
        }

        public ActionResult Create()
        {
            return View(v.BuildInput(new TEntity()));
        }

        [HttpPost]
        public virtual ActionResult Create(TCreateInput o)
        {
            if (!ModelState.IsValid)
                return View(v.RebuildInput(o));
            s.Create(v.BuildEntity(o));
            return Content("ok");
        }

        public ActionResult Edit(int id)
        {
            var o = s.Get(id);
            if (o == null) throw new AsmsEx("this entity doesn't exist anymore");
            return View(ve.BuildInput(o));
        }

        [HttpPost]
        public ActionResult Edit(TEditInput input)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(ve.RebuildInput(input, input.Id));
                s.Save(ve.BuildEntity(input, input.Id));
            }
            catch (AsmsEx ex)
            {
                return Content(ex.Message);
            }
            return Content("ok");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            s.Delete(id);
            return RedirectToAction("Index");
        }
    }
}