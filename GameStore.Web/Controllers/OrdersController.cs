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

		[CustomAuthorize(AuthorizationMode.Allow, AccessLevel.User)]
		public ActionResult ShowActive()
		{
			var orderDto = _orderService.GetSingleActive(CurrentUser.Id);
			var model = _mapper.Map<OrderDto, OrderViewModel>(orderDto);

			return View(model);
		}

		[CustomAuthorize(AuthorizationMode.Allow, AccessLevel.User, AccessLevel.Manager, AccessLevel.Moderator, AccessLevel.Administrator)]
		public ActionResult Buy(string key)
		{
			var orderDto = _orderService.GetSingleActive(CurrentUser.Id);
			_orderService.BuyItem(orderDto.Id, key);

			if (Request.UrlReferrer != null)
			{
				return Redirect(Request.UrlReferrer.AbsolutePath);
			}

			return RedirectToAction("ShowActive", "Orders");
		}

		[CustomAuthorize(AuthorizationMode.Allow, AccessLevel.User, AccessLevel.Manager, AccessLevel.Moderator, AccessLevel.Administrator)]
		public ActionResult DeleteDetails(string key)
		{
			var orderDto = _orderService.GetSingleActive(CurrentUser.Id);
			_orderService.DeleteItem(orderDto.Id, key);

			if (Request.UrlReferrer != null)
			{
				return Redirect(Request.UrlReferrer.AbsolutePath);
			}

			return RedirectToAction("ShowActive", "Orders");
		}

		[CustomAuthorize(AuthorizationMode.Allow, AccessLevel.User)]
		[HttpPost]
		public ActionResult Update(OrderViewModel model)
		{
			var orderDto = _mapper.Map<OrderViewModel, OrderDto>(model);
			_orderService.Update(orderDto);
			return View();
		}

		[CustomAuthorize(AuthorizationMode.Allow, AccessLevel.Manager)]
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

		[CustomAuthorize(AuthorizationMode.Allow, AccessLevel.Manager)]
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

		[CustomAuthorize(AuthorizationMode.Allow, AccessLevel.Manager)]
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

		[CustomAuthorize(AuthorizationMode.Allow, AccessLevel.Manager)]
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
	}
}