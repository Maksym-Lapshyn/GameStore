using AutoMapper;
using GameStore.Common.Entities;
using GameStore.Common.Enums;
using GameStore.DAL.Abstract.Common;
using GameStore.Services.Concrete;
using GameStore.Services.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.Services.Tests
{
	[TestClass]
	public class OrderServiceTests
	{
		private const string ValidString = "test";
		private const int ValidInt = 10;
		private const int InvalidInt = 0;
		private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new ServiceProfile())));
		private Mock<IUnitOfWork> _mockOfUow;
		private Mock<IOrderRepository> _mockOfOrderRepository;
		private Mock<IGameRepository> _mockOfGameRepository;
		private Mock<IUserRepository> _mockOfUserRepository;
		private OrderService _target;
		private List<Order> _orders;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfUow = new Mock<IUnitOfWork>();
			_mockOfOrderRepository = new Mock<IOrderRepository>();
			_mockOfGameRepository = new Mock<IGameRepository>();
			_mockOfUserRepository = new Mock<IUserRepository>();
			_orders = new List<Order>();
			_mockOfUserRepository.Setup(m => m.GetSingle(u => u.Id == ValidInt));
			_mockOfOrderRepository.Setup(m => m.Insert(It.IsAny<Order>())).Callback<Order>(o => _orders.Add(o));
			_target = new OrderService(_mockOfUow.Object, _mapper, _mockOfOrderRepository.Object, _mockOfGameRepository.Object,
				_mockOfUserRepository.Object);
		}

		[TestMethod]
		public void CreateActive_CreatesActiveOrder()
		{
			_target.CreateActive(ValidInt);

			Assert.AreEqual(1, _orders.Count);
			Assert.AreEqual(OrderStatus.Active, _orders.First().OrderStatus);
		}

		[TestMethod]
		public void CreateActive_CallsSaveOnce_WhenValidUserIdIsPassed()
		{
			_target.CreateActive(ValidInt);

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public void AddDetails_AddsOrderDetails_IfThereIsNoSuchDetails()
		{
			var order = new Order
			{
				OrderDetails = new List<OrderDetails>()
			};

			_mockOfOrderRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Order, bool>>>())).Returns(order);
			_mockOfGameRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Game, bool>>>())).Returns(new Game
			{
				Key = ValidString,
				Price = ValidInt
			});

			_target.AddDetails(ValidInt, ValidString);

			Assert.AreEqual(1, order.OrderDetails.Count);
		}

		[TestMethod]
		public void AddDetails_IncreasesCount_IfThereIsSuchDetails()
		{
			var game = new Game
			{
				Key = ValidString,
				Price = ValidInt
			};

			var order = new Order
			{
				OrderDetails = new List<OrderDetails>
				{
					new OrderDetails
					{
						Game = game,
						GameKey = ValidString,
						Quantity = 1
					}
				}
			};

			_mockOfOrderRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Order, bool>>>())).Returns(order);
			_mockOfGameRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Game, bool>>>())).Returns(game);

			_target.AddDetails(ValidInt, ValidString);

			Assert.AreEqual(1, order.OrderDetails.Count);
			Assert.AreEqual(2, order.OrderDetails.First().Quantity);
		}

		[TestMethod]
		public void AddDetails_CalculatesTotalPrice_WhenAnyOrderIdAndGameKeyArePassed()
		{
			var order = new Order
			{
				OrderDetails = new List<OrderDetails>()
			};

			_mockOfOrderRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Order, bool>>>())).Returns(order);
			_mockOfGameRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Game, bool>>>())).Returns(new Game
			{
				Key = ValidString,
				Price = ValidInt
			});

			_target.AddDetails(ValidInt, ValidString);

			Assert.AreEqual(10, order.TotalPrice);
		}

		[TestMethod]
		public void AddDetails_CallsSaveOnce_WhenAnyOrderIdAndGameKeyArePassed()
		{
			var order = new Order
			{
				OrderDetails = new List<OrderDetails>()
			};

			_mockOfOrderRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Order, bool>>>())).Returns(order);
			_mockOfGameRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Game, bool>>>())).Returns(new Game
			{
				Key = ValidString,
				Price = ValidInt
			});

			_target.AddDetails(ValidInt, ValidString);

			_mockOfUow.Verify(m => m.Save(), Times.Once());
		}

		[TestMethod]
		public void DeleteDetails_LowersQuantity_IfQuantityIsHigherThanOne()
		{
			var game = new Game
			{
				Key = ValidString,
				Price = ValidInt
			};

			var order = new Order
			{
				OrderDetails = new List<OrderDetails>
				{
					new OrderDetails
					{
						Game = game,
						GameKey = ValidString,
						Quantity = 2
					}
				}
			};

			_mockOfOrderRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Order, bool>>>())).Returns(order);
			_mockOfGameRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Game, bool>>>())).Returns(game);

			_target.DeleteDetails(ValidInt, ValidString);

			Assert.AreEqual(1, order.OrderDetails.First().Quantity);
		}

		[TestMethod]
		public void DeleteDetails_DeletesDetails_IfQuantityIsOne()
		{
			var game = new Game
			{
				Key = ValidString,
				Price = ValidInt
			};

			var order = new Order
			{
				OrderDetails = new List<OrderDetails>
				{
					new OrderDetails
					{
						Game = game,
						GameKey = ValidString,
						Quantity = 1
					}
				}
			};

			_mockOfOrderRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Order, bool>>>())).Returns(order);
			_mockOfGameRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Game, bool>>>())).Returns(game);

			_target.DeleteDetails(ValidInt, ValidString);

			Assert.AreEqual(0, order.OrderDetails.Count);
		}

		[TestMethod]
		public void DeleteDetails_CalculatesTotalPrice_WhenAnyOrderIdAndGameKeyArePassed()
		{
			var game = new Game
			{
				Key = ValidString,
				Price = ValidInt
			};

			var order = new Order
			{
				TotalPrice = ValidInt,
				OrderDetails = new List<OrderDetails>
				{
					new OrderDetails
					{
						Game = game,
						GameKey = ValidString,
						Quantity = 1
					}
				}
			};

			_mockOfOrderRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Order, bool>>>())).Returns(order);
			_mockOfGameRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Game, bool>>>())).Returns(new Game
			{
				Key = ValidString,
				Price = ValidInt
			});

			_target.DeleteDetails(ValidInt, ValidString);

			Assert.AreEqual(InvalidInt, order.TotalPrice);
		}

		[TestMethod]
		public void DeleteDetails_CallsSaveOnce_WhenAnyOrderIdAndGameKeyArePassed()
		{
			var order = new Order
			{
				OrderDetails = new List<OrderDetails>()
			};

			_mockOfOrderRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Order, bool>>>())).Returns(order);
			_mockOfGameRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Game, bool>>>())).Returns(new Game
			{
				Key = ValidString,
				Price = ValidInt
			});

			_target.DeleteDetails(ValidInt, ValidString);

			_mockOfUow.Verify(m => m.Save(), Times.Once());
		}

		[TestMethod]
		public void Confirm_ChangeOrderStatus_WhenAnyOrderIdIsPassed()
		{
			var order = new Order();
			_mockOfOrderRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Order, bool>>>())).Returns(order);

			_target.Confirm(ValidInt);

			Assert.AreEqual(OrderStatus.Paid, order.OrderStatus);
		}

		[TestMethod]
		public void Confirm_CallsSaveOnce_WhenAnyOrderIdIsPassed()
		{
			var order = new Order();
			_mockOfOrderRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Order, bool>>>())).Returns(order);

			_target.Confirm(ValidInt);

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public void Ship_ChangeOrderStatus_WhenAnyOrderIdIsPassed()
		{
			var order = new Order();
			_mockOfOrderRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Order, bool>>>())).Returns(order);

			_target.Ship(ValidInt);

			Assert.AreEqual(OrderStatus.Shipped, order.OrderStatus);
		}

		[TestMethod]
		public void Shup_CallsSaveOnce_WhenAnyOrderIdIsPassed()
		{
			var order = new Order();
			_mockOfOrderRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Order, bool>>>())).Returns(order);

			_target.Ship(ValidInt);

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}
	}
}