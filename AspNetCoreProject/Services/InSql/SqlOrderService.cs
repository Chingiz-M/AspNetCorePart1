using AspNetCoreProject.DAL.Context;
using AspNetCoreProject.Domain.Entities.Identity;
using AspNetCoreProject.Domain.Entities.Orders;
using AspNetCoreProject.Services.Interfaces;
using AspNetCoreProject.ViewModels;
using Microsoft.AspNetCore.Identity;
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

        public Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrder(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetUserOrders(string UserName)
        {
            throw new NotImplementedException();
        }
    }
}
