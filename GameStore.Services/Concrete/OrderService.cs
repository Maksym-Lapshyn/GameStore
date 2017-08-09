using AutoMapper;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

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
			order.OrderDate = DateTime.UtcNow;
			Map(order);
			_orderRepository.Insert(order);
			_unitOfWork.Save();
		}

		public OrderDto GetSingleBy(string customerId)
		{
			Order order;

			if (!_orderRepository.Contains(customerId))
			{
				order = new Order
				{
					CustomerId = customerId,
					OrderDate = DateTime.UtcNow
				};
			}
			else
			{
				order = _orderRepository.GetSingle(customerId);
			}

			var orderDto = _mapper.Map<Order, OrderDto>(order);

			return orderDto;
		}

		public void Update(OrderDto orderDto, string gameKey)
		{
			var order = _orderRepository.Contains(orderDto.CustomerId) ? _orderRepository.GetSingle(orderDto.CustomerId) : _mapper.Map<OrderDto, Order>(orderDto);
			var details = order.OrderDetails.FirstOrDefault(o => o.GameKey == gameKey);

			if (details == null)
			{
				var game = _gameRepository.GetSingle(gameKey);
				order.OrderDetails.Add(new OrderDetails{GameKey = gameKey, Game = game, Price = game.Price, Quantity = 1});
			}
			else
			{
				details.Quantity++;
				details.Price = details.Quantity * details.Game.Price;
			}

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

		private void Map(Order output)
		{
			output.OrderDetails.ToList().ForEach(o =>
			{
				o.Game = _gameRepository.GetSingle(o.GameKey);
				o.Price = o.Game.Price;
			});
		}
	}
}