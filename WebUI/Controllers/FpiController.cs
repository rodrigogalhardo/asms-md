using System.Web.Mvc;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FpiController : BaseController
    {
        private readonly IFpiService s;
        private readonly IMeasuresetService mss;
        private readonly IBuilder<Fpi, FpiInput> v;

        public FpiController(IFpiService s, IMeasuresetService mss, IBuilder<Fpi, FpiInput> v)
        {
            this.s = s;
            this.mss = mss;
            this.v = v;
        }

        public ActionResult Index(int measuresetId)
        {
            ViewData["measuresetId"] = measuresetId;
            ViewData["measures"] = mss.GetAssignedMeasures(measuresetId);
            return View(s.GetWhere(new { measuresetId }));
        }

        public ActionResult Create(int measuresetId, int measureId, int month)
        {
            return View(v.BuildInput(new Fpi { MeasureId = measureId, MeasuresetId = measuresetId, Month = month }));
        }

        [HttpPost]
        public ActionResult Create(FpiInput input)
        {
            if (!ModelState.IsValid) return View(v.RebuildInput(input));
            s.Create(v.BuildEntity(input));
            return Content("ok");
        }

        public ActionResult ChangeAmount(int fpiId)
        {
            var fpi = s.Get(fpiId);
            return View(new ChangeAmountInput().InjectFrom(fpi));
        }

        [HttpPost]
        public ActionResult ChangeAmount(ChangeAmountInput input)
        {
            if (!ModelState.IsValid)
                return View(input);
            try
            {
                s.ChangeAmount(input.Id, input.Amount, input.Amountm);
            }
            catch (AsmsEx e)
            {
                ModelState.AddModelError("amount", e.Message);
                return View(input);
            }
            return Content("ok");
        }

        public ActionResult State(int id)
        {
            return View(s.Get(id));
        }

        [HttpPost]
        public ActionResult GoAgreement(int id)
        {
            s.GoAgreement(id);
            return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
        }

        [HttpPost]
        public ActionResult Seal(int id)
        {
            s.Seal(id);
            return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
        }
    }
}