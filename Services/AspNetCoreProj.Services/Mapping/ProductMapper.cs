using AspNetCoreProject.Domain.Entities;
using AspNetCoreProject.Domain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreProject.Infrastructure.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel ToView(this Product product) => product is null
            ? null
            : new ProductViewModel
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Price = product.Price,
                Section = product.Section.Name,
                Brand = product.Brand.Name,

            };

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> products) => products.Select(ToView);
    }
}
