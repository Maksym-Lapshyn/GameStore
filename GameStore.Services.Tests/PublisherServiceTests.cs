using System;
using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.Services.Concrete;
using GameStore.Services.Dtos;
using GameStore.Services.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Services.Abstract;

namespace GameStore.Services.Tests
{
	[TestClass]
	public class PublisherServiceTests
	{
		private const string TestString = "test";

		private readonly IMapper _mapper = new Mapper(
			new MapperConfiguration(cfg => cfg.AddProfile(new ServiceProfile())));

		private Mock<IUnitOfWork> _mockOfUow;
		private Mock<IPublisherRepository> _mockOfPublisherRepository;
		private Mock<IOutputLocalizer<Publisher>> _mockOfOutputLocalizer;
		private Mock<IInputLocalizer<Publisher>> _mockOfInputLocalizer;
		private PublisherService _target;
		private List<Publisher> _publishers;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfUow = new Mock<IUnitOfWork>();
			_mockOfOutputLocalizer = new Mock<IOutputLocalizer<Publisher>>();
			_mockOfInputLocalizer = new Mock<IInputLocalizer<Publisher>>();
			_mockOfPublisherRepository = new Mock<IPublisherRepository>();
			_target = new PublisherService(_mockOfUow.Object, _mapper, _mockOfInputLocalizer.Object, _mockOfOutputLocalizer.Object, _mockOfPublisherRepository.Object);
		}

		[TestMethod]
		public void Create_CreatesPublisher_WhenAnyPublisherIsPassed()
		{
			_publishers = new List<Publisher>();
			_mockOfPublisherRepository.Setup(m => m.Insert(It.IsAny<Publisher>()))
				.Callback<Publisher>(p => _publishers.Add(p));

			_target.Create(TestString, new PublisherDto());
			var result = _publishers.Count;

			Assert.AreEqual(result, 1);
		}

		[TestMethod]
		public void Create_CallsSaveOnce_WhenAnyPublisherIsPassed()
		{
			_publishers = new List<Publisher>();
			_mockOfPublisherRepository.Setup(m => m.Insert(It.IsAny<Publisher>()))
				.Callback<Publisher>(p => _publishers.Add(p));

			_target.Create(TestString, new PublisherDto());

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public void GetStingleBy_ReturnsPublisher_WhenValidPublisherIdIsPassed()
		{
			var publisher = new Publisher
			{
				CompanyName = TestString
			};

			_mockOfPublisherRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Publisher, bool>>>())).Returns(publisher);

			var result = _target.GetSingle(TestString, TestString).CompanyName;

			Assert.AreEqual(result, TestString);
		}

		[TestMethod]
		public void GetAll_ReturnsAllPublishers()
		{
			_publishers = new List<Publisher>
			{
				new Publisher(),
				new Publisher(),
				new Publisher()
			};

			_mockOfPublisherRepository.Setup(m => m.GetAll(null)).Returns(_publishers);

			var result = _target.GetAll(TestString).ToList().Count;

			Assert.AreEqual(result, 3);
		}

		[TestMethod]
		public void GetSingleBy_ReturnsPublisher_WhenValidCompanyNameIsPassed()
		{
			var publisher = new Publisher
			{
				CompanyName = TestString
			};

			_mockOfPublisherRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Publisher, bool>>>())).Returns(publisher);

			var result = _target.GetSingle(TestString, TestString).CompanyName;

			Assert.AreEqual(result, TestString);
		}
	}
}