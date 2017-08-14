using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IOrderService
	{
		void Create(OrderDto orderDto);

		void Update(OrderDto orderDtoy);

		void Buy(int orderId, string gameKey);

		bool ContainsActive(int userId);

		OrderDto GetSingleActive(int userId);

		IEnumerable<OrderDto> GetAll(OrderFilterDto orderFilter = null);
	}
}