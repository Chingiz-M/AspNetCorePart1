using AspNetCoreProject.Domain.Entities.Identity;
using AspNetCoreProject.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            userManager = UserManager;
            signInManager = SignInManager;
        }
        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterViewModel());
        [AllowAnonymous]

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new User { UserName = model.UserName };

            var register_result = await userManager.CreateAsync(user, model.Password);

            if (register_result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                await userManager.AddToRoleAsync(user, Role.Users);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in register_result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }
        [AllowAnonymous]
        public IActionResult Login(string returnURL) => View(new LoginViewModel { ReturnURL = returnURL});
        [AllowAnonymous]
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var login_result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);

            if (login_result.Succeeded)
                return LocalRedirect(model.ReturnURL ?? "/");

            ModelState.AddModelError("", "Ошибка ввода логина или пароля");

            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult AccsessDenied() => View();
    }
}
