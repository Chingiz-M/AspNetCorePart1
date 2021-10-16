using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreProject.Domain.Entities
{
    public class Cart
    {
        public ICollection<ItemCart> Items { get; set; } = new List<ItemCart>();
        public int ItemsSum => Items.Sum(i => i.Quantity);
    }
    public class ItemCart
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
