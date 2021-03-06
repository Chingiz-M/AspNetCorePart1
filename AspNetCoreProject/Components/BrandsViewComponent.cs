using AspNetCoreProject.Data;
using AspNetCoreProject.Services.Interfaces;
using AspNetCoreProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AspNetCoreProject.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData productData;

        public BrandsViewComponent(IProductData ProductData)
        {
            productData = ProductData;
        }
        public IViewComponentResult Invoke()
        {
            var brands = productData.GetBrands().OrderBy(b => b.Order).Select(
                b => new BrandViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                });
            return View(brands);
        }
    }
}
