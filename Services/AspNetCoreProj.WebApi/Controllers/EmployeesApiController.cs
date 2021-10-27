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
    }
}
