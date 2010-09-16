using System.Web.Mvc;
using MRGSP.ASMS.Core.Security;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    [HandleError]
    public class AccountController : BaseController
    {
        private readonly IFormsAuthentication formsAuth;
        private readonly IUserService userService;

        public AccountController(IFormsAuthentication formsAuth, IUserService userService)
        {
            this.formsAuth = formsAuth;
            this.userService = userService;
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(SignInInput input)
        {
            if(!ModelState.IsValid)
            {
                input.Password = null;
                input.Name = null;
                return View(input);
            }

            if (!userService.Validate(input.Name, input.Password))
            {
                SetError("Numele sau parola nu sunt introduse corect, va rugam sa mai incercati o data");
                return View();
            }

            var roles = userService.GetRoles(userService.Get(input.Name).Id);

            formsAuth.SignIn(input.Name, false, roles);

            return RedirectToAction("Index", "Home");

        }

        public ActionResult SignOff()
        {
            formsAuth.SignOut();
            return RedirectToAction("SignIn", "Account");
        }
    }
}
