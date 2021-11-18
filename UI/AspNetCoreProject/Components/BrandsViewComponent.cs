using AspNetCoreProject.Domain.ViewModels;
using AspNetCoreProject.Services.Interfaces;
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
        public IViewComponentResult Invoke(string BrandId)
        {
            ViewBag.BrandId = int.TryParse(BrandId, out var id) ? id : (int?)null;
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
