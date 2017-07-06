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

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(OrderDto orderDto)
        {
            var order = Mapper.Map<OrderDto, Order>(orderDto);
            order.Date = DateTime.UtcNow;
            MergeToEntity(order);
            _unitOfWork.OrderRepository.Insert(order);
            _unitOfWork.Save();
        }

        public OrderDto GetSingleBy(string customerId)
        {
            var order = _unitOfWork.OrderRepository.Get(o => o.CustomerId == customerId).First();
            var orderDto = Mapper.Map<Order, OrderDto>(order);

            return orderDto;
        }

        public void Edit(OrderDto orderDto)
        {
            var order = _unitOfWork.OrderRepository.Get(o => o.Id == orderDto.Id).First();
            order = Mapper.Map(orderDto, order);
            MergeToEntity(order);
            _unitOfWork.OrderRepository.Update(order);
            _unitOfWork.Save();
        }

        private void MergeToEntity(Order output)
        {
            output.OrderDetails.ToList().ForEach(o =>
            {
                o.Game = _unitOfWork.GameRepository.GetById(o.GameId);
                o.Price = o.Game.Price;
            });
        }
    }
}