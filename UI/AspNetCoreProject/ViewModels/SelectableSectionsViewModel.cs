using AspNetCoreProject.Domain.ViewModels;
using System.Collections.Generic;

namespace AspNetCoreProject.ViewModels
{
    public class SelectableSectionsViewModel
    {
        public IEnumerable<SectionViewModel> Sections { get; set; }

        public int? SectionId { get; set; }

        public int? ParentSectionId { get; set; }
    }
}
