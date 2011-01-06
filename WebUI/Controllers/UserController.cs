using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class UserController : Crudere<User, UserCreateInput, UserEditInput>
    {
        private new readonly IUserService s;

        public UserController(IUserService s, IBuilder<User, UserCreateInput> v, IBuilder<User, UserEditInput> ve)
            : base(s, v, ve)
        {
            this.s = s;
        }

        public ActionResult ChangePassword(int id)
        {
            return View(new ChangePasswordInput { Id = id });
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordInput input)
        {
            if (!ModelState.IsValid)
                return View(input);

            s.ChangePassword(input.Id, input.Password);
            return Json(new { input.Id });
        }
    }
}