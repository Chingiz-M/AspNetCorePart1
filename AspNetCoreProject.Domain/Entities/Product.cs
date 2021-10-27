using AspNetCoreProject.Domain.Entities.Base;
using AspNetCoreProject.Domain.Entities.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreProject.Domain.Entities
{
    [Index(nameof(Name))]
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public int SectionId { get; set; }
        [ForeignKey(nameof(SectionId))]
        public Section Section { get; set; }
        public int? BrandId { get; set; }
        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; }
        public string ImageUrl { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
