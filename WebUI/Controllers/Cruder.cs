using System.Web.Mvc;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class Cruder<TEntity, TInput> : BaseController
        where TInput : IdInput, new()
        where TEntity : Entity, new()
    {
        protected readonly ICrudService<TEntity> s;
        private readonly IBuilder<TEntity, TInput> v;


        public Cruder(ICrudService<TEntity> s, IBuilder<TEntity, TInput> v)
        {
            this.s = s;
            this.v = v;
        }

        public virtual ActionResult Index(int? page)
        {
            return View(s.GetPageable(page ?? 1, 10));
        }

        public ActionResult Create()
        {
            return View(v.BuildInput(new TEntity()));
        }

        [HttpPost]
        public ActionResult Create(TInput o)
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
            return View("create", v.BuildInput(o));
        }

        [HttpPost]
        public ActionResult Edit(TInput input)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("create", v.RebuildInput(input, input.Id));
                s.Save(v.BuildEntity(input, input.Id));
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
            return RedirectToAction("index");
        }
    }
}