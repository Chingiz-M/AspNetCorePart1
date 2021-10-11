using AspNetCoreProject.Domain.Entities.Identity;
using AspNetCoreProject.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            userManager = UserManager;
            signInManager = SignInManager;
        }
        public IActionResult Register => View(new RegisterViewModel());
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login => View();
        public IActionResult Logout => RedirectToAction("Index", "Home");
        public IActionResult AccsessDenied => View();
    }
}
