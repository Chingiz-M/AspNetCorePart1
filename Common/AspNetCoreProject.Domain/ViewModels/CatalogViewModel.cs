using System.Collections.Generic;

namespace AspNetCoreProject.Domain.ViewModels
{
    public class CatalogViewModel
    {
        public int? SectionId { get; set; }
        public int? BrandId { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }

    }
}
