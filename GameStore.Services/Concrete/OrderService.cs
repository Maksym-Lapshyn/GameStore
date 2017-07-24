using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using System;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public void Create(OrderDto orderDto)
		{
			var order = _mapper.Map<OrderDto, Order>(orderDto);
			order.Date = DateTime.UtcNow;
			Map(order);
			_unitOfWork.OrderGenericRepository.Insert(order);
			_unitOfWork.Save();
		}

		public OrderDto GetSingleBy(string customerId)
		{
			var order = _unitOfWork.OrderGenericRepository.Get().First(o => o.CustomerId == customerId);
			var orderDto = _mapper.Map<Order, OrderDto>(order);

			return orderDto;
		}

		public void Edit(OrderDto orderDto, int gameId)
		{
			var order = _unitOfWork.OrderGenericRepository.Get(orderDto.Id);
			var details = order.OrderDetails.FirstOrDefault(o => o.GameId == gameId);

			if (details == null)
			{
				var game = _unitOfWork.GameGenericRepository.Get(gameId);
				order.OrderDetails.Add(new OrderDetails{GameId = gameId, Game = game, Price = game.Price, Quantity = 1});
			}
			else
			{
				details.Quantity++;
				details.Price = details.Quantity * details.Game.Price;
			}

			_unitOfWork.OrderGenericRepository.Update(order);
			_unitOfWork.Save();
		}

		private void Map(Order output)
		{
			output.OrderDetails.ToList().ForEach(o =>
			{
				o.Game = _unitOfWork.GameGenericRepository.Get(o.GameId);
				o.Price = o.Game.Price;
			});
		}
	}
}