using AspNetCoreProject.Domain.ViewModels;
using AspNetCoreProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }
        public IActionResult Index() => View(new CartOrderViewModel { CartViewModel = cartService.GetViewModel() });
        public IActionResult Add(int id) 
        {
            cartService.Add(id);
            return RedirectToAction("Index", "Cart");
        }
        public IActionResult Decrement(int id)
        {
            cartService.Decrement(id);
            return RedirectToAction("Index", "Cart");
        }
        public IActionResult Remove(int id)
        {
            cartService.Remove(id);
            return RedirectToAction("Index", "Cart");
        }
        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(OrderViewModel orderViewModel, [FromServices] IOrderService orderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Index), new CartOrderViewModel
                {
                    CartViewModel = cartService.GetViewModel(),
                    OrderViewModel = orderViewModel,
                });

            var order = await orderService.CreateOrder(
                User.Identity.Name,
                cartService.GetViewModel(),
                orderViewModel);

            cartService.Clear(); 
            return RedirectToAction(nameof(OrderConfirmed), new { id = order.Id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
