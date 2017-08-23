using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IOrderService
	{
		void AddDetails(int orderId, string gameKey);

		void DeleteDetails(int orderId, string gameKey);

		void Confirm(int orderId);

		void Ship(int orderId);

		OrderDto GetSingleActive(int userId);

		void CreateActive(int userId);

		bool ContainsActive(int userId);

		OrderDto GetSingle(int orderId);

		IEnumerable<OrderDto> GetAll(OrderFilterDto orderFilter = null);
	}
}