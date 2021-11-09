using Microsoft.AspNetCore.Mvc;
using System;

namespace AspNetCoreProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Error404() => View();
        public IActionResult Cart() => View();
        public IActionResult Checkout() => View();
        public IActionResult ContactUs() => View();
        public IActionResult Login() => View();
        public IActionResult ProductDetails() => View();
        public IActionResult Shop() => View();
        public IActionResult Status(string id)
        {
            if (id is null)
                throw new ArgumentNullException(nameof(id));
            switch (id)
            {
                case "404": return View("Error404");
                default: return Content($"Status code -{id}");
            }
             
        }

    }
}
