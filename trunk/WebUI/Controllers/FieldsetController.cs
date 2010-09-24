using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FieldsetController : BaseController
    {
        private readonly IBuilder<Fieldset, FieldsetInput> builder;
        private readonly IFieldsetService fieldsetService;

        public FieldsetController(IBuilder<Fieldset, FieldsetInput> builder, IFieldsetService fieldsetService)
        {
            this.fieldsetService = fieldsetService;
            this.builder = builder;
        }

        public ActionResult Index(int? page)
        {
            return View(fieldsetService.GetPageable(page ?? 1, 5));
        }

        public ActionResult Create()
        {
            return View(builder.BuildInput(new Fieldset()));
        }

        [HttpPost]
        public ActionResult Create(FieldsetInput o)
        {
            if (!ModelState.IsValid)
                return View(o);
            fieldsetService.Create(builder.BuilEntity(o));
            return Content("ok");
        }

        public ActionResult ManageFields(int id)
        {
            return View(fieldsetService.Get(id));
        }

        public ActionResult Open(int id)
        {
            return View(fieldsetService.Get(id));
        }

        public ActionResult View(int id)
        {
            return View(fieldsetService.Get(id));
        }

        public ActionResult Assigned(int id)
        {
            var o = new FieldsInput
                        {
                            Fields = fieldsetService.GetAssignedFields(id),
                            FieldsetId = id
                        };
            return View(o);
        }

        public ActionResult AssignedListLite(int fieldsetId)
        {
            return View(fieldsetService.GetAssignedFields(fieldsetId));
        }

        public ActionResult Unassigned(int id)
        {
            var o = new FieldsInput
            {
                Fields = fieldsetService.GetUnassignedFields(id),
                FieldsetId = id
            };
            return View(o);
        }

        [HttpPost]
        public ActionResult Assign(int fieldId, int fieldsetId)
        {
            fieldsetService.Assign(fieldId, fieldsetId);
            return RedirectToAction("ManageFields", new { id = fieldsetId });
        }

        [HttpPost]
        public ActionResult Unassign(int fieldId, int fieldsetId)
        {
            fieldsetService.Unassign(fieldId, fieldsetId);
            return RedirectToAction("ManageFields", new { id = fieldsetId });
        }

        [HttpPost]
        public ActionResult HasFields(int fieldsetId)
        {
            fieldsetService.HasFields(fieldsetId);
            return RedirectToAction("Open", new { id = fieldsetId });
        }   
        
        [HttpPost]
        public ActionResult HasIndicators(int fieldsetId)
        {
            fieldsetService.HasIndicators(fieldsetId);
            return RedirectToAction("Open", new { id = fieldsetId });
        }        
        
        [HttpPost]
        public ActionResult HasCoefficients(int fieldsetId)
        {
            fieldsetService.HasCoefficients(fieldsetId);
            return RedirectToAction("Open", new { id = fieldsetId });
        }

        [HttpPost]
        public ActionResult Activate(int fieldsetId)
        {
            fieldsetService.Activate(fieldsetId);
            return RedirectToAction("Open", new { id = fieldsetId });
        } 
        
        [HttpPost]
        public ActionResult Deactivate(int fieldsetId)
        {
            fieldsetService.Deactivate(fieldsetId);
            return RedirectToAction("Open", new { id = fieldsetId });
        }
    }
}