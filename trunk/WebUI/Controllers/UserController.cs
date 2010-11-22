using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        private readonly IBuilder<User, UserCreateInput> cv;
        private readonly IBuilder<User, UserEditInput> ev;

        public UserController(IUserService userService, IBuilder<User, UserCreateInput> cv, IBuilder<User, UserEditInput> ev)
        {
            this.userService = userService;
            this.cv = cv;
            this.ev = ev;
        }

        public ActionResult Index(int? page)
        {
            return View(userService.GetPage(page ?? 1, 5));
        }

        public ActionResult Create()
        {
            return View(cv.BuildInput(new User()));
        }

        [HttpPost]
        public ActionResult Create(UserCreateInput input)
        {
            if (input.Roles == null) ModelState.AddModelError("roles", "selectati macar un rol");

            if (!ModelState.IsValid)
                return View(cv.RebuildInput(input));

            userService.Create(cv.BuildEntity(input));
            return Content("ok");
        }

        public ActionResult Edit(int id)
        {
            return View(ev.BuildInput(userService.GetFull(id)));
        }

        [HttpPost]
        public ActionResult Edit(UserEditInput input)
        {
            if (input.Roles == null) ModelState.AddModelError("roles", "selectati macar un rol");
            if (!ModelState.IsValid)
                return View(ev.RebuildInput(input, input.Id));

            userService.Save(ev.BuildEntity(input, input.Id));
            return Content("ok");
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

            userService.ChangePassword(input.Id, input.Password);
            return Content("ok");
        }
    }
}