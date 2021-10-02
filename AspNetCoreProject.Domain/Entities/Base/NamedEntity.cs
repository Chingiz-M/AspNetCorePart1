using AspNetCoreProject.Domain.Entities.Base.Interfaces;

namespace AspNetCoreProject.Domain.Entities.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        public string Name { get; set; }
    }
}
