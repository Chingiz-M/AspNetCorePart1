using AspNetCoreProject.Domain.Entities.Base;
using AspNetCoreProject.Domain.Entities.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreProject.Domain.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
    }
}
