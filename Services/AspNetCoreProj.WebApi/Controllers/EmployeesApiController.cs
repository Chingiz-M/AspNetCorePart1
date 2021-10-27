using AspNetCoreProject.Domain.Models;
using AspNetCoreProject.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProj.WebApi.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesApiController : ControllerBase
    {
        private readonly IEmployeesData employeesData;

        public EmployeesApiController(IEmployeesData employeesData) => this.employeesData = employeesData;

        [HttpGet]
        public IActionResult Get()
        {
            var employees = employeesData.GetAll();
            return Ok(employees);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var employee = employeesData.GetById(id);
            if (employee is null)
                return NotFound();
            return Ok(employee);
        }
        [HttpPut]
        public IActionResult Update(Employee employee)
        {
            employeesData.Update(employee);
            return Ok();
        }
        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            var id = employeesData.Add(employee);
            return CreatedAtAction(nameof(GetById), new { id }, employee);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = employeesData.Delete(id);
            return res == true ? Ok(true) : NotFound(false);
        }
    }
}
