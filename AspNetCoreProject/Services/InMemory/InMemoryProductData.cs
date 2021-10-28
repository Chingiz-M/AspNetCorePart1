using AspNetCoreProject.Data;
using AspNetCoreProject.Domain;
using AspNetCoreProject.Domain.Entities;
using AspNetCoreProject.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreProject.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public Brand GetBrandById(int id) => TestData.Brands.FirstOrDefault(b => b.Id == id);

        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public Product GetProductById(int id) => TestData.Products.FirstOrDefault(p => p.Id == id);


        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IEnumerable<Product> products = TestData.Products;

            if (filter?.SectionId != null)
                products = products.Where(p => p.SectionId == filter.SectionId);

            if (filter?.BrandId != null)
                products = products.Where(p => p.BrandId == filter.BrandId);

            return products;
        }

        public Section GetSectionById(int id) => TestData.Sections.FirstOrDefault(s => s.Id == id);

        public IEnumerable<Section> GetSections() => TestData.Sections;
    }
}
