using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
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
		private Mock<IPlatformTypeRepository> _mockOfPlatformTypeRepository;
		private PlatformTypeService _target;
		private List<PlatformType> _platformTypes;
		
		[TestInitialize]
		public void Initialize()
		{
			_mockOfPlatformTypeRepository = new Mock<IPlatformTypeRepository>();
			_target = new PlatformTypeService(_mapper, _mockOfPlatformTypeRepository.Object);
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

			_mockOfPlatformTypeRepository.Setup(m => m.GetAll(null)).Returns(_platformTypes);

			var result = _target.GetAll().ToList().Count;

			Assert.AreEqual(result, 3);
		}
	}
}