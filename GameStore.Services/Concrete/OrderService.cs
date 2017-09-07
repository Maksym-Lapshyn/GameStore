﻿using AutoMapper;
using GameStore.Common.Entities;
using GameStore.Common.Enums;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Infrastructure;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
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
		private readonly IUserRepository _userRepository;

		public OrderService(IUnitOfWork unitOfWork,
			IMapper mapper,
			IOrderRepository orderRepository,
			IGameRepository gameRepository,
			IUserRepository userRepository)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_orderRepository = orderRepository;
			_gameRepository = gameRepository;
			_userRepository = userRepository;
		}

		public void CreateActive(int userId)
		{
			var order = new Order
			{
				User = _userRepository.GetSingle(u => u.Id == userId),
				OrderDetails = new List<OrderDetails>(),
				OrderStatus = OrderStatus.Active
			};

			_orderRepository.Insert(order);
			_unitOfWork.Save();
		}

		public bool ContainsActiveById(int orderId)
		{
			return _orderRepository.Contains(o => o.Id == orderId && o.OrderStatus == OrderStatus.Active);
		}

		public bool ContainsActive(int userId)
		{
			return _orderRepository.Contains(o => o.User.Id == userId && o.OrderStatus == OrderStatus.Active);
		}

		public OrderDto GetSingleActive(int userId)
		{
			var order = _orderRepository.GetSingle(o => o.User.Id == userId && o.OrderStatus == OrderStatus.Active);
			var orderDto = _mapper.Map<Order, OrderDto>(order);

			return orderDto;
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

		public void AddDetails(int orderId, string gameKey)
		{
			var order = _orderRepository.GetSingle(o => o.Id == orderId);

			if (order.OrderDetails.Any(o => o.GameKey == gameKey))
			{
				var orderDetails = order.OrderDetails.First(o => o.GameKey == gameKey);
				orderDetails.Quantity++;
				orderDetails.Price = orderDetails.Game.Price * orderDetails.Quantity;
			}
			else
			{
				var game = _gameRepository.GetSingle(g => g.Key == gameKey);
				var orderDetails = new OrderDetails
				{
					Game = game,
					GameKey = gameKey,
					Price = game.Price,
					Quantity = 1
				};

				order.OrderDetails.Add(orderDetails);
			}

			order.TotalPrice = CalculateTotalPrice(order);

			_orderRepository.Update(order);
			_unitOfWork.Save();
		}

		public void DeleteDetails(int orderId, string gameKey)
		{
			var order = _orderRepository.GetSingle(o => o.Id == orderId);

			if (order.OrderDetails.Any(o => o.GameKey == gameKey))
			{
				var orderDetails = order.OrderDetails.First(o => o.GameKey == gameKey);

				if (orderDetails.Quantity > 1)
				{
					orderDetails.Quantity--;
					orderDetails.Price = orderDetails.Game.Price * orderDetails.Quantity;
				}
				else
				{
					order.OrderDetails.Remove(orderDetails);
				}
			}

			order.TotalPrice = CalculateTotalPrice(order);

			_orderRepository.Update(order);
			_unitOfWork.Save();
		}

		public OrderDto GetSingle(int orderId)
		{
			var order = _orderRepository.GetSingle(o => o.Id == orderId);
			var orderDto = _mapper.Map<Order, OrderDto>(order);

			return orderDto;
		}

		public void Confirm(int orderId)
		{
			var order = _orderRepository.GetSingle(o => o.Id == orderId);
			order.DateOrdered = DateTime.UtcNow;
			order.OrderStatus = OrderStatus.Paid;
			_orderRepository.Update(order);
			_unitOfWork.Save();
		}

		public void CheckoutActive(int userId)
		{
			var order = _orderRepository.GetSingle(o => o.User.Id == userId && o.OrderStatus == OrderStatus.Active);
			order.DateOrdered = DateTime.UtcNow;
			order.OrderStatus = OrderStatus.Paid;
			_orderRepository.Update(order);
			_unitOfWork.Save();
		}

		public void Ship(int orderId)
		{
			var order = _orderRepository.GetSingle(o => o.Id == orderId);
			order.OrderStatus = OrderStatus.Shipped;
			order.DateShipped = DateTime.UtcNow;
			_orderRepository.Update(order);
			_unitOfWork.Save();
		}

		public bool Contains(int orderId)
		{
			return _orderRepository.Contains(o => o.Id == orderId);
		}

		public void Update(OrderDto orderDto)
		{
			var existingOrder = _orderRepository.GetSingle(o => o.Id == orderDto.Id);
			existingOrder.OrderDetails.Clear();
			_orderRepository.Update(existingOrder);
			_unitOfWork.Save();//clears order details of existing order
			var order = _mapper.Map<OrderDto, Order>(orderDto);
			order = MapEmbeddedEntities(order);
			CalculateTotalPrice(order);
			_orderRepository.Update(order);
			_unitOfWork.Save();//re-enters order details
		}

		public void Create(OrderDto orderDto)
		{
			var order = _mapper.Map<OrderDto, Order>(orderDto);
			order = MapEmbeddedEntities(order);
			CalculateTotalPrice(order);
			order.OrderStatus = OrderStatus.Active;
			_orderRepository.Insert(order);
			_unitOfWork.Save();
		}

		private Order MapEmbeddedEntities(Order order)
		{
			var orderDetailsList = order.OrderDetails.ToList();

			for (var i = 0; i < orderDetailsList.Count; i++)
			{
				orderDetailsList[i].Game = _gameRepository.GetSingle(g => g.Key == orderDetailsList[i].GameKey);
			}

			return order;
		}

		private decimal CalculateTotalPrice(Order order)
		{
			return order.OrderDetails.Sum(orderDetails => orderDetails.Price);
		}
	}
}