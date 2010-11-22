using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FarmersCruder<TEntity, TInput> : BaseController
        where TEntity : FarmersEntity, new()
        where TInput : FarmersInput, new()
    {
        private readonly IBuilder<TEntity, TInput> v;
        private readonly IFarmersEntityService<TEntity> service;

        public FarmersCruder(IBuilder<TEntity, TInput> v, IFarmersEntityService<TEntity> service)
        {
            this.v = v;
            this.service = service;
        }

        [ChildActionOnly]
        public virtual ActionResult Index(int farmerId)
        {
            return View(service.GetByFarmerId(farmerId));
        }

        public ActionResult Create(int farmerId)
        {
            return View(v.BuildInput(new TEntity { FarmerId = farmerId }));
        }

        [HttpPost]
        public ActionResult Create(TInput input)
        {
            if (!ModelState.IsValid) return View(v.RebuildInput(input));
            service.Create(v.BuildEntity(input));
            return RedirectToAction("Index", "ContactInfo", new { input.FarmerId });
        }

        [HttpPost]
        public ActionResult Deactivate(int id, int farmerId)
        {
            service.Deactivate(id);
            return RedirectToAction("Index", "ContactInfo", new { farmerId });
        }

    }
}