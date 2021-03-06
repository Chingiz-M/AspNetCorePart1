using AspNetCoreProject.Domain.Entities.Base;
using AspNetCoreProject.Domain.Entities.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreProject.Domain.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public Section Parent { get; set; }
        public ICollection<Product> Products { get; set; } 
    }
}
