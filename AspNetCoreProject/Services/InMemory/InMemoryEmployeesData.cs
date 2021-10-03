using AspNetCoreProject.Data;
using AspNetCoreProject.Models;
using AspNetCoreProject.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreProject.Services.InMemory
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly ILogger<InMemoryEmployeesData> logger;
        int CurrentMaxId;
        public InMemoryEmployeesData(ILogger<InMemoryEmployeesData> Logger)
        {
            logger = Logger;
            CurrentMaxId = TestData.Employees.Max(e => e.Id);
        }
        public int Add(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (TestData.Employees.Contains(employee)) return employee.Id;

            employee.Id = ++CurrentMaxId;
            TestData.Employees.Add(employee);
            return employee.Id;
        }

        public bool Delete(int id)
        {
            var delete_employee = GetById(id);
            if (delete_employee is null) return false;

            TestData.Employees.Remove(delete_employee);
            return true;
        }

        public IEnumerable<Employee> GetAll() => TestData.Employees;

        public Employee GetById(int id) => TestData.Employees.SingleOrDefault(e => e.Id == id);

        public void Update(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            var update_employee = GetById(employee.Id);
            if (update_employee is null) return;

            update_employee.Name = employee.Name;
            update_employee.Lastname = employee.Lastname;
            update_employee.Surname = employee.Surname;
            update_employee.Age = employee.Age;
        }
    }
}
