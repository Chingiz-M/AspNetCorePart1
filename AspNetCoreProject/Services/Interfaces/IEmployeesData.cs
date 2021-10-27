using AspNetCoreProject.Models;
using System.Collections.Generic;

namespace AspNetCoreProject.Services.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        int Add(Employee employee);
        void Update(Employee employee);
        bool Delete(int id);
    }
}
