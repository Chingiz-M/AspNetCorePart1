using AspNetCoreProject.Domain.Entities.Identity;
using AspNetCoreProject.Infrastructure.Mapping;
using AspNetCoreProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Administrators)]
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
