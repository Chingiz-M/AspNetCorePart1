using AspNetCoreProject.Domain.ViewModels;
using AspNetCoreProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        public IActionResult Index() => View();
        public async Task<IActionResult> Orders([FromServices] IOrderService orderService)
        {
            var orders = await orderService.GetUserOrders(User.Identity.Name);

            return View(orders.Select(o => new UserOrderViewModel
            {
                Id = o.Id,
                Address = o.Address,
                Description = o.Description,
                Phone = o.Phone,
                TotalPrice = o.TotalPrice,
                Date = o.Date,
            }));
        }
    }
}
