using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FieldsController : Controller
    {
        private readonly IUberRepo<Field> repo;
        private readonly IBuilder<Field, FieldInput> builder;


        public FieldsController(IUberRepo<Field> repo, IBuilder<Field, FieldInput> builder)
        {
            this.repo = repo;
            this.builder = builder;
        }

        public ActionResult Index(int? page)
        {
            return View(repo.GetPageable(page ?? 1, 5));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FieldInput o)
        {
            if (!ModelState.IsValid)
                return View(o);
            repo.Insert(builder.BuilEntity(o));
            return RedirectToAction("index");
        }

        
    }
}