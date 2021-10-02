using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreProject.Domain;
using AspNetCoreProject.Domain.Entities;

namespace AspNetCoreProject.Services.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections(); 
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProducts(ProductFilter filter = null);
    }
}
