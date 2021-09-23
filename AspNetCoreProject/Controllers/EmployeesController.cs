using AspNetCoreProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject.Controllers
{
    public class EmployeesController : Controller
    {
        private static readonly List<Employee> employees = new List<Employee>()
        {
            new Employee(1,"Ivan","Ivanov","Ivanovich",19,new DateTime(2002,12,3), "5 лет"),
            new Employee(2,"Petr","Petrov","Petrovich",31,new DateTime(1990,11,4), "6 лет")
        };
        public IActionResult Index() => View(employees);
    }
}
