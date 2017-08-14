using AutoMapper;
using GameStore.Common.Enums;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Infrastructure.Attributes;
using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	[CustomAuthorize(AccessLevel = AccessLevel.User | AccessLevel.Manager)]
	public class OrdersController : BaseController
	{
		private readonly IOrderService _orderService;
		private readonly IUserService _userService;
		private readonly IMapper _mapper;

		public OrdersController(IOrderService orderService,
			IUserService userService,
			IMapper mapper)
		{
			_orderService = orderService;
			_userService = userService;
			_mapper = mapper;
		}

		public ActionResult Show()
		{
			OrderDto orderDto;
			OrderViewModel model;

			if (_orderService.ContainsActive(CurrentUser.Id))
			{
				orderDto = _orderService.GetSingleActive(CurrentUser.Id);
				model = _mapper.Map<OrderDto, OrderViewModel>(orderDto);

				return View(model);
			}

			orderDto = CreateNewOrder();
			model = _mapper.Map<OrderDto, OrderViewModel>(orderDto);

			return View(model);
		}

		public ActionResult Buy(string key)
		{
			var orderDto = _orderService.ContainsActive(CurrentUser.Id) 
				? _orderService.GetSingleActive(CurrentUser.Id) 
				: CreateNewOrder();
			_orderService.Buy(orderDto.Id, key);

			return Request.UrlReferrer == null ? RedirectToAction("ShowAll", "Games") : RedirectToAction(Request.UrlReferrer.ToString());
		}

		[CustomAuthorize(AccessLevel = AccessLevel.User)]
		[HttpPost]
		public ActionResult Update(OrderViewModel model)
		{
			var orderDto = _mapper.Map<OrderViewModel, OrderDto>(model);
			_orderService.Update(orderDto);
			return View();
		}

		[CustomAuthorize(AccessLevel = AccessLevel.Manager)]
		[HttpGet]
		public ActionResult History()
		{
			var model = new CompositeOrdersViewModel
			{
				Filter = new OrderFilterViewModel
				{
					From = DateTime.UtcNow,
					To = DateTime.UtcNow
				}
			};

			return View(model);
		}

		[CustomAuthorize(AccessLevel = AccessLevel.Manager)]
		[HttpPost]
		public ActionResult History(CompositeOrdersViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var filterDto = _mapper.Map<OrderFilterViewModel, OrderFilterDto>(model.Filter);
			model.Orders = _mapper.Map<IEnumerable<OrderDto>, List<OrderViewModel>>(_orderService.GetAll(filterDto));

			return View(model);
		}

		[CustomAuthorize(AccessLevel = AccessLevel.Manager)]
		[HttpGet]
		public ActionResult ShowAll()
		{
			var model = new CompositeOrdersViewModel
			{
				Filter = new OrderFilterViewModel
				{
					From = DateTime.UtcNow,
					To = DateTime.UtcNow
				}
			};

			return View(model);
		}

		[CustomAuthorize(AccessLevel = AccessLevel.Manager)]
		[HttpPost]
		public ActionResult ShowAll(CompositeOrdersViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (model.Filter.From < DateTime.UtcNow.AddDays(-30))
			{
				model.Filter.From = DateTime.UtcNow.AddDays(-30);
			}

			var filterDto = _mapper.Map<OrderFilterViewModel, OrderFilterDto>(model.Filter);
			model.Orders = _mapper.Map<IEnumerable<OrderDto>, List<OrderViewModel>>(_orderService.GetAll(filterDto));

			return View(model);
		}

		private OrderDto CreateNewOrder()
		{
			var userDto = _userService.GetSingle(CurrentUser.Login);
			var orderDto = new OrderDto { OrderStatus = OrderStatus.Active, User = userDto };
			_orderService.Create(orderDto);
			orderDto = _orderService.GetSingleActive(CurrentUser.Id);

			return orderDto;
		}
	}
}