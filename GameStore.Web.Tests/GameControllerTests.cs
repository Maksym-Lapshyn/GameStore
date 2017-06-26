using System;
using System.Web.Mvc;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Web.Tests
{
    [TestClass]
    public class GameControllerTests
    {
        private Mock<IGameService> _mockOfGameService;
        private GameController _target;

        public GameControllerTests()
        {
            _mockOfGameService = new Mock<IGameService>();
            _target = new GameController(_mockOfGameService.Object);
        }

        [TestMethod]
        public void NewGame_GameDto_AddsNewGame()
        {
            ActionResult result = _target.NewGame(new GameDto() {Name = "COD"});
            _mockOfGameService.Verify(m => m.Create(It.IsAny<GameDto>()), Times.Once);
        }

        [TestMethod]
        public void UpdateGame_GameDto_UpdatesGame()
        {
            ActionResult result = _target.UpdateGame(new GameDto() { Name = "COD" });
            _mockOfGameService.Verify(m => m.Edit(It.IsAny<GameDto>()), Times.Once);
        }

        [TestMethod]
        public void ShowGame_GameKey_ReturnsJson()
        {
            ActionResult result = _target.ShowGame("somegame");
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void ListAllGames_Nothing_ReturnsJson()
        {
            ActionResult result = _target.ListAllGames();
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
    }
}
