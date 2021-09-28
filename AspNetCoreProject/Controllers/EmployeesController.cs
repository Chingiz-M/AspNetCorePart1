using AspNetCoreProject.Services.Interfaces;
using AspNetCoreProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreProject.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData employeesData;
        private readonly ILogger<EmployeesController> logger;

        public EmployeesController(IEmployeesData EmployeesData, ILogger<EmployeesController> Logger)
        {
            employeesData = EmployeesData;
            logger = Logger;
        }

        public IActionResult Index() => View(employeesData.GetAll());

        public IActionResult Details(int id)
        {
            var employee = employeesData.GetById(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            var employee = employeesData.GetById(id);

            if (employee == null)
                return NotFound();

            var model = new EmployeeViewModel(employee.Id,
                employee.Name, employee.Lastname, employee.Surname);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {

        }
    }
