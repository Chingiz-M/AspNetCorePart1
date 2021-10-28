using AspNetCoreProj.Interfaces.TestApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject.Controllers
{
    public class WebApiController : Controller
    {
        private readonly IValuesService service;

        public WebApiController(IValuesService service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            var results = service.GetAll();
            return View(results);
        }
    }
}
