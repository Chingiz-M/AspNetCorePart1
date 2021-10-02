using AspNetCoreProject.Data;
using AspNetCoreProject.Domain.Entities;
using AspNetCoreProject.Services.Interfaces;
using System.Collections.Generic;

namespace AspNetCoreProject.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public IEnumerable<Section> GetSections() => TestData.Sections;
    }
}
