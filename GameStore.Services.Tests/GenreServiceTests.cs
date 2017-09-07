using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.Services.Abstract;
using GameStore.Services.Concrete;
using GameStore.Services.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Tests
{
	[TestClass]
	public class GenreServiceTests
	{
		private const string TestString = "test";

		private readonly IMapper _mapper = new Mapper(
			new MapperConfiguration(cfg => cfg.AddProfile(new ServiceProfile())));

		private Mock<IGenreRepository> _mockOfGenreRepository;
		private Mock<IGameRepository> _mockOfGameRepository;
		private Mock<IOutputLocalizer<Genre>> _mockOfOutputLocalizer;
		private Mock<IInputLocalizer<Genre>> _mockOfInputLocalizer;
		private Mock<IUnitOfWork> _mockOfUow;
		private GenreService _target;
		private List<Genre> _genres;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfGenreRepository = new Mock<IGenreRepository>();
			_mockOfGameRepository = new Mock<IGameRepository>();
			_mockOfOutputLocalizer = new Mock<IOutputLocalizer<Genre>>();
			_mockOfInputLocalizer = new Mock<IInputLocalizer<Genre>>();
			_mockOfUow = new Mock<IUnitOfWork>();
			_target = new GenreService(_mockOfUow.Object, _mapper, _mockOfInputLocalizer.Object, _mockOfOutputLocalizer.Object, _mockOfGameRepository.Object, _mockOfGenreRepository.Object);
		}

		[TestMethod]
		public void GetAll_ReturnsAllGenres()
		{
			_genres = new List<Genre>
			{
				new Genre(),
				new Genre(),
				new Genre()
			};

			_mockOfGenreRepository.Setup(m => m.GetAll(null)).Returns(_genres);

			var result = _target.GetAll(TestString).ToList().Count;

			Assert.AreEqual(result, 3);
		}
	}
}
