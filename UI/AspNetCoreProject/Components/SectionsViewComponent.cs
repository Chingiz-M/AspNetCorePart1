using AspNetCoreProject.Domain.ViewModels;
using AspNetCoreProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreProject.Components
{
    public class SectionsViewComponent : ViewComponent 
    {
        private readonly IProductData productData;

        public SectionsViewComponent(IProductData ProductData)
        {
            productData = ProductData;
        }
        public IViewComponentResult Invoke()
        {
            var sections = productData.GetSections();

            var parent_sections = sections.Where(s => s.ParentId is null);

            var parent_sections_view = parent_sections.Select(s =>
             new SectionViewModel()
             {
                 Id = s.Id,
                 Name = s.Name,
                 Order = s.Order,
             }).ToList();

            foreach(var parent_section in parent_sections_view)
            {
                var child_sections = sections.Where(s => s.ParentId == parent_section.Id);

                foreach (var child in child_sections)
                    parent_section.ChildSections.Add(new SectionViewModel
                    {
                        Id = child.Id,
                        Name = child.Name,
                        Order = child.Order,
                        Parent = parent_section,
                    });

                parent_section.ChildSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }
            parent_sections_view.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));


            return View(parent_sections_view);
        } 
    }
}
