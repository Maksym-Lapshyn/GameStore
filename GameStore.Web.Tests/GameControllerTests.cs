using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.Controllers;
using GameStore.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;

namespace GameStore.Web.Tests
{
	[TestClass]
	public class GameControllerTests
	{
		private Mock<IGameService> _mockOfGameService;
		private Mock<IGenreService> _mockOfGenreService;
		private Mock<IPlatformTypeService> _mockOfPlatformTypeService;
		private Mock<IPublisherService> _mockOfPublisherService;
		private readonly IMapper _mapper = new Mapper(
			new MapperConfiguration(cfg => cfg.AddProfile(new WebProfile())));
		private const string ValidGameKey = "test";
		private const int ValidGameId = 10;
		private List<GameDto> _games;
		private GameController _target;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfGameService = new Mock<IGameService>();
			_mockOfPlatformTypeService = new Mock<IPlatformTypeService>();
			_mockOfGenreService = new Mock<IGenreService>();
			_mockOfPublisherService = new Mock<IPublisherService>();
			_target = new GameController(_mockOfGameService.Object, _mockOfGenreService.Object, _mockOfPlatformTypeService.Object, _mockOfPublisherService.Object, _mapper);
			_games = new List<GameDto>();
			_mockOfGameService.Setup(m => m.Create(It.IsAny<GameDto>())).Callback<GameDto>(g => _games.Add(g));
			_mockOfGameService.Setup(m => m.GetSingleBy(ValidGameKey)).Returns(new GameDto());
			_mockOfGameService.Setup(m => m.Delete(ValidGameId)).Callback<int>(i => _games.RemoveAll(g => g.Id == i));
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

			Assert.IsTrue(_games.Count == 1);
		}

		[TestMethod]
		public void New_DoesNotCraeteGame_WhenModelStateIsInvalid()
		{
			_target.ModelState.AddModelError("test", "test");

			_target.New(new GameViewModel());

			Assert.IsTrue(_games.Count == 0);
		}

		[TestMethod]
		public void New_ReturnsRedirectToRouteResult_WhenModelStateIsValid()
		{
			var result = _target.New(new GameViewModel());

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
		}

		[TestMethod]
		public void Update_CallsEditOnce_WhenAnyGameIsPassed()
		{
			_target.Update(new GameDto());

			_mockOfGameService.Verify(m => m.Edit(It.IsAny<GameDto>()), Times.Once);
		}

		[TestMethod]
		public void Show_ReturnsHttpStatusCodeResult()
		{
			var result = _target.Update(new GameDto());

			Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
		}

		[TestMethod]
		public void Show_SendsGameToView_WhenValidGameKeyIsPassed()
		{
			var result = ((ViewResult)_target.Show(ValidGameKey)).Model;

			Assert.IsInstanceOfType(result, typeof(GameViewModel));
		}

		[TestMethod]
		public void ListAll_SendsListOfGamesToView()
		{
			var model = new AllGamesViewModel();

			var result = ((ViewResult)_target.ListAll(model)).Model;

			Assert.IsInstanceOfType(result, typeof(AllGamesViewModel));
		}

		[TestMethod]
		public void Delete_DeletesGame_WhenValidGameIdIsPassed()
		{
			_games = new List<GameDto>
			{
				new GameDto {Id = ValidGameId }
			};

			_target.Delete(ValidGameId);

			Assert.IsTrue(_games.Count == 0);
		}

		[TestMethod]
		public void ShowCount_SendsNumberOfGamesToView()
		{
			_games = new List<GameDto>
			{
				new GameDto(),
				new GameDto()
			};

			_mockOfGameService.Setup(m => m.GetAll(null, null, null)).Returns(_games);

			var result = (int)((PartialViewResult)_target.ShowCount()).Model;

			Assert.IsTrue(result == 2);
		}
	}
}
