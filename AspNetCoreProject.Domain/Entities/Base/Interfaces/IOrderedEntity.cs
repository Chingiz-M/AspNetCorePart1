namespace AspNetCoreProject.Domain.Entities.Base.Interfaces
{
    interface IOrderedEntity : IEntity
    {
        int Order { get; }
    }
}
