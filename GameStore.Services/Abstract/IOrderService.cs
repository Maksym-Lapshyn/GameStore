using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IOrderService
	{
		void AddDetails(int orderId, string gameKey);

		void DeleteDetails(int orderId, string gameKey);

		void Confirm(int orderId);

		void CheckoutActive(int userId);

		void Ship(int orderId);

		OrderDto GetSingleActive(int userId);

		OrderDto GetSingleActiveOrDefault(int userId);

		void CreateActive(int userId);

		bool ContainsActiveById(int orderId);

		bool ContainsActive(int userId);

		OrderDto GetSingle(int orderId);

		OrderDto GetSingleOrDefault(int orderId);

		IEnumerable<OrderDto> GetAll(OrderFilterDto orderFilter = null);

		void Update(OrderDto orderDto);

		void Create(OrderDto orderDto);

		bool Contains(int orderId);
	}
}