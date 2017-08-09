using AutoMapper;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Entities;
using GameStore.Services.Concrete;
using GameStore.Services.DTOs;
using GameStore.Services.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Tests
{
	[TestClass]
	public class OrderServiceTests
	{
		private const string ValidString = "test";
		private const string InvalidString = "testtest";
		private const int ValidInt = 10;
		private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new ServiceProfile())));
		private Mock<IUnitOfWork> _mockOfUow;
		private Mock<IOrderRepository> _mockOfOrderRepository;
		private Mock<IGameRepository> _mockOfGameRepository;
		private OrderService _target;
		private List<Order> _orders;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfUow = new Mock<IUnitOfWork>();
			_mockOfOrderRepository = new Mock<IOrderRepository>();
			_mockOfGameRepository = new Mock<IGameRepository>();
			_mockOfOrderRepository.Setup(m => m.Insert(It.IsAny<Order>())).Callback<Order>(o => _orders.Add(o));
			_mockOfGameRepository.Setup(m => m.GetSingle(It.IsAny<string>())).Returns(new Game());
			_target = new OrderService(_mockOfUow.Object, _mapper, _mockOfOrderRepository.Object, _mockOfGameRepository.Object);
		}

		[TestMethod]
		public void Create_CreatesOrder_WhenAnyGameIsPassed()
		{
			_orders = new List<Order>();
			_target.Create(new OrderDto());

			var result = _orders.Count;

			Assert.AreEqual(result, 1);
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

			_mockOfOrderRepository.Setup(m => m.GetAll(null)).Returns(_orders);

			var result = _target.GetSingleBy(ValidString);

			Assert.AreEqual(result.CustomerId, ValidString);
		}

		[TestMethod]
		public void GetSingleBy_ReturnsEmptyOrderWithPassedCustomerId_WhenInValidCustomerIdIsPassed()
		{
			_orders = new List<Order>
			{
				new Order{CustomerId = ValidString}
			};

			_mockOfOrderRepository.Setup(m => m.GetAll(null)).Returns(_orders);

			var result = _target.GetSingleBy(InvalidString).CustomerId;

			Assert.AreEqual(result, InvalidString);
		}

		[TestMethod]
		public void Updates_CreatesNewOrderDetails_WhenNonExistingOrderDetailsIsPassed()
		{
			var order = new Order
			{
				Id = ValidInt,
				OrderDetails = new List<OrderDetails>()
			};

			var orderDto = new OrderDto { Id = ValidInt };
			_mockOfGameRepository.Setup(m => m.GetSingle(ValidString)).Returns(new Game{ Key = ValidString, Price = ValidInt});
			_mockOfOrderRepository.Setup(m => m.GetSingle(It.IsAny<string>())).Returns(order);
			_mockOfOrderRepository.Setup(m => m.Update(It.IsAny<Order>())).Callback<Order>(o => order = o);

			_target.Update(orderDto, ValidString);
			var result = order.OrderDetails.Count;

			Assert.AreEqual(result, 1);
		}

		[TestMethod]
		public void Update_UpdatesExistingOrderDetails_WhenExistingOrderDetailsIsPassed()
		{
			var order = new Order
			{
				CustomerId = ValidString,
				Id = ValidInt,
				OrderDetails = new List<OrderDetails>
				{
					new OrderDetails
					{
						GameKey = ValidString,
						Quantity = ValidInt,
						Game = new Game
						{
							Price = ValidInt
						}
					}
				}
			};

			var newOrder = new OrderDto
			{
				CustomerId = ValidString,
				Id = ValidInt,
				OrderDetails = new List<OrderDetailsDto>
				{
					new OrderDetailsDto
					{
						GameKey = ValidString,
						Quantity = ValidInt,
						Game = new GameDto
						{
							Price = ValidInt
						}
					}
				}
			};

			_mockOfGameRepository.Setup(m => m.GetSingle(ValidString)).Returns(new Game { Price = ValidInt });
			_mockOfOrderRepository.Setup(m => m.GetSingle(ValidString)).Returns(order);
			_mockOfOrderRepository.Setup(m => m.Contains(ValidString)).Returns(true);

			_target.Update(newOrder, ValidString);
			var result = order.OrderDetails.First().Quantity;

			Assert.AreEqual(ValidInt + 1, result);
		}

		[TestMethod]
		public void Update_CallsSaveOnvce_WhenAnyOrderDetailsIsPassed()
		{
			var order = new Order
			{
				Id = ValidInt,
				OrderDetails = new List<OrderDetails>()
			};

			var orderDto = new OrderDto { Id = ValidInt, CustomerId = ValidString };
			_mockOfGameRepository.Setup(m => m.GetSingle(ValidString)).Returns(new Game { Price = ValidInt });
			_mockOfOrderRepository.Setup(m => m.GetSingle(It.IsAny<string>())).Returns(order);
			_mockOfOrderRepository.Setup(m => m.Update(It.IsAny<Order>())).Callback<Order>(o => order = o);

			_target.Update(orderDto, ValidString);

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}
	}
}
