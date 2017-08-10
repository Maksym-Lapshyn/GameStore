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
	public class PublisherServiceTests
	{
		private const string TestString = "test";
		private readonly IMapper _mapper = new Mapper(
			new MapperConfiguration(cfg => cfg.AddProfile(new ServiceProfile())));
		private Mock<IUnitOfWork> _mockOfUow;
		private Mock<IPublisherRepository> _mockOfPublisherRepository;
		private Mock<IGameRepository> _mockOfGameRepository;
		private PublisherService _target;
		private List<Publisher> _publishers;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfUow = new Mock<IUnitOfWork>();
			_mockOfPublisherRepository = new Mock<IPublisherRepository>();
			_mockOfGameRepository = new Mock<IGameRepository>();
			_target = new PublisherService(_mockOfUow.Object, _mapper, _mockOfPublisherRepository.Object, _mockOfGameRepository.Object);
		}

		[TestMethod]
		public void Create_CreatesPublisher_WhenAnyPublisherIsPassed()
		{
			_publishers = new List<Publisher>();
			_mockOfPublisherRepository.Setup(m => m.Insert(It.IsAny<Publisher>()))
				.Callback<Publisher>(p => _publishers.Add(p));

			_target.Create(new PublisherDto());
			var result = _publishers.Count;

			Assert.AreEqual(result, 1);
		}

		[TestMethod]
		public void Create_CallsSaveOnce_WhenAnyPublisherIsPassed()
		{
			_publishers = new List<Publisher>();
			_mockOfPublisherRepository.Setup(m => m.Insert(It.IsAny<Publisher>()))
				.Callback<Publisher>(p => _publishers.Add(p));

			_target.Create(new PublisherDto());

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public void GetStingleBy_ReturnsPublisher_WhenValidPublisherIdIsPassed()
		{
			var publisher = new Publisher
			{
				CompanyName = TestString
			};

			_mockOfPublisherRepository.Setup(m => m.GetSingle(TestString)).Returns(publisher);

			var result = _target.GetSingle(TestString).CompanyName;

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

			_mockOfPublisherRepository.Setup(m => m.GetAll()).Returns(_publishers);

			var result = _target.GetAll().ToList().Count;

			Assert.AreEqual(result, 3);
		}

		[TestMethod]
		public void GetSingleBy_ReturnsPublisher_WhenValidCompanyNameIsPassed()
		{
			var publisher = new Publisher
			{
				CompanyName = TestString
			};

			_mockOfPublisherRepository.Setup(m => m.GetSingle(TestString)).Returns(publisher);

			var result = _target.GetSingle(TestString).CompanyName;

			Assert.AreEqual(result, TestString);
		}
	}
}