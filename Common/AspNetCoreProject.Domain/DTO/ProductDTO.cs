using AspNetCoreProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreProject.Domain.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public BrandDTO Brand { get; set; }
        public SectionDTO Section { get; set; }
    }
    public class BrandDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
    public class SectionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int? ParentId { get; set; }
    }

    public static class BrandDTOMapper
    {
        public static BrandDTO ToDTO(this Brand brand) => brand is null ?
            null
            : new BrandDTO
            {
                Id = brand.Id,
                Name = brand.Name,
                Order = brand.Order,
            };
        public static Brand FromDTO(this BrandDTO brand) => brand is null ?
            null
            : new Brand
            {
                Id = brand.Id,
                Name = brand.Name,
                Order = brand.Order,
            };
        public static IEnumerable<BrandDTO> ToDTO(this IEnumerable<Brand> brands) => brands.Select(ToDTO);
        public static IEnumerable<Brand> FromDTO(this IEnumerable<BrandDTO> brands) => brands.Select(FromDTO);
    }

    public static class SectionDTOMapper
    {
        public static SectionDTO ToDTO(this Section section) => section is null ?
            null
            : new SectionDTO
            {
                Id = section.Id,
                Name = section.Name,
                Order = section.Order,
                ParentId = section.ParentId,
            };
        public static Section FromDTO(this SectionDTO section) => section is null ?
            null
            : new Section
            {
                Id = section.Id,
                Name = section.Name,
                Order = section.Order,
                ParentId = section.ParentId,
            };
        public static IEnumerable<SectionDTO> ToDTO(this IEnumerable<Section> section) => section.Select(ToDTO);
        public static IEnumerable<Section> FromDTO(this IEnumerable<SectionDTO> section) => section.Select(FromDTO);
    }
    public static class ProductDTOMapper
    {
        public static ProductDTO ToDTO(this Product product) => product is null ?
            null
            : new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Brand = product.Brand.ToDTO(),
                Section = product.Section.ToDTO(),
            };
        public static Product FromDTO(this ProductDTO product) => product is null ?
            null
            : new Product
            {
                Id = product.Id,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Brand = product.Brand.FromDTO(),
                BrandId = product.Brand.Id,
                Section = product.Section.FromDTO(),
                SectionId = product.Section.Id
            };
        public static IEnumerable<ProductDTO> ToDTO(this IEnumerable<Product> product) => product.Select(ToDTO);
        public static IEnumerable<Product> FromDTO(this IEnumerable<ProductDTO> product) => product.Select(FromDTO);
    }
}
