using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FieldsetController : Crudere<Fieldset, FieldsetInput, FieldsetEditInput>
    {
        private new readonly IFieldsetService s;

        public FieldsetController(IFieldsetService s, IBuilder<Fieldset, FieldsetInput> v, IBuilder<Fieldset, FieldsetEditInput> ve)
            : base(s, v, ve)
        {
            this.s = s;
        }

        public override ActionResult Index(int? page)
        {
            return View(s.GetDisplayPageable(page ?? 1, 5));
        }

        public ActionResult ManageFields(int id)
        {
            return View(s.Get(id));
        }

        public ActionResult Open(int id)
        {
            return View(s.Get(id));
        }

        public ActionResult View(int id)
        {
            return View(s.Get(id));
        }

        public ActionResult Assigned(int id)
        {
            var o = new FieldsInput
                        {
                            Fields = s.GetAssignedFields(id),
                            FieldsetId = id
                        };
            return View(o);
        }

        public ActionResult AssignedListLite(int fieldsetId)
        {
            return View(s.GetAssignedFields(fieldsetId));
        }

        public ActionResult Unassigned(int id)
        {
            var o = new FieldsInput
            {
                Fields = s.GetUnassignedFields(id),
                FieldsetId = id
            };
            return View(o);
        }

        [HttpPost]
        public ActionResult Assign(int fieldId, int fieldsetId)
        {
            s.Assign(fieldId, fieldsetId);
            return RedirectToAction("ManageFields", new { id = fieldsetId });
        }

        [HttpPost]
        public ActionResult Unassign(int fieldId, int fieldsetId)
        {
            s.Unassign(fieldId, fieldsetId);
            return RedirectToAction("ManageFields", new { id = fieldsetId });
        }

        [HttpPost]
        public ActionResult HasFields(int fieldsetId)
        {
            s.HasFields(fieldsetId);
            return RedirectToAction("Open", new { id = fieldsetId });
        }

        [HttpPost]
        public ActionResult HasIndicators(int fieldsetId)
        {
            s.HasIndicators(fieldsetId);
            return RedirectToAction("Open", new { id = fieldsetId });
        }

        [HttpPost]
        public ActionResult HasCoefficients(int fieldsetId)
        {
            s.HasCoefficients(fieldsetId);
            return RedirectToAction("Open", new { id = fieldsetId });
        }

        [HttpPost]
        public ActionResult Activate(int fieldsetId)
        {
            s.Activate(fieldsetId);
            return RedirectToAction("Open", new { id = fieldsetId });
        }

        [HttpPost]
        public ActionResult Deactivate(int fieldsetId)
        {
            s.Deactivate(fieldsetId);
            return RedirectToAction("Open", new { id = fieldsetId });
        }
    }
}