using AspNetCoreProject.Data;
using AspNetCoreProject.Domain;
using AspNetCoreProject.Domain.Entities;
using AspNetCoreProject.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreProject.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IEnumerable<Product> products = TestData.Products;

            if (filter?.SectionId != null)
                products = products.Where(p => p.SectionId == filter.SectionId);

            if (filter?.BrandId != null)
                products = products.Where(p => p.BrandId == filter.BrandId);

            return products;
        }

        public IEnumerable<Section> GetSections() => TestData.Sections;
    }
}
