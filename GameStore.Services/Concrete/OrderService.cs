﻿using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.EntityFramework;
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
		private readonly IEfOrderRepository _orderRepository;
		private readonly IEfGameRepository _gameRepository;

		public OrderService(IUnitOfWork unitOfWork,
			IMapper mapper,
			IEfOrderRepository orderRepository,
			IEfGameRepository gameRepository)
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
					CustomerId = customerId
				};
			}
			else
			{
				order = _orderRepository.Get(customerId);
			}

			var orderDto = _mapper.Map<Order, OrderDto>(order);

			return orderDto;
		}

		public void Edit(OrderDto orderDto, string gameKey)
		{
			var order = _orderRepository.Get(orderDto.CustomerId);
			var details = order.OrderDetails.FirstOrDefault(o => o.GameKey == gameKey);

			if (details == null)
			{
				var game = _gameRepository.Get(gameKey);
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

		private void Map(Order output)
		{
			output.OrderDetails.ToList().ForEach(o =>
			{
				o.Game = _gameRepository.Get(o.GameKey);
				o.Price = o.Game.Price;
			});
		}
	}
}