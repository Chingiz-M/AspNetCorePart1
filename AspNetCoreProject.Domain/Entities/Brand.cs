using AspNetCoreProject.Domain.Entities.Base;
using AspNetCoreProject.Domain.Entities.Base.Interfaces;

namespace AspNetCoreProject.Domain.Entities
{
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
    }
}
