using AspNetCoreProject.Domain.Entities;
using AspNetCoreProject.Domain.Entities.Orders;
using AspNetCoreProject.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreProject.Domain.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public IEnumerable<OrderItemDTO> Items { get; set; }
    }
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
    public class CreateOrderDTO
    {
        public OrderViewModel Order { get; set; }
        public IEnumerable<OrderItemDTO> Items { get; set; }
    }

    public static class OrderDTOMapper
    {
        public static OrderItemDTO ToDTO(this OrderItem order) => order is null ?
            null
            : new OrderItemDTO
            {
                Id = order.Id,
                Price = order.Price,
                ProductId = order.Product.Id,
                Quantity = order.Quantity
            };
        public static OrderItem FromDTO(this OrderItemDTO order) => order is null ?
            null
            : new OrderItem
            {
                Id = order.Id,
                Price = order.Price,
                Product = new Product { Id = order.ProductId },
                Quantity = order.Quantity
            };
        public static OrderDTO ToDTO(this Order order) => order is null ?
            null
            : new OrderDTO
            {
                Id = order.Id,
                Address = order.Address,
                Date = order.Date,
                Description = order.Description,
                Items = order.Items.Select(ToDTO),
                Phone = order.Phone,
            };
        public static Order FromDTO(this OrderDTO order) => order is null ?
            null
            : new Order
            {
                Id = order.Id,
                Address = order.Address,
                Date = order.Date,
                Description = order.Description,
                Items = order.Items.Select(FromDTO).ToList(),
                Phone = order.Phone,
            };
        public static IEnumerable<OrderDTO> ToDTO(this IEnumerable<Order> orders) => orders.Select(ToDTO);
        public static IEnumerable<Order> FromDTO(this IEnumerable<OrderDTO> orders) => orders.Select(FromDTO);

        public static IEnumerable<OrderItemDTO> ToDTO(this CartViewModel cart) =>
            cart.Items.Select(p => new OrderItemDTO
            {
                ProductId = p.product.Id,
                Price = p.product.Price,
                Quantity = p.Quantity,
            });
        public static CartViewModel ToCartView(this IEnumerable<OrderItemDTO> orders) => new()
        {
            Items = orders.Select(p => (new ProductViewModel { Id = p.ProductId }, p.Quantity))
        };
    }

}
