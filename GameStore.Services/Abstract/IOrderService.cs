using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	public interface IOrderService
	{
		void Create(OrderDto orderDto);

		void Edit(OrderDto orderDto, string gameKey);

		OrderDto GetSingleBy(string customerId);
	}
}