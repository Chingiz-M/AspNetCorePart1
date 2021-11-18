using AspNetCoreProject.Domain.Entities;

namespace AspNetCoreProject.Domain.ViewModels
{
    public class BreadCrumbsViewModel
    {
        public Section Section { get; set; }

        public Brand Brand { get; set; }

        public string Product { get; set; }
    }
}
