using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	public interface IOrderService
	{
		void Create(OrderDto orderDto);

		void Edit(OrderDto orderDto, int gameId);

		OrderDto GetSingleBy(string customerId);
	}
}