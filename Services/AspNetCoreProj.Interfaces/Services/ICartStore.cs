using AspNetCoreProject.Domain.Entities;

namespace AspNetCoreProj.Interfaces.Services
{
    public interface ICartStore
    {
        public Cart Cart { get; set; }
    }
}
