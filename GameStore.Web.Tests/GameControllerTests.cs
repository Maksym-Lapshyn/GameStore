using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.App_Start;
using GameStore.Web.Controllers;
using GameStore.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;

namespace GameStore.Web.Tests
{
	[TestClass]
	public class GameControllerTests
	{
		private readonly Mock<IGameService> _mockOfGameService;
		private readonly Mock<IGenreService> _mockOfGenreService;
		private readonly Mock<IPlatformTypeService> _mockOfPlatformTypeService;
		private readonly Mock<IPublisherService> _mockOfPublisherService;
		private readonly GameController _target;

		public GameControllerTests()
		{
			WebAutoMapperConfig.RegisterMappings();
			_mockOfGameService = new Mock<IGameService>();
			_mockOfPlatformTypeService = new Mock<IPlatformTypeService>();
			_mockOfGenreService = new Mock<IGenreService>();
			_mockOfPublisherService = new Mock<IPublisherService>();
			_target = new GameController(_mockOfGameService.Object, _mockOfGenreService.Object, _mockOfPlatformTypeService.Object, _mockOfPublisherService.Object);
		}

		[TestMethod]
		public void NewGame_CallsCreateOnce_WhenValidGamePassed()
		{
			_target.NewGame(new GameViewModel());

			_mockOfGameService.Verify(m => m.Create(It.IsAny<GameDto>()), Times.Once);
		}

		[TestMethod]
		public void UpdateGame_CallsEditOnce_WhenValidGamePassed()
		{
			_target.UpdateGame(new GameDto());

			_mockOfGameService.Verify(m => m.Edit(It.IsAny<GameDto>()), Times.Once);
		}

		[TestMethod]
		public void ShowGame_RetrunsViewResult_WhenValidGameKeyPassed()
		{
			var result = _target.ShowGame("somegame");

			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public void ListAllGames_ReturnsJson()
		{
			var result = _target.ListAllGames();

			Assert.IsInstanceOfType(result, typeof(JsonResult));
		}

		[TestMethod]
		public void NewGame_ReturnsViewResult()
		{
			var result = _target.NewGame();

			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public void NewGame_ReturnsRedirectResult_WhenValidGamePassed()
		{
			var result = _target.NewGame(new GameViewModel());

			Assert.IsInstanceOfType(result, typeof(RedirectResult));
		}

		[TestMethod]
		public void NewGame_ReturnsViewResult_WhenModelStateIsInvalid()
		{
			_target.ModelState.AddModelError("testError", "testError");
			var result = _target.NewGame(new GameViewModel());

			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}
	}
}
