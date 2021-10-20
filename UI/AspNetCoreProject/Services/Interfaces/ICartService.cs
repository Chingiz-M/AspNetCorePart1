using AspNetCoreProject.Domain.ViewModels;

namespace AspNetCoreProject.Services.Interfaces
{
    public interface ICartService
    {
        void Add(int id);
        void Decrement(int id);
        void Remove(int id);
        void Clear();
        CartViewModel GetViewModel();
    }
}
