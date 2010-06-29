using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra.Dto;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class BankController : BaseController
    {
        private readonly IBankService bankService;

        public BankController(IBankService bankService)
        {
            this.bankService = bankService;
        }

        [HttpPost]
        public ActionResult Get(long id)
        {
            return Content(bankService.Get(id).Name);
        }

        public ActionResult Index() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Page(int? page, string name, string code)
        {
            return View(bankService.GetPage(page ?? 1, 5, name, code));
        }

        public ActionResult Create()
        {
            return View(new BankCreateInput());
        }

        [HttpPost]
        public ActionResult Create(BankCreateInput input)
        {
            if (!ModelState.IsValid)
                return View(input);

            bankService.Create((Bank)new Bank().InjectFrom(input));

            return Content("ok");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            SetError(bankService.Delete(id));
            return RedirectToAction("Index");
        }
    }
}