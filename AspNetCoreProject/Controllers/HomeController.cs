using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

    }
}
