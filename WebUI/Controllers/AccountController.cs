using System.Web.Mvc;
using MRGSP.ASMS.Core.Security;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.WebUI.Controllers
{
    [HandleError]
    public class AccountController : Controller
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
        public ActionResult SignIn(string name, string password)
        {
            var user = userService.Get(name, password);
            if (user == null)
            {
                ModelState.AddModelError("_FORM", "Login or Password not correct, please try againg");
                return View();
            }

            var roles = userService.GetRoles(user.Id);

            formsAuth.SignIn(name, false, roles);

            return RedirectToAction("Index", "Home");

        }

        public ActionResult SignOff()
        {
            formsAuth.SignOut();
            return RedirectToAction("SignIn", "Account");
        }
    }
}
