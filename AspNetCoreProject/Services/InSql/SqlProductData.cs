using AspNetCoreProject.DAL.Context;
using AspNetCoreProject.Domain;
using AspNetCoreProject.Domain.Entities;
using AspNetCoreProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreProject.Services.InSql
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB db;

        public SqlProductData(WebStoreDB db)
        {
            this.db = db;
        }
        public IEnumerable<Brand> GetBrands() => db.Brands;

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IQueryable<Product> products = db.Products;

            if (filter?.SectionId != null)
                products = products.Where(p => p.SectionId == filter.SectionId);

            if (filter?.BrandId != null)
                products = products.Where(p => p.BrandId == filter.BrandId);

            return products;
        }

        public IEnumerable<Section> GetSections() => db.Sections;
    }
}
