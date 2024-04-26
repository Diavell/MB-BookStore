﻿using MB.Web.Models.Order;
using MB.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MB.Web.Controllers
{
    public class OrderController : Controller
    {
        public readonly IOrderService _orderService;
        public readonly IBasketService _basketService;

        public OrderController(IOrderService orderService, IBasketService basketService)
        {
            _orderService = orderService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();

            ViewBag.Basket = basket;

            return View(new CheckoutInfoInput());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
        {
            var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);

            if (!orderStatus.IsSuccessful)
            {
                var basket = await _basketService.Get();

                ViewBag.Basket = basket;

                ViewBag.Error = orderStatus.Error;

                return View();
            }

            return RedirectToAction(nameof(ReceivingPayment), new { orderId = orderStatus.OrderId });
        }

        public IActionResult ReceivingPayment(int orderId)
        {
            ViewBag.OrderId = orderId;

            return View();
        }

        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.OrderId = orderId;

            return View();
        }

        public async Task<IActionResult> CheckoutHistory()
        {
            return View(await _orderService.GetOrder());
        }
        
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();

            orders = orders.OrderByDescending(x => x.Id).ToList();

            return View(orders);
        }

        public async Task<IActionResult> GetOrderById(int id)
        {
            var orders = await _orderService.GetAllOrders();

            return View(orders.FirstOrDefault(x => x.Id == id));
        }
    }
}