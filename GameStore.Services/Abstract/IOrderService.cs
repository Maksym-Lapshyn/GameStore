using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IOrderService
	{
		void Create(OrderDto orderDto);

		void Update(OrderDto orderDtoy);

        bool Contains(string userLogin);

		OrderDto GetSingle(string userLogin);

		IEnumerable<OrderDto> GetAll(OrderFilterDto orderFilter = null);
	}
}