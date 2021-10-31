using AspNetCoreProj.Interfaces;
using AspNetCoreProject.Domain.DTO;
using AspNetCoreProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreProj.WebApi.Controllers
{
    [Route(WebApiAddresses.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersApiController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [HttpGet("user/{UserName}")]
        public async Task<IActionResult> GetUserOrders(string UserName)
        {
            var orders = await orderService.GetUserOrders(UserName);
            return Ok(orders.ToDTO());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await orderService.GetOrderById(id);
            if (order is null)
                return NotFound();
            return Ok(order.ToDTO());
        }
        [HttpPost("{UserName}")]
        public async Task<IActionResult> CreateOrder(string Username, [FromBody] CreateOrderDTO createOrder)
        {
            var order = await orderService.CreateOrder(Username, createOrder.Items.ToCartView(), createOrder.Order);
            return Ok(order.ToDTO());
        }
    }
}
