using AspNetCoreProject.Domain.Entities.Identity;
using AspNetCoreProject.Domain.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(
            UserManager<User> UserManager,
            SignInManager<User> SignInManager,
            ILogger<AccountController> Logger)
        {
            userManager = UserManager;
            signInManager = SignInManager;
            logger = Logger;
        }
        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterViewModel());
        [AllowAnonymous]

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            using (logger.BeginScope("Регистрация пользователя {0}", model.UserName))
            {
                var user = new User { UserName = model.UserName };

                logger.LogInformation("Регистрация пользователя {0}", user.UserName);

                var register_result = await userManager.CreateAsync(user, model.Password);

                if (register_result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Role.Users);

                    await signInManager.SignInAsync(user, false);
                    logger.LogInformation("Пользователь {0} зарегистрирован", user.UserName);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in register_result.Errors)
                    ModelState.AddModelError("", error.Description);

                logger.LogWarning("Ошибка при регистрации пользователя {0}: {1}",
                    user.UserName, string.Join(",", register_result.Errors.Select(er => er.Description)));
            }
           
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
            {
                logger.LogInformation("Пользователь {0} вошёл в систему", model.UserName);
                return LocalRedirect(model.ReturnURL ?? "/");
            }

            ModelState.AddModelError("", "Ошибка ввода логина или пароля");

            logger.LogInformation("Ошибка ввода логина или пароля пользователя {0}", model.UserName);

            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            var username = User.Identity.Name;
            await signInManager.SignOutAsync();
            logger.LogInformation("Пользователь {0} вышел из системы", username);

            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult AccsessDenied() => View();
    }
}
