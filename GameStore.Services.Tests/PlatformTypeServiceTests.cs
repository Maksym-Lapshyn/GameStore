using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Concrete;
using GameStore.Services.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Tests
{
	[TestClass]
	public class PlatformTypeServiceTests
	{
		private readonly IMapper _mapper = new Mapper(
			new MapperConfiguration(cfg => cfg.AddProfile(new ServiceProfile())));
		private Mock<IUnitOfWork> _mockOfUow;
		private PlatformTypeService _target;
		private List<PlatformType> _platformTypes;
		
		[TestInitialize]
		public void Initialize()
		{
			_mockOfUow = new Mock<IUnitOfWork>();
			_target = new PlatformTypeService(_mockOfUow.Object, _mapper);
		}

		[TestMethod]
		public void GetAll_ReturnsAllPlatformTypes()
		{
			_platformTypes = new List<PlatformType>
			{
				new PlatformType(),
				new PlatformType(),
				new PlatformType()
			};

			_mockOfUow.Setup(m => m.PlatformTypeRepository.Get()).Returns(_platformTypes.AsQueryable);

			var result = _target.GetAll().ToList().Count;

			Assert.AreEqual(result, 3);
		}
	}
}
