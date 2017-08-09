using AutoMapper;
using GameStore.DAL.Abstract.Common;
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
	public class GenreServiceTests
	{
		private readonly IMapper _mapper = new Mapper(
			new MapperConfiguration(cfg => cfg.AddProfile(new ServiceProfile())));
		private Mock<IGenreRepository> _mockOfGenreRepository;
		private GenreService _target;
		private List<Genre> _genres;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfGenreRepository = new Mock<IGenreRepository>();
			_target = new GenreService(_mapper, _mockOfGenreRepository.Object);
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

			_mockOfGenreRepository.Setup(m => m.GetAll()).Returns(_genres);

			var result = _target.GetAll().ToList().Count;

			Assert.AreEqual(result, 3);
		}
	}
}
