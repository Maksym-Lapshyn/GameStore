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
		private const int TestInt = 10;
		private const string ValidString = "test";
		private const string InvalidString = "testtest";
		private readonly IMapper _mapper = new Mapper(
			new MapperConfiguration(cfg => cfg.AddProfile(new ServiceProfile())));
		private readonly IPipeline<IQueryable<Game>> _pipeline = new GamePipeline();
		private Mock<IUnitOfWork> _mockOfUow;
		private readonly IFilterMapper _filterMapper = new GameFilterMapper();
		private GameService _target;
		private List<Game> _games;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfUow = new Mock<IUnitOfWork>();
			_mockOfUow.Setup(m => m.GameRepository.Insert(It.IsAny<Game>())).Callback<Game>(g => _games.Add(g));
			_mockOfUow.Setup(m => m.PublisherRepository.Get(It.IsAny<int>())).Returns(new Publisher());
			_mockOfUow.Setup(m => m.PlatformTypeRepository.Get(It.IsAny<int>())).Returns(new PlatformType());
			_mockOfUow.Setup(m => m.GenreRepository.Get(It.IsAny<int>())).Returns(new Genre());
			_target = new GameService(_mockOfUow.Object, _mapper, _pipeline, _filterMapper);
		}

		[TestMethod]
		public void Create_CreatesGame_WhenAnyGameIsPassed()
		{
			_games = new List<Game>();
			_mockOfUow.Setup(m => m.GameRepository.Insert(It.IsAny<Game>())).Callback<Game>(g => _games.Add(g));

			_target.Create(new GameDto());

			Assert.Equals(_games.Count, 1);
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
				new Game { Id = TestInt, Name = ValidString }
			};

			_mockOfUow.Setup(m => m.GameRepository.Get()).Returns(_games.AsQueryable);
			_mockOfUow.Setup(m => m.GameRepository.Update(It.IsAny<Game>())).Callback<Game>(g => _games.First(game => game.Id == g.Id).Name = g.Name);
			_target.Edit(new GameDto
			{
				Id = TestInt,
				Name = InvalidString
			});

			var result = _games.First().Name;

			Assert.Equals(result, InvalidString);
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
				new Game {Id = TestInt, Name = ValidString }
			};

			_mockOfUow.Setup(m => m.GameRepository.Get()).Returns(_games.AsQueryable);

			var result = _target.GetSingleBy(TestInt).Name;

			Assert.Equals(result, ValidString);
		}

		[TestMethod]
		public void GetSingleBy_ReturnsGame_WhenValidGameKeyIsPassed()
		{
			_games = new List<Game>
			{
				new Game {Id = TestInt, Key = ValidString }
			};

			_mockOfUow.Setup(m => m.GameRepository.Get()).Returns(_games.AsQueryable);

			var result = _target.GetSingleBy(ValidString).Key;

			Assert.Equals(result, ValidString);
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

			Assert.Equals(games.Count, 3);
		}

		[TestMethod]
		public void GetBy_ReturnsAllGames_WhenValidGenreIsPassed()
		{
			_games = new List<Game>
			{
				new Game {Genres = new List<Genre> {new Genre {Name = ValidString } } },
				new Game {Genres = new List<Genre> {new Genre {Name = ValidString } } },
				new Game {Genres = new List<Genre> {new Genre {Name = ValidString } } }
			};

			_mockOfUow.Setup(m => m.GameRepository.Get()).Returns(_games.AsQueryable);

			var result = _target.GetBy(ValidString).ToList().Count;

			Assert.Equals(result, 3);
		}

		[TestMethod]
		public void GetBy_ReturnsAllGames_WhenValidPlatformTypeIsPassed()
		{
			_games = new List<Game>
			{
				new Game {PlatformTypes = new List<PlatformType> {new PlatformType {Type = ValidString } } },
				new Game {PlatformTypes = new List<PlatformType> {new PlatformType {Type = ValidString } } },
				new Game {PlatformTypes = new List<PlatformType> {new PlatformType {Type = ValidString } } }
			};

			_mockOfUow.Setup(m => m.GameRepository.Get()).Returns(_games.AsQueryable);

			var result = _target.GetBy(new List<string> { ValidString }).ToList().Count;

			Assert.Equals(result, 3);
		}
	}
}