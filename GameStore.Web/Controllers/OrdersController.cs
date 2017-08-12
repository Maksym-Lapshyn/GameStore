using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
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

		public ActionResult Show(string key)
		{
            var orderDto = _orderService.GetSingle(key);
            var model = _mapper.Map<OrderDto, OrderViewModel>(orderDto);

			return View(model);
		}

        [HttpPost]
		public ActionResult Update(OrderViewModel model)
        {
            var orderDto = _mapper.Map<OrderViewModel, OrderDto>(model);
            _orderService.Update(orderDto);
            return View();
        }

		public ActionResult ShowAll()
		{
            var orderDtos = _orderService.GetAll();
            var model = _mapper.Map<IEnumerable<OrderDto>, List<OrderViewModel>>(orderDtos);

            return View(model);
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