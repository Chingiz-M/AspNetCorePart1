using AspNetCoreProject.Domain;
using AspNetCoreProject.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProj.WebApi.Controllers
{
    [Route("api/products")]
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
            return Ok(sections);
        }
        [HttpGet("sections/{id}")]
        public IActionResult GetSectionById(int id)
        {
            var section = productData.GetSectionById(id);
            return Ok(section);
        }
        [HttpGet("brands")]
        public IActionResult GetBrands()
        {
            var brands = productData.GetBrands();
            return Ok(brands);
        }
        [HttpGet("brands/{id}")]
        public IActionResult GetBrandsById(int id)
        {
            var brand = productData.GetBrandById(id);
            return Ok(brand);
        }
        [HttpPost]
        public IActionResult GetProducts(ProductFilter filter = null)
        {
            var products = productData.GetProducts(filter);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = productData.GetProductById(id);
            if (product is null)
                return NotFound();
            return Ok(product);
        }
    }
}
