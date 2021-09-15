using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => Content("FFF");
        public IActionResult SecondAction() => Content("SSS");
    }
}
