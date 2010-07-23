using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra.Dto;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FillFieldsController : Controller
    {
        private readonly IFieldsetService fService;
        private readonly IDossierService dService;

        public FillFieldsController(IFieldsetService fService, IDossierService dService)
        {
            this.fService = fService;
            this.dService = dService;
        }

        public ActionResult Index(int id)
        {
            var fields = fService.GetFieldsByDossier(id);
            var list = new List<FieldInputz>();
            foreach (var field in fields)
            {
                var f = new FieldInputz();
                f.InjectFrom(field);
                list.Add(f);
            }

            ViewData["dossierId"] = id;
            return View(list);
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            var dossierId = Convert.ToInt32(formCollection["dossierId"]);

            var fields = fService.GetFieldsByDossier(dossierId);
            var list = new List<FieldInputz>();
            foreach (var field in fields)
            {
                var f = new FieldInputz();
                f.InjectFrom(field);
                decimal v;
                if (!decimal.TryParse(formCollection["c" + f.Id], out v))
                    f.ErrorMessage = "valoarea " + formCollection["c" + f.Id] + " nu este valida pentru acest camp";
                f.Value = v;
                list.Add(f);
            }

            if (list.Any(o => o.HasError))
            {
                ViewData["dossierId"] = dossierId;
                return View(list);
            }

            var fieldValues = list.AsParallel().Select(
                fieldInputz => new FieldValue { FieldId = fieldInputz.Id, Value = fieldInputz.Value, DossierId = dossierId }).ToList();

            dService.Go(fieldValues);
            return Content("");
        }
    }
}