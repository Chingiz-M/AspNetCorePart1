using AspNetCoreProj.Interfaces;
using AspNetCoreProject.Domain;
using AspNetCoreProject.Domain.DTO;
using AspNetCoreProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProj.WebApi.Controllers
{
    [Route(WebApiAddresses.Products)]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductData productData;

        public ProductsApiController(IProductData productData)
        {
            this.productData = productData;
        }
        [HttpGet("sections")]
        public IActionResult GetSections()
        {
            var sections = productData.GetSections();
            return Ok(sections.ToDTO());
        }
        [HttpGet("sections/{id}")]
        public IActionResult GetSectionById(int id)
        {
            var section = productData.GetSectionById(id);
            return Ok(section.ToDTO());
        }
        [HttpGet("brands")]
        public IActionResult GetBrands()
        {
            var brands = productData.GetBrands();
            return Ok(brands.ToDTO());
        }
        [HttpGet("brands/{id}")]
        public IActionResult GetBrandsById(int id)
        {
            var brand = productData.GetBrandById(id);
            return Ok(brand.ToDTO());
        }
        [HttpPost]
        public IActionResult GetProducts(ProductFilter filter = null)
        {
            var products = productData.GetProducts(filter);
            return Ok(products.ToDTO());
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = productData.GetProductById(id);
            if (product is null)
                return NotFound();
            return Ok(product.ToDTO());
        }
    }
}
