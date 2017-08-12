using AutoMapper;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Infrastructure;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Common.Entities;

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

		public OrderDto GetSingle(string userLogin)
		{
            var order = _orderRepository.GetSingle(o => o.UserLogin == userLogin);
            var orderDto = _mapper.Map<Order, OrderDto>(order);

			return orderDto;
		}

        public void Update(OrderDto orderDto)
		{
            var order = _orderRepository;
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

        public bool Contains(string userLogin)
        {
            return _orderRepository.Contains(o => o.UserLogin == userLogin);
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