using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProject.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
