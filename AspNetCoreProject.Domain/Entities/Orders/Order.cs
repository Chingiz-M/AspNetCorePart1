using AspNetCoreProject.Domain.Entities.Base;
using AspNetCoreProject.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreProject.Domain.Entities.Orders
{
    public class Order : Entity
    {
        [Required]
        public User User { get; set; }
        [Required]
        [MaxLength(400)]
        public string Address { get; set; }
        [Required]
        [MaxLength(100)]
        public string Phone { get; set; }
        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
        public string Description { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
    public class OrderItem : Entity
    {
        [Required]
        public Product Product { get; set; }
        [Required]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [NotMapped]
        public decimal TotalItemPrice => Price * Quantity;
        public Order Order { get; set; }
    }
}
