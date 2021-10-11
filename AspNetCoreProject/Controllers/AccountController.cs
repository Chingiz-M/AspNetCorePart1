using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProject.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register => View();
        public IActionResult Login => View();
        public IActionResult Logout => RedirectToAction("Index", "Home");
        public IActionResult AccsessDenied => View();
    }
}
