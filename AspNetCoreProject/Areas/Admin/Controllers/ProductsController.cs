using AspNetCoreProject.Infrastructure.Mapping;
using AspNetCoreProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductData productData;

        public ProductsController(IProductData productData)
        {
            this.productData = productData;
        }
        public IActionResult Index()
        {
            var products = productData.GetProducts().ToView();
            return View(products);
        }

        public IActionResult Edit(int id) => RedirectToAction(nameof(Index));
        public IActionResult Remove(int id) => RedirectToAction(nameof(Index));
    }
}
