using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public string Experience { get; set; }

        public Employee(int id, string name, string lastname, string surname, int age)
        {
            Id = id;
            Name = name;
            Lastname = lastname;
            Surname = surname;
            Age = age;
        }
    }
}
