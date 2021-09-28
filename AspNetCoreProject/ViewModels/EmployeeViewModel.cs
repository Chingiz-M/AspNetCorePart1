using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public string Experience { get; set; }
        public EmployeeViewModel(int id, string name, string lastname, string surname)
        {
            Id = id;
            Name = name;
            Lastname = lastname;
            Surname = surname;
        }
    }
}
