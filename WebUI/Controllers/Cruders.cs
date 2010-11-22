using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class Cruders<TEntity, TInput> : BaseController
        where TInput : IdInput, new()
        where TEntity : Entity, new()
    {
        protected readonly ICrudService<TEntity> s;
        private readonly IBuilder<TEntity, TInput> v;
        

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
            return View("create", v.BuildInput(s.Get(id)));
        }

        [HttpPost]
        public ActionResult Edit(TInput input)
        {
            if (!ModelState.IsValid)
                return View("create", v.RebuildInput(input, input.Id));
            s.Save(v.BuildEntity(input, input.Id));
            return Content("ok");
        }
    }
}