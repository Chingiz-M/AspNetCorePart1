using AspNetCoreProject.Models;
using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; } = new List<Employee>()
        {
            new Employee(1,"Ivan","Ivanov","Ivanovich",19,new DateTime(2002,12,3), "5 лет"),
            new Employee(2,"Petr","Petrov","Petrovich",31,new DateTime(1990,11,4), "6 лет")
        };
    }
}
