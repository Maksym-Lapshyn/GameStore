using GameStore.Services.Dtos;
using GameStore.Services.DTOs;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IOrderService
	{
		void Create(OrderDto orderDto);

		void Update(OrderDto orderDto, string gameKey);

		OrderDto GetSingle(string customerId);

		IEnumerable<OrderDto> GetAll(OrderFilterDto orderFilter = null);
	}
}