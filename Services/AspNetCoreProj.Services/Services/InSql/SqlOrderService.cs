using AspNetCoreProject.DAL.Context;
using AspNetCoreProject.Domain.Entities.Identity;
using AspNetCoreProject.Domain.Entities.Orders;
using AspNetCoreProject.Domain.ViewModels;
using AspNetCoreProject.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject.Services.InSql
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreDB db;
        private readonly UserManager<User> userManager;

        public SqlOrderService(WebStoreDB dB, UserManager<User> userManager)
        {
            db = dB;
            this.userManager = userManager;
        }

        public async Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel)
        {
            var user = await userManager.FindByNameAsync(UserName).ConfigureAwait(false);
            if (user is null)
                throw new InvalidOperationException($"Пользователь {UserName} не найден");

            await using var transaction = await db.Database.BeginTransactionAsync();

            var order = new Order
            {
                User = user,
                Address = OrderModel.Address,
                Phone = OrderModel.Phone,
                Description = OrderModel.Description,
            };

            var product_ids = Cart.Items.Select(i => i.product.Id).ToArray();
            var cart_products = await db.Products.Where(p => product_ids.Contains(p.Id)).ToArrayAsync();

            order.Items = Cart.Items.Join(
                cart_products,
                cart_item => cart_item.product.Id,
                cart_product => cart_product.Id,
                (cart_item, cart_product) => new OrderItem
                {
                    Order = order,
                    Product = cart_product,
                    Price = cart_product.Price,
                    Quantity = cart_item.Quantity,
                }).ToArray();

            await db.Orders.AddAsync(order);
            await db.SaveChangesAsync();

            await transaction.CommitAsync();

            return order;
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await db.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == id)
                .ConfigureAwait(false);
                
            return order;
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string UserName)
        {
            var orders = await db.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.User.UserName == UserName)
                .ToArrayAsync()
                .ConfigureAwait(false);

            return orders;
        }
    }
}
