using AspNetCoreProject.Domain;
using AspNetCoreProject.Infrastructure.Mapping;
using AspNetCoreProject.Services.Interfaces;
using AspNetCoreProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData productData;

        public CatalogController(IProductData ProductData)
        {
            productData = ProductData;
        }
        public IActionResult Index(int? sectionId, int? brandId)
        {
            var filter = new ProductFilter
            {
                SectionId = sectionId,
                BrandId = brandId
            };

            var products = productData.GetProducts(filter);

            var view_model = new CatalogViewModel()
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products.OrderBy(p => p.Order).ToView(),
            };

            return View(view_model);
        }

        public IActionResult Details(int id)
        {
            var product = productData.GetProductById(id);

            if (product is null)
                return NotFound();

            return View(product.ToView());
        }
    }
}
