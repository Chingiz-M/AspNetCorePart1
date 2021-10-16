using AspNetCoreProject.DAL.Context;
using AspNetCoreProject.Domain;
using AspNetCoreProject.Domain.Entities;
using AspNetCoreProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public Brand GetBrandById(int id) => db.Brands.SingleOrDefault(b => b.Id == id);

        public IEnumerable<Brand> GetBrands() => db.Brands;

        public Product GetProductById(int id) => db.Products
            .Include(p => p.Brand)
            .Include(p => p.Section)
            .FirstOrDefault(p => p.Id == id);

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            IQueryable<Product> products = db.Products.Include(p => p.Brand).Include(p => p.Section);

            if (filter?.SectionId != null)
                products = products.Where(p => p.SectionId == filter.SectionId);

            if (filter?.BrandId != null)
                products = products.Where(p => p.BrandId == filter.BrandId);

            return products;
        }

        public Section GetSectionById(int id) => db.Sections.SingleOrDefault(s => s.Id == id);

        public IEnumerable<Section> GetSections() => db.Sections;
    }
}
