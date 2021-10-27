using AspNetCoreProj.WebApi.Clients.Base;
using AspNetCoreProject.Domain.Models;
using AspNetCoreProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreProj.WebApi.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(HttpClient client) : base(client, "api/employees")
        {
        }

        public int Add(Employee employee)
        {
            var response = Post<Employee>(address, employee);
            var added_emp = response.Content.ReadFromJsonAsync<Employee>();
            var id_emp = added_emp.Id;
            return id_emp;
        }

        public bool Delete(int id)
        {
            var response = Delete($"{address}/{id}");
            var res = response.IsSuccessStatusCode;
            return res;
        }

        public IEnumerable<Employee> GetAll()
        {
            var response = Get<IEnumerable<Employee>>(address);
            return response;
        }

        public Employee GetById(int id)
        {
            var response = Get<Employee>($"{address}/{id}");
            return response;
        }

        public void Update(Employee employee)
        {
            Put(address,employee);
        }
    }
}
