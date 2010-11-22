using System;
using System.Web.Mvc;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class ReportController : BaseController
    {
        public ActionResult DossiersByDistrict()
        {
            return View(new DossiersByDistrictInput{Year = DateTime.Now.Year});
        }

        [HttpPost]
        public ActionResult DossiersByDistrict(DossiersByDistrictInput input)
        {
            if (!ModelState.IsValid) return View(input);
            return Json(input);
        }

        public ActionResult CrossDistrictMeasure(int id)
        {
            return View(new CrossDistrictMeasureInput { MeasuresetId = id });
        }

        [HttpPost]
        public ActionResult CrossDistrictMeasure(CrossDistrictMeasureInput input)
        {
            if (!ModelState.IsValid) return View(input);
            return Json(new { input.MeasuresetId, Date = input.Date.ToShortDateString() });
        }
        
        public ActionResult CrossDistrictMeasureAmountPayed(int id)
        {
            return View(new CrossDistrictMeasureInput { MeasuresetId = id });
        }

        [HttpPost]
        public ActionResult CrossDistrictMeasureAmountPayed(CrossDistrictMeasureInput input)
        {
            if (!ModelState.IsValid) return View(input);
            return Json(new { input.MeasuresetId, Date = input.Date.ToShortDateString() });
        }
    }
}