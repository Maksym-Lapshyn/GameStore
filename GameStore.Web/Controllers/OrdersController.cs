using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Common.Enums;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Infrastructure.Attributes;
using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	public class OrdersController : BaseController
	{
		private readonly IOrderService _orderService;
		private readonly IUserService _userService;
		private readonly IGameService _gameService;
		private readonly IMapper _mapper;

		public OrdersController(IOrderService orderService,
			IUserService userService,
			IGameService gameService,
			IMapper mapper, 
			IAuthentication authentication)
			: base(authentication)
		{
			_orderService = orderService;
			_userService = userService;
			_gameService = gameService;
			_mapper = mapper;
		}

		#region User's actions
		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.User)]
		public ActionResult Busket()
		{
			if (!_orderService.ContainsActive(CurrentUser.Id))
			{
				return View(new OrderViewModel());
			}

			var orderDto = _orderService.GetSingleActive(CurrentUser.Id);
			var model = _mapper.Map<OrderDto, OrderViewModel>(orderDto);

			return View(model);
		}

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.User)]
		[HttpPost]
		public ActionResult Confirm(int orderId)
		{
			_orderService.Confirm(orderId);

			return RedirectToAction("Busket", "Orders");
		}

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.User)]
		[HttpPost]
		public ActionResult Buy(string gameKey)
		{
			if (!_orderService.ContainsActive(CurrentUser.Id))
			{
				_orderService.CreateActive(CurrentUser.Id);
			}

			var orderDto = _orderService.GetSingleActive(CurrentUser.Id);
			_orderService.AddDetails(orderDto.Id, gameKey);

			return RedirectToAction("Busket", "Orders");
		}
		#endregion

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.User, AccessLevel.Manager)]
		[HttpPost]
		public ActionResult AddDetails(int orderId, string gameKey)
		{
			_orderService.AddDetails(orderId, gameKey);

			return CurrentUser.Roles.Any(r => r.AccessLevel == AccessLevel.User) 
				? RedirectToAction("Busket", "Orders")
				: RedirectToAction("Show", "Orders", new { key = orderId });
		}

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.User, AccessLevel.Manager)]
		[HttpPost]
		public ActionResult DeleteDetails(int orderId, string gameKey)
		{
			_orderService.DeleteDetails(orderId, gameKey);

			return CurrentUser.Roles.Any(r => r.AccessLevel == AccessLevel.User) 
				? RedirectToAction("Busket", "Orders")
				: RedirectToAction("Show", "Orders", new {key = orderId});
		}

		#region Manager's actions
		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		public ActionResult Show(int key)
		{
			var orderDto = _orderService.GetSingle(key);
			var model = _mapper.Map<OrderDto, OrderViewModel>(orderDto);

			return View(model);
		}

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.Manager)]
		[HttpPost]
		public ActionResult Ship(int orderId)
		{
			_orderService.Ship(orderId);

			return RedirectToAction("Show", "Orders", new { key = orderId });
		}

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.Manager)]
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

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.Manager)]
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

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.Manager)]
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

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.Manager)]
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
		#endregion
	}
}