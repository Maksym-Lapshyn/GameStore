using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Models;
using System.Net;
using System.Web.Http;

namespace GameStore.Web.Controllers.Api
{
	public class OrdersController : BaseApiController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrdersController(IApiAuthentication authentication,
			IOrderService orderService,
			IMapper mapper)
			: base(authentication)
		{
			_orderService = orderService;
			_mapper = mapper;
		}

		public IHttpActionResult Get(int key, string contentType)
		{
			if (!_orderService.Contains(key))
			{
				return Content(HttpStatusCode.BadRequest, "Order with such id does not exist");
			}

			var dto = _orderService.GetSingle(key);
			var model = _mapper.Map<OrderDto, OrderViewModel>(dto);

			return SerializeResult(model, contentType);
		}

		public IHttpActionResult Post(OrderViewModel model)
		{
			var dto = _mapper.Map<OrderViewModel, OrderDto>(model);
			_orderService.Create(dto);

			return Ok();
		}

		public IHttpActionResult Put(OrderViewModel model)
		{
			if (!_orderService.ContainsActiveById(model.Id))
			{
				return Content(HttpStatusCode.BadRequest, "Order with such id does not exist");
			}

			var dto = _mapper.Map<OrderViewModel, OrderDto>(model);
			_orderService.Update(dto);

			return Ok();
		}
	}
}