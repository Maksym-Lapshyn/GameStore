using GameStore.DAL.Entities;
using GameStore.Services.Dtos;
using GameStore.Services.DTOs;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IOrderService : IService
	{
		void Create(OrderDto orderDto);

		void Edit(OrderDto orderDto, string gameKey);

		OrderDto GetSingleBy(string customerId);

		IEnumerable<Order> GetAll(OrderFilterDto orderFilter = null);
	}
}