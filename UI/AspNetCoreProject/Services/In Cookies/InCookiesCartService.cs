using AspNetCoreProject.Domain.Entities;
using AspNetCoreProject.Domain.ViewModels;
using AspNetCoreProject.Infrastructure.Mapping;
using AspNetCoreProject.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace AspNetCoreProject.Services.In_Cookies
{
    public class InCookiesCartService : ICartService
    {
        private readonly IHttpContextAccessor accessor;
        private readonly IProductData productData;
        private readonly string cookieCartName;

        private Cart Cart
        {
            get
            {
                var context = accessor.HttpContext;
                var cookies = context.Response.Cookies;

                var cart_cookies = context.Request.Cookies[$"{cookieCartName}"];
                if(cart_cookies is null)
                {
                    var cart = new Cart();
                    cookies.Append(cookieCartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }

                ReplaceCart(cookies, cart_cookies);
                return JsonConvert.DeserializeObject<Cart>(cart_cookies);
            }
            set => ReplaceCart(accessor.HttpContext!.Response.Cookies, JsonConvert.SerializeObject(value));

        }

        private void ReplaceCart(IResponseCookies cookies, string cart)
        {
            cookies.Delete(cookieCartName);
            cookies.Append(cookieCartName, cart);
        }
        public InCookiesCartService(IHttpContextAccessor accessor, IProductData productData)
        {
            this.accessor = accessor;
            this.productData = productData;

            var user = accessor.HttpContext!.User;
            var user_name = user.Identity!.IsAuthenticated ? user.Identity.Name : null;

            cookieCartName = $"Project{user_name}";
        }

        public void Add(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
                cart.Items.Add(new ItemCart { ProductId = id, Quantity = 1 });
            else
                item.Quantity++;

            Cart = cart;
        }

        public void Clear()
        {
            var cart = Cart;
            cart.Items.Clear();
            Cart = cart;
        }

        public void Decrement(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
                return;

            if (item.Quantity > 0)
                item.Quantity--;

            if (item.Quantity <= 0)
                cart.Items.Remove(item);

            Cart = cart;
        }

        public CartViewModel GetViewModel()
        {
            var products = productData.GetProducts(new Domain.ProductFilter
            {
                Ids = Cart.Items.Select(i => i.ProductId).ToArray()
            });

            var products_views = products.ToView().ToDictionary(p => p.Id);

            return new CartViewModel()
            {
                Items = Cart.Items.Where(i => products_views.ContainsKey(i.ProductId))
                                    .Select(i => (products_views[i.ProductId], i.Quantity))
            };
        }

        public void Remove(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
                return;

            cart.Items.Remove(item);
            Cart = cart;
        }
    }
}
