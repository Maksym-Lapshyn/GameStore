using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Services.DTOs;
using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	public class OrdersController : BaseController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrdersController(IOrderService orderService,
			IMapper mapper)
		{
			_orderService = orderService;
			_mapper = mapper;
		}

		public ActionResult Show()
		{
			var order = GetOrder();

			return View(order);
		}

		public ActionResult Buy(string key)
		{
			var orderViewModel = GetOrder();
			var orderDto = _mapper.Map<OrderViewModel, OrderDto>(orderViewModel);
			_orderService.Update(orderDto, key);

			return RedirectToAction("Show");
		}

		public ActionResult ShowAll()
		{

		}

		[HttpGet]
		public ActionResult History()
		{
			var allOrdersModel = new CompositeOrdersViewModel
			{
				Filter = new OrderFilterViewModel
				{
					From = DateTime.UtcNow,
					To = DateTime.UtcNow
				}
			};

			return View(allOrdersModel);
		}

		[HttpPost]
		public ActionResult History(CompositeOrdersViewModel compositeOrdersViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(compositeOrdersViewModel);
			}

			var filterDto = _mapper.Map<OrderFilterViewModel, OrderFilterDto>(compositeOrdersViewModel.Filter);
			compositeOrdersViewModel.Orders = _mapper.Map<IEnumerable<OrderDto>, List<OrderViewModel>>(_orderService.GetAll(filterDto));

			return View(compositeOrdersViewModel);
		}
	}
}