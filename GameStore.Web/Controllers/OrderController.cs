using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private const string CookieKey = "customerId";

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public ActionResult Show()
        {
            var order = GetOrder();

            return View(order);
        }

        public ActionResult Edit(int gameId)
        {
            var orderViewModel = GetOrder();
            if (orderViewModel.OrderDetails.Any(d => d.GameId == gameId))
            {
                orderViewModel.OrderDetails.First(d => d.GameId == gameId).Quantity++;
            }
            else
            {
                orderViewModel.OrderDetails.Add(new OrderDetailsViewModel{GameId = gameId, Quantity = 1});
            }

            var orderDto = Mapper.Map<OrderViewModel, OrderDto>(orderViewModel);
            _orderService.Edit(orderDto);

            return RedirectToAction("Show");
        }

        private OrderViewModel GetOrder()
        {
            OrderViewModel orderViewModel;
            if (Request.Cookies[CookieKey] != null)
            {
                var orderDto = _orderService.GetSingleBy(Request.Cookies[CookieKey].Value);
                orderViewModel = Mapper.Map<OrderDto, OrderViewModel>(orderDto);

                return orderViewModel;
            }
                
            var customerId = Guid.NewGuid().ToString();
            Response.Cookies.Add(new HttpCookie(CookieKey, customerId));
            orderViewModel = new OrderViewModel
            {
                CustomerId = customerId
            };

            _orderService.Create(Mapper.Map<OrderViewModel, OrderDto>(orderViewModel));
            orderViewModel = Mapper.Map<OrderDto, OrderViewModel>(_orderService.GetSingleBy(customerId));

            return orderViewModel;
        }
    }
}