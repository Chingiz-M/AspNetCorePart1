using AspNetCoreProj.Interfaces;
using AspNetCoreProj.WebApi.Clients.Base;
using AspNetCoreProject.Domain.DTO;
using AspNetCoreProject.Domain.Entities.Orders;
using AspNetCoreProject.Domain.ViewModels;
using AspNetCoreProject.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AspNetCoreProj.WebApi.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(HttpClient client) : base(client, WebApiAddresses.Orders)
        {
        }

        public async Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel)
        {
            var model = new CreateOrderDTO
            {
                Items = Cart.ToDTO(),
                Order = OrderModel
            };
            var response = await PostAsync($"{Address}/{UserName}", model).ConfigureAwait(false);
            var order = await response.EnsureSuccessStatusCode()
                .Content.ReadFromJsonAsync<OrderDTO>().ConfigureAwait(false);
            return order.FromDTO();
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await GetAsync<OrderDTO>($"{Address}/{id}").ConfigureAwait(false);
            return order.FromDTO();
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string UserName)
        {
            var orders = await GetAsync<IEnumerable<OrderDTO>>($"{Address}/user/{UserName}").ConfigureAwait(false);
            return orders.FromDTO();
        }
    }
}
