using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProject.Components
{
    public class SectionsViewComponent : ViewComponent 
    {
        public IViewComponentResult Invoke() => View();
    }
}
