using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.App_Start;
using GameStore.Web.Controllers;
using GameStore.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.Web.Tests
{
	[TestClass]
	public class GameControllerTests
	{
		private Mock<IGameService> _mockOfGameService;
		private Mock<IGenreService> _mockOfGenreService;
		private Mock<IPlatformTypeService> _mockOfPlatformTypeService;
		private Mock<IPublisherService> _mockOfPublisherService;
		private const string ValidGameKey = "test";
		private List<GameDto> _games;
		private GameController _target;

		[TestInitialize]
		public void Initialize()
		{
			WebAutoMapperConfig.RegisterMappings();
			_mockOfGameService = new Mock<IGameService>();
			_mockOfPlatformTypeService = new Mock<IPlatformTypeService>();
			_mockOfGenreService = new Mock<IGenreService>();
			_mockOfPublisherService = new Mock<IPublisherService>();
			_target = new GameController(_mockOfGameService.Object, _mockOfGenreService.Object, _mockOfPlatformTypeService.Object, _mockOfPublisherService.Object);
			_games = new List<GameDto>();
			_mockOfGameService.Setup(m => m.Create(It.IsAny<GameDto>())).Callback<GameDto>(g => _games.Add(g));
			_mockOfGameService.Setup(m => m.GetSingleBy(ValidGameKey)).Returns(new GameDto());
			_mockOfGameService.Setup(m => m.GetAll()).Returns(_games);
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
		public void Update_CallsEditOnce_WhenAnyGamePassed()
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
		public void Show_SendsGameToView_WhenValidGameKeyPassed()
		{
			var result = ((ViewResult)_target.Show(ValidGameKey)).Model;

			Assert.IsInstanceOfType(result, typeof(GameViewModel));
		}

		[TestMethod]
		public void ListAll_ReturnsJson()
		{
			_games = new List<GameDto>
			{
				new GameDto{Name = "firstGame"},
				new GameDto{Name = "secondGame"}
			};

			var json = ((JsonResult)_target.ListAll()).Data;
			var result = JsonConvert.DeserializeObject<List<OrderViewModel>>(json.ToString());

			Assert.IsTrue(result.Count == 2);
		}

		[TestMethod]
		public void New_ReturnsRedirectToRouteResult_WhenValidGamePassed()
		{
			var result = _target.New(new GameViewModel());

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
		}

		[TestMethod]
		public void New_ReturnsViewResult_WhenModelStateIsInvalid()
		{
			_target.ModelState.AddModelError("testError", "testError");
			var result = _target.New(new GameViewModel());

			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public void Dowload_ReturnsFileResult_WhenValidGameKeyIsPassed()
		{
			var result = _target.Download(ValidGameKey);

			Assert.IsInstanceOfType(result, typeof(FileResult));
		}
	}
}
