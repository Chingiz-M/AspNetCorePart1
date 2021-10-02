namespace AspNetCoreProject.Domain.Entities.Base.Interfaces
{
    interface INamedEntity : IEntity
    {
       string Name { get; }
    }
}
