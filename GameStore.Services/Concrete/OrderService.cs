using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Infrastructure;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Common.Enums;

namespace GameStore.Services.Concrete
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IOrderRepository _orderRepository;
		private readonly IGameRepository _gameRepository;

		public OrderService(IUnitOfWork unitOfWork,
			IMapper mapper,
			IOrderRepository orderRepository,
			IGameRepository gameRepository)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_orderRepository = orderRepository;
			_gameRepository = gameRepository;
		}

		public void Create(OrderDto orderDto)
		{
			var order = _mapper.Map<OrderDto, Order>(orderDto);
			order.DateOrdered = DateTime.UtcNow;
			Map(order);
			_orderRepository.Insert(order);
			_unitOfWork.Save();
		}

		public OrderDto GetSingleActive(int userId)
		{
			var order = _orderRepository.GetSingle(o => o.UserId == userId && o.OrderStatus == OrderStatus.Active);
			var orderDto = _mapper.Map<Order, OrderDto>(order);

			return orderDto;
		}

		public void Update(OrderDto orderDto)
		{
			var order = _orderRepository.GetSingle(o => o.Id == orderDto.Id);
			order = _mapper.Map(orderDto, order);
			_orderRepository.Update(order);
			_unitOfWork.Save();
		}

		public IEnumerable<OrderDto> GetAll(OrderFilterDto orderFilter = null)
		{
			IEnumerable<Order> orders;

			if (orderFilter != null)
			{
				orders = _orderRepository.GetAll(_mapper.Map<OrderFilterDto, OrderFilter>(orderFilter));

				return _mapper.Map<IEnumerable<Order>, List<OrderDto>>(orders);
			}

			orders = _orderRepository.GetAll(_mapper.Map<OrderFilterDto, OrderFilter>(null));

			return _mapper.Map<IEnumerable<Order>, List<OrderDto>>(orders);
		}

		public bool ContainsActive(int userId)
		{
			return _orderRepository.Contains(o => o.UserId == userId && o.OrderStatus == OrderStatus.Active);
		}

		public void Buy(int orderId, string gameKey)
		{
			var order = _orderRepository.GetSingle(o => o.Id == orderId && o.OrderStatus == OrderStatus.Active);

			if (order.OrderDetails.Any(o => o.GameKey == gameKey))
			{
				var orderDetails = order.OrderDetails.First(o => o.GameKey == gameKey);
				orderDetails.Quantity++;
				orderDetails.Price = orderDetails.Game.Price * orderDetails.Quantity;
			}

			var game = _gameRepository.GetSingle(g => g.Key == gameKey);
			order.OrderDetails.Add(new OrderDetails
			{
				Game = game,
				GameKey = gameKey,
				Price = game.Price,
				Quantity = 1
			});

			_orderRepository.Update(order);
			_unitOfWork.Save();
		}

		private void Map(Order output)
		{
			output.OrderDetails.ToList().ForEach(o =>
			{
				o.Game = _gameRepository.GetSingle(g => g.Key == o.GameKey);
				o.Price = o.Game.Price;
			});
		}
	}
}