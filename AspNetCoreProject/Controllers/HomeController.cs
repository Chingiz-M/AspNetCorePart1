using AspNetCoreProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> employees = new List<Employee>()
        {
            new Employee(1,"Ivan","Ivanov","Ivanovich",19),
            new Employee(1,"Petr","Petrov","Petrovich",32)
        };
        public IActionResult Index() => View();
        public IActionResult SecondAction() => Content("SSS");
        public IActionResult Employees() => View(employees);
    }
}
