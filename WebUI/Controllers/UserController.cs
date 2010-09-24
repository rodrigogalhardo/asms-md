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
        private readonly IBuilder<User, UserCreateInput> createBuilder;
        private readonly IBuilder<User, UserEditInput> editBuilder;

        public UserController(IUserService userService, IBuilder<User, UserCreateInput> createBuilder, IBuilder<User, UserEditInput> editBuilder)
        {
            this.userService = userService;
            this.editBuilder = editBuilder;
            this.createBuilder = createBuilder;
        }

        public ActionResult Index(int? page)
        {
            return View(userService.GetPage(page ?? 1, 5));
        }

        public ActionResult Create()
        {
            return View(createBuilder.BuildInput(new User()));
        }

        [HttpPost]
        public ActionResult Create(UserCreateInput input)
        {
            if (input.Roles == null) ModelState.AddModelError("roles", "selectati macar un rol");

            if (!ModelState.IsValid)
                return View(createBuilder.RebuildInput(input));

            userService.Create(createBuilder.BuilEntity(input));
            return Content("ok");
        }

        public ActionResult Edit(int id)
        {
            return View(editBuilder.BuildInput(userService.GetFull(id)));
        }

        [HttpPost]
        public ActionResult Edit(UserEditInput input)
        {
            if (input.Roles == null) ModelState.AddModelError("roles", "selectati macar un rol");
            if (!ModelState.IsValid)
                return View(editBuilder.RebuildInput(input));

            userService.Save(editBuilder.BuilEntity(input));
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