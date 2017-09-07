using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.Localization;
using GameStore.Services.Abstract;
using GameStore.Services.Concrete;
using GameStore.Services.Dtos;
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

		private Mock<IUnitOfWork> _mockOfUow;
		private Mock<IGameRepository> _mockOfGameRepository;
		private Mock<IPublisherRepository> _mockOfPublisherRepository;
		private Mock<IGenreRepository> _mockOfGenreRepository;
		private Mock<IPlatformTypeRepository> _mockOfPlatformTypeRepository;
		private Mock<IOutputLocalizer<Game>> _mockOfOutputLocalizer;
		private Mock<IInputLocalizer<Game>> _mockOfInputLocalizer;
		private Mock<IGameLocaleRepository> _mockOfGameLocaleRepository;
		private Mock<ICommentRepository> _mockOfCommentRepository;
		private GameService _target;
		private List<Game> _games;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfGameRepository = new Mock<IGameRepository>();
			_mockOfPublisherRepository = new Mock<IPublisherRepository>();
			_mockOfGenreRepository = new Mock<IGenreRepository>();
			_mockOfPlatformTypeRepository = new Mock<IPlatformTypeRepository>();
			_mockOfUow = new Mock<IUnitOfWork>();
			_mockOfOutputLocalizer = new Mock<IOutputLocalizer<Game>>();
			_mockOfInputLocalizer = new Mock<IInputLocalizer<Game>>();
			_mockOfGameLocaleRepository = new Mock<IGameLocaleRepository>();
			_mockOfCommentRepository = new Mock<ICommentRepository>();
			_target = new GameService(_mockOfUow.Object, _mapper, _mockOfInputLocalizer.Object, _mockOfOutputLocalizer.Object,
				_mockOfGameLocaleRepository.Object, _mockOfGameRepository.Object, _mockOfPublisherRepository.Object,
				_mockOfGenreRepository.Object, _mockOfPlatformTypeRepository.Object, _mockOfCommentRepository.Object);
			_mockOfPublisherRepository.Setup(m => m.GetSingle(p => p.CompanyName == ValidString)).Returns(new Publisher());
			_mockOfPlatformTypeRepository.Setup(m => m.GetSingle(p => p.Type == ValidString)).Returns(new PlatformType());
			_mockOfGenreRepository.Setup(m => m.GetSingle(g => g.Name == ValidString)).Returns(new Genre());
			
		}

		[TestMethod]
		public void Create_CreatesGame_WhenAnyGameIsPassed()
		{
			_games = new List<Game>();
			_mockOfGameRepository.Setup(m => m.Insert(It.IsAny<Game>())).Callback<Game>(g => _games.Add(g));

			_target.Create(ValidString, new GameDto());

			Assert.AreEqual(1, _games.Count);
		}

		[TestMethod]
		public void Create_CallsSaveOnce_WhenAnyGameIsPassed()
		{
			_games = new List<Game>();

			_target.Create(ValidString, new GameDto());

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public void Update_UpdatesGame_WhenValidGamePassed()
		{
			_games = new List<Game>
			{
				new Game { Id = TestInt, Name = ValidString }
			};

			_mockOfGameRepository.Setup(m => m.GetAll(null, null, null, null)).Returns(_games);
			_mockOfGameRepository.Setup(m => m.Update(It.IsAny<Game>())).Callback<Game>(g => _games.First(game => game.Id == g.Id).Name = g.Name);
			_target.Update(ValidString, new GameDto
			{
				Id = TestInt,
				Name = InvalidString
			});

			var result = _games.First().Name;

			Assert.AreEqual(result, InvalidString);
		}

		[TestMethod]
		public void Update_CallsSaveOnce_WhenAnyGameIsPassed()
		{
			_games = new List<Game>
			{
				new Game { Key = ValidString }
			};

			_mockOfGameRepository.Setup(m => m.GetAll(null, null, null, null)).Returns(_games);
			_mockOfGameRepository.Setup(m => m.Update(It.IsAny<Game>())).Callback<Game>(g => _games[0] = g);

			_target.Update(ValidString, new GameDto
			{
				Key = ValidString
			});

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public void Delete_CallsDeleteOnce_WhenValidGameKeyIsPassed()
		{
			_target.Delete(ValidString);

			_mockOfGameRepository.Verify(m => m.Delete(ValidString), Times.Once);
		}
	}
}