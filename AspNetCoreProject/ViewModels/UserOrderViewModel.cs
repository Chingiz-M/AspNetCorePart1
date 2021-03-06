using System;

namespace AspNetCoreProject.ViewModels
{
    public class UserOrderViewModel
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
