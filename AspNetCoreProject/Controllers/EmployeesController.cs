using AspNetCoreProject.Services.Interfaces;
using AspNetCoreProject.ViewModels;
using AspNetCoreProject.Models;
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

        #region Edit
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return View(new EmployeeViewModel());

            var employee = employeesData.GetById((int)id);

            if (employee == null)
                return NotFound();

            var model = new EmployeeViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Lastname = employee.Lastname,
                Surname = employee.Surname,
                Age = employee.Age,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            var employee = new Employee(model.Id, model.Name, model.Surname, model.Lastname, model.Age);
            if (employee.Id == 0)
                employeesData.Add(employee);
            else
                employeesData.Update(employee);
            return RedirectToAction(nameof(Index));
        } 
        #endregion

        public IActionResult Delete(int id)
        {
            var employee = employeesData.GetById(id);

            if (employee == null)
                return NotFound();

            return View(new EmployeeViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Lastname = employee.Lastname,
                Surname = employee.Surname,
                Age = employee.Age,
            });
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            employeesData.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create() => View("Edit", new EmployeeViewModel());
    }
}
