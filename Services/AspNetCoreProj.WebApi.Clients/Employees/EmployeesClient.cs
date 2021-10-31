using AspNetCoreProj.Interfaces;
using AspNetCoreProj.WebApi.Clients.Base;
using AspNetCoreProject.Domain.Models;
using AspNetCoreProject.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;

namespace AspNetCoreProj.WebApi.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(HttpClient client) : base(client, WebApiAddresses.Employees)
        {
        }

        public int Add(Employee employee)
        {
            var response = Post<Employee>(Address, employee);
            var added_emp = response.Content.ReadFromJsonAsync<Employee>().Result;
            if (added_emp is null)
                return -1;
            var id_emp = added_emp.Id;
            return id_emp;
        }

        public bool Delete(int id)
        {
            var response = Delete($"{Address}/{id}");
            var res = response.IsSuccessStatusCode;
            return res;
        }

        public IEnumerable<Employee> GetAll()
        {
            var response = Get<IEnumerable<Employee>>(Address);
            return response;
        }

        public Employee GetById(int id)
        {
            var response = Get<Employee>($"{Address}/{id}");
            return response;
        }

        public void Update(Employee employee)
        {
            Put(Address,employee);
        }
    }
}
