using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel product, int Quantity)> Items { get; set; }
        public int ItemsSum => Items?.Sum(i => i.Quantity) ?? 0;
        public decimal TotalSum => Items?.Sum(i => i.product.Price * i.Quantity) ?? 0;
    }
}
