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
		private const string ValidString = "test";
		private const string InvalidString = "testtest";
		private const int ValidInt = 10;
		private List<GameDto> _games;
		private GamesController _target;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfGameService = new Mock<IGameService>();
			_mockOfPlatformTypeService = new Mock<IPlatformTypeService>();
			_mockOfGenreService = new Mock<IGenreService>();
			_mockOfPublisherService = new Mock<IPublisherService>();
			_target = new GamesController(_mockOfGameService.Object, _mockOfGenreService.Object, _mockOfPlatformTypeService.Object, _mockOfPublisherService.Object, _mapper);
			_games = new List<GameDto>();
			_mockOfGameService.Setup(m => m.Create(It.IsAny<GameDto>())).Callback<GameDto>(g => _games.Add(g));
			_mockOfGameService.Setup(m => m.GetSingleBy(ValidString)).Returns(new GameDto());
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
            _mockOfGameService.Setup(m => m.Update(_mapper.Map<GameViewModel, GameDto>(game))).Callback<GameDto>(g => _games[0] = g);

            _target.Update(game);

            Assert.AreEqual(_games[0].Key, ValidString);
		}

        [TestMethod]
        public void Update_ReturnsRedirectResult_WhenModelStateIsValid()
        {
            var result = _target.Update(new GameViewModel());

            Assert.IsInstanceOfType(result, typeof(RedirectResult));
        }

		[TestMethod]
		public void Show_SendsGameToView_WhenValidGameKeyIsPassed()
		{
			var result = ((ViewResult)_target.Show(ValidString)).Model;

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
		public void Remove_DeletesGame_WhenValidGameIdIsPassed()
		{
			_games = new List<GameDto>
			{
				new GameDto {Key = ValidString }
			};

			_target.Remove(ValidString);
			var result = _games.Count;

			Assert.AreEqual(result, 0);
		}

		[TestMethod]
		public void ShowCount_SendsNumberOfGamesToView()
		{
			_games = new List<GameDto>
			{
				new GameDto(),
				new GameDto()
			};

			_mockOfGameService.Setup(m => m.GetCount(null)).Returns(_games.Count);

			var result = (int)((PartialViewResult)_target.ShowCount()).Model;

			Assert.AreEqual(result, 2);
		}
	}
}