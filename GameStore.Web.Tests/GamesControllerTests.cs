using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Controllers;
using GameStore.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.Web.Tests
{
	[TestClass]
	public class GamesControllerTests
	{
		private const string ValidString = "test";
		private const string InvalidString = "testtest";

		private readonly IMapper _mapper = new Mapper(
			new MapperConfiguration(cfg => cfg.AddProfile(new WebProfile())));

		private List<GameDto> _games;
		private GamesController _target;
		private Mock<IGameService> _mockOfGameService;
		private Mock<IGenreService> _mockOfGenreService;
		private Mock<IPlatformTypeService> _mockOfPlatformTypeService;
		private Mock<IPublisherService> _mockOfPublisherService;
		private Mock<IAuthentication> _mockOfAuthentication;

		[TestInitialize]
		public void Initialize()
		{
			Mapper.Initialize(cfg => cfg.CreateMap<IEnumerable<GenreDto>, List<GenreViewModel>>());
			Mapper.Initialize(cfg => cfg.CreateMap<IEnumerable<PlatformTypeDto>, List<PlatformTypeViewModel>>());
			Mapper.Initialize(cfg => cfg.CreateMap<IEnumerable<PublisherDto>, List<PublisherViewModel>>());
			Mapper.Initialize(cfg => cfg.CreateMap<IEnumerable<GameDto>, List<GameViewModel>>());
			Mapper.Initialize(cfg => cfg.CreateMap<IEnumerable<CommentDto>, List<CommentViewModel>>());
			_mockOfGameService = new Mock<IGameService>();
			_mockOfPlatformTypeService = new Mock<IPlatformTypeService>();
			_mockOfGenreService = new Mock<IGenreService>();
			_mockOfPublisherService = new Mock<IPublisherService>();
			_mockOfAuthentication = new Mock<IAuthentication>();
			_target = new GamesController(_mockOfGameService.Object, _mockOfGenreService.Object, _mockOfPlatformTypeService.Object, _mockOfPublisherService.Object, _mapper, _mockOfAuthentication.Object);
			_games = new List<GameDto>();
			_mockOfGameService.Setup(m => m.Create(It.IsAny<string>(), It.IsAny<GameDto>())).Callback<string, GameDto>((l, g) => _games.Add(g));
			_mockOfGameService.Setup(m => m.GetSingle(It.IsAny<string>(), ValidString)).Returns(new GameDto());
			_mockOfGameService.Setup(m => m.Delete(ValidString)).Callback<string>(k => _games.RemoveAll(g => g.Key == k));
		}

		[TestMethod]
		public void New_SendsGameToView()
		{
			var result = ((ViewResult)_target.New()).Model;

			Assert.IsInstanceOfType(result, typeof(GameViewModel));
		}

		[TestMethod]
		public void New_CreatesGame_WhenModelStateIsValid()
		{
			_target.New(new GameViewModel());
			var result = _games.Count;

			Assert.AreEqual(result, 1);
		}

		[TestMethod]
		public void New_DoesNotCraeteGame_WhenModelStateIsInvalid()
		{
			_target.ModelState.AddModelError(InvalidString, InvalidString);

			_target.New(new GameViewModel());
			var result = _games.Count;

			Assert.AreEqual(result, 0);
		}

		[TestMethod]
		public void New_ReturnsRedirectToRouteResult_WhenModelStateIsValid()
		{
			var result = _target.New(new GameViewModel());

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
		}

		[TestMethod]
		public void Update_SendsGameToView_WhenValidGameKeyIsPassed()
		{
			var result = _target.Update(ValidString);

			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public void Update_ReturnsViewResult_WhenModelStateIsInValid()
		{
			_target.ModelState.AddModelError(InvalidString, InvalidString);

			var result = _target.Update(new GameViewModel());

			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public void Update_UpdatesGame_WhenModelStateIsValid()
		{
			var game = new GameViewModel { Key = ValidString };
			_games = new List<GameDto> { new GameDto { Key = InvalidString } };
			_mockOfGameService.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<GameDto>())).Callback<string, GameDto>((l, g) => _games[0] = g);

			_target.Update(game);

			Assert.AreEqual(ValidString, _games[0].Key);
		}

		[TestMethod]
		public void Update_ReturnsRedirectToRouteResult_WhenModelStateIsValid()
		{
			var result = _target.Update(new GameViewModel());

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
		}

		[TestMethod]
		public void Show_SendsGameToView_WhenValidGameKeyIsPassed()
		{
			var result = ((ViewResult)_target.Show(ValidString)).Model;

			Assert.IsInstanceOfType(result, typeof(GameViewModel));
		}

		[TestMethod]
		public void Delete_DeletesGame_WhenValidGameIdIsPassed()
		{
			_games = new List<GameDto>
			{
				new GameDto {Key = ValidString }
			};

			_target.Delete(ValidString);
			var result = _games.Count;

			Assert.AreEqual(result, 0);
		}
	}
}