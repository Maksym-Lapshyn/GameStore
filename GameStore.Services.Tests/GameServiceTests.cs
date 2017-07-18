using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
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
	public class GameServiceTests
	{
		private Mock<IUnitOfWork> _mockOfUow;
		private readonly IMapper _mapper = new Mapper(
			new MapperConfiguration(cfg => cfg.AddProfile(new ServiceProfile())));
		private readonly IPipeline<IQueryable<Game>> _pipeline = new GamePipeline();
		private GameService _target;
		private const int TestInt = 10;
		private const string TestString = "test";
		private List<Game> _games;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfUow = new Mock<IUnitOfWork>();
			_mockOfUow.Setup(m => m.GameRepository.Insert(It.IsAny<Game>())).Callback<Game>(g => _games.Add(g));
			_mockOfUow.Setup(m => m.PublisherRepository.Get(It.IsAny<int>())).Returns(new Publisher());
			_mockOfUow.Setup(m => m.PlatformTypeRepository.Get(It.IsAny<int>())).Returns(new PlatformType());
			_mockOfUow.Setup(m => m.GenreRepository.Get(It.IsAny<int>())).Returns(new Genre());
			_target = new GameService(_mockOfUow.Object, _mapper, _pipeline);
		}

		[TestMethod]
		public void Create_CreatesGame_WhenAnyGameIsPassed()
		{
			_games = new List<Game>();
			_mockOfUow.Setup(m => m.GameRepository.Insert(It.IsAny<Game>())).Callback<Game>(g => _games.Add(g));

			_target.Create(new GameDto());

			Assert.IsTrue(_games.Count == 1);
		}

		[TestMethod]
		public void Create_CallsSaveOnce_WhenAnyGameIsPassed()
		{
			_games = new List<Game>();
			_target.Create(new GameDto());

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public void Edit_UpdatesGame_WhenValidGamePassed()
		{
			_games = new List<Game>
			{
				new Game { Id = TestInt, Name = TestString }
			};

			_mockOfUow.Setup(m => m.GameRepository.Get()).Returns(_games.AsQueryable);
			_mockOfUow.Setup(m => m.GameRepository.Update(It.IsAny<Game>())).Callback<Game>(g => _games.First(game => game.Id == g.Id).Name = g.Name);
			_target.Edit(new GameDto
			{
				Id = TestInt,
				Name = "testtest"
			});

			var result = _games.First().Name;

			Assert.IsTrue(result == "testtest");
		}

		[TestMethod]
		public void Edit_CallsSaveOnce_WhenValidGameIsPassed()
		{
			_games = new List<Game>
			{
				new Game { Id = TestInt }
			};

			_mockOfUow.Setup(m => m.GameRepository.Get()).Returns(_games.AsQueryable);
			_mockOfUow.Setup(m => m.GameRepository.Update(It.IsAny<Game>())).Callback<Game>(g => _games.First(game => game.Id == g.Id).Name = g.Name);
			_target.Edit(new GameDto
			{
				Id = TestInt
			});

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public void Delete_CallsDeleteOnce_WhenValidGameIdIsPassed()
		{
			_target.Delete(TestInt);

			_mockOfUow.Verify(m => m.GameRepository.Delete(TestInt), Times.Once);
		}

		[TestMethod]
		public void Delete_CallsSaveOnce_WhenValidGameIdIsPassed()
		{
			_target.Delete(TestInt);

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public void GetSingleBy_ReturnsGame_WhenValidGameIdIsPassed()
		{
			_games = new List<Game>
			{
				new Game {Id = TestInt, Name = TestString }
			};

			_mockOfUow.Setup(m => m.GameRepository.Get()).Returns(_games.AsQueryable);

			var result = _target.GetSingleBy(TestInt).Name;

			Assert.IsTrue(result == TestString);
		}

		[TestMethod]
		public void GetSingleBy_ReturnsGame_WhenValidGameKeyIsPassed()
		{
			_games = new List<Game>
			{
				new Game {Id = TestInt, Key = TestString }
			};

			_mockOfUow.Setup(m => m.GameRepository.Get()).Returns(_games.AsQueryable);

			var result = _target.GetSingleBy(TestString).Key;

			Assert.IsTrue(result == TestString);
		}

		[TestMethod]
		public void GetAll_ReturnsAllGames()
		{
			_games = new List<Game>
			{
				new Game(),
				new Game(),
				new Game()
			};

			_mockOfUow.Setup(m => m.GameRepository.Get()).Returns(_games.AsQueryable);

			var games = _target.GetAll().ToList();

			Assert.IsTrue(games.Count == 3);
		}

		[TestMethod]
		public void GetBy_ReturnsAllGames_WhenValidGenreIsPassed()
		{
			_games = new List<Game>
			{
				new Game {Genres = new List<Genre> {new Genre {Name = TestString } } },
				new Game {Genres = new List<Genre> {new Genre {Name = TestString } } },
				new Game {Genres = new List<Genre> {new Genre {Name = TestString } } }
			};

			_mockOfUow.Setup(m => m.GameRepository.Get()).Returns(_games.AsQueryable);

			var result = _target.GetBy(TestString).ToList().Count;

			Assert.IsTrue(result == 3);
		}

		[TestMethod]
		public void GetBy_ReturnsAllGames_WhenValidPlatformTypeIsPassed()
		{
			_games = new List<Game>
			{
				new Game {PlatformTypes = new List<PlatformType> {new PlatformType {Type = TestString } } },
				new Game {PlatformTypes = new List<PlatformType> {new PlatformType {Type = TestString } } },
				new Game {PlatformTypes = new List<PlatformType> {new PlatformType {Type = TestString } } }
			};

			_mockOfUow.Setup(m => m.GameRepository.Get()).Returns(_games.AsQueryable);

			var result = _target.GetBy(new List<string> { TestString }).ToList().Count;

			Assert.IsTrue(result == 3);
		}
	}
}