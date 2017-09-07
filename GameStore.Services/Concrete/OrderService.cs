using AutoMapper;
using GameStore.Common.Entities;
using GameStore.Common.Enums;
using GameStore.Common.Infrastructure.Extensions;
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

		public OrderDto GetSingleActiveOrDefault(int userId)
		{
			var order = _orderRepository.GetSingleOrDefault(o => o.User.Id == userId && o.OrderStatus == OrderStatus.Active);

			if (order == null)
			{
				return null;
			}

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

		public OrderDto GetSingleOrDefault(int orderId)
		{
			var order = _orderRepository.GetSingleOrDefault(o => o.Id == orderId);

			if (order == null)
			{
				return null;
			}

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
			var order = _mapper.Map<OrderDto, Order>(orderDto);
			var existingOrder = _orderRepository.GetSingle(o => o.Id == orderDto.Id);

			UpdateOrderDetails(order, existingOrder);
			CalculateTotalPrice(existingOrder);
			_orderRepository.Update(existingOrder);
			_unitOfWork.Save();
		}

		public void Create(OrderDto orderDto)
		{
			var order = _mapper.Map<OrderDto, Order>(orderDto);
			order = MapEmbeddedEntities(order);
			order.OrderStatus = OrderStatus.Active;

			CalculateTotalPrice(order);
			_orderRepository.Insert(order);
			_unitOfWork.Save();
		}

		private void UpdateOrderDetails(Order input, Order result)
		{
			var deletedOrderDetails = result.OrderDetails.Except(input.OrderDetails, o => o.Id).ToList();
			var addedOrderDetails = input.OrderDetails.Except(result.OrderDetails, o => o.Id).ToList();

			deletedOrderDetails.ForEach(o => result.OrderDetails.Remove(o));

			foreach (var detail in addedOrderDetails)
			{
				detail.Game = _gameRepository.GetSingle(g => g.Key == detail.GameKey);
				result.OrderDetails.Add(detail);
			}

			foreach (var detail in result.OrderDetails)
			{
				detail.Quantity = input.OrderDetails.First(o => o.Id == detail.Id).Quantity;
				detail.Price = detail.Game.Price * detail.Quantity;
			}
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