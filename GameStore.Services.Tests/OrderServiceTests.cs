using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Concrete;
using GameStore.Services.DTOs;
using GameStore.Services.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Tests
{
	[TestClass]
	public class OrderServiceTests
	{
		private Mock<IUnitOfWork> _mockOfUow;
		private OrderService _target;
		private List<Order> _orders;
		private readonly IMapper _mapper = new Mapper(
			new MapperConfiguration(cfg => cfg.AddProfile(new ServiceProfile())));
		private const string ValidString = "test";
		private const string InvalidString = "testtest";
		private const int ValidInt = 10;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfUow = new Mock<IUnitOfWork>();
			_mockOfUow.Setup(m => m.OrderRepository.Insert(It.IsAny<Order>())).Callback<Order>(o => _orders.Add(o));
			_mockOfUow.Setup(m => m.GameRepository.Get(It.IsAny<int>())).Returns(new Game());
			_target = new OrderService(_mockOfUow.Object, _mapper);
		}

		[TestMethod]
		public void Create_CreatesOrder_WhenAnyGameIsPassed()
		{
			_orders = new List<Order>();
			_target.Create(new OrderDto());

			var result = _orders.Count;

			Assert.IsTrue(result == 1);
		}

		[TestMethod]
		public void Create_CallsSaveOnce_WhenAnyGameIsPassed()
		{
			_orders = new List<Order>();

			_target.Create(new OrderDto());

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public void GetSingleBy_ReturnsOrder_WhenValidCustomerIdIsPassed()
		{
			_orders = new List<Order>
			{
				new Order{CustomerId = ValidString}
			};

			_mockOfUow.Setup(m => m.OrderRepository.Get()).Returns(_orders.AsQueryable);

			var result = _target.GetSingleBy(ValidString);

			Assert.IsTrue(result.CustomerId == ValidString);
		}

		[ExpectedException(typeof(InvalidOperationException))]
		[TestMethod]
		public void GetSingleBy_ThrowsArgumentException_WhenInValidCustomerIdIsPassed()
		{
			_orders = new List<Order>
			{
				new Order{CustomerId = ValidString}
			};

			_mockOfUow.Setup(m => m.OrderRepository.Get()).Returns(_orders.AsQueryable);

			_target.GetSingleBy(InvalidString);
		}

		[TestMethod]
		public void Edit_Adds_WhenNonExistingOrderDetailsIsPassed()
		{
			var order = new Order
			{
				Id = ValidInt,
				OrderDetails = new List<OrderDetails>()
			};

			var orderDto = new OrderDto { Id = ValidInt };
			_mockOfUow.Setup(m => m.GameRepository.Get(ValidInt)).Returns(new Game{Price = ValidInt});
			_mockOfUow.Setup(m => m.OrderRepository.Get(It.IsAny<int>())).Returns(order);
			_mockOfUow.Setup(m => m.OrderRepository.Update(It.IsAny<Order>())).Callback<Order>(o => order = o);

			_target.Edit(orderDto, ValidInt);
			var result = order.OrderDetails.Count;

			Assert.IsTrue(result == 1);
		}

		[TestMethod]
		public void Edit_Updates_WhenExistingOrderDetailsIsPassed()
		{
			var order = new Order
			{
				Id = ValidInt,
				OrderDetails = new List<OrderDetails>
				{
					new OrderDetails
					{
						GameId = ValidInt,
						Quantity = ValidInt,
						Game = new Game
						{
							Price = ValidInt
						}
					}
				}
			};

			var orderDto = new OrderDto { Id = ValidInt};
			_mockOfUow.Setup(m => m.GameRepository.Get(ValidInt)).Returns(new Game { Price = ValidInt });
			_mockOfUow.Setup(m => m.OrderRepository.Get(It.IsAny<int>())).Returns(order);
			_mockOfUow.Setup(m => m.OrderRepository.Update(It.IsAny<Order>())).Callback<Order>(o => order = o);

			_target.Edit(orderDto, ValidInt);
			var result = order.OrderDetails.First().Quantity;

			Assert.IsTrue(result == (ValidInt + 1)); // TODO Required: Remove useless '()'
		}

		[TestMethod]
		public void Edit_CallsSaveOnvce_WhenAnyOrderDetailsIsPassed()
		{
			var order = new Order
			{
				Id = ValidInt,
				OrderDetails = new List<OrderDetails>()
			};

			var orderDto = new OrderDto { Id = ValidInt, CustomerId = ValidString };
			_mockOfUow.Setup(m => m.GameRepository.Get(ValidInt)).Returns(new Game { Price = ValidInt });
			_mockOfUow.Setup(m => m.OrderRepository.Get(It.IsAny<int>())).Returns(order);
			_mockOfUow.Setup(m => m.OrderRepository.Update(It.IsAny<Order>())).Callback<Order>(o => order = o);

			_target.Edit(orderDto, ValidInt);

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}
	}
}
