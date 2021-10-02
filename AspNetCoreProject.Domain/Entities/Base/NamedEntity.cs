using AspNetCoreProject.Domain.Entities.Base.Interfaces;

namespace AspNetCoreProject.Domain.Entities.Base
{
    class NamedEntity : Entity, INamedEntity
    {
        public string Name { get; set; }
    }
}
