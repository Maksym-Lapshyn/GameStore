using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Concrete;
using GameStore.Services.DTOs;
using GameStore.Services.Infrastructure;
using Moq;

namespace GameStore.Services.Tests
{
    [TestClass]
    public class GameServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockOfUow;
        private readonly GameService _target;

        public GameServiceTests()
        {
            ServiceAutoMapperConfig.RegisterMappings();
            _mockOfUow = new Mock<IUnitOfWork>();
            _mockOfUow.Setup(m => m.GameRepository.Get(null, null)).Returns(
            new List<Game>
                {
                    new Game {Id = 1, Name = "Quake", Key = "Quakeiii",
                        Genres = new List<Genre>{new Genre{Name = "Action"}}},
                    new Game {Id = 2, Name = "Doom", Key = "Doombfg",
                        Genres = new List<Genre>{new Genre{Name = "Action"}}},
                    new Game {Id = 3, Name = "Diablo", Key = "Diabloiii",
                        Genres = new List<Genre>{new Genre{Name = "RPG"}}}
                });
            _target = new GameService(_mockOfUow.Object);
        }

        [TestMethod]
        public void Create_CallsInsertOnce_WhenValidGamePassed()
        {
            _target.Create(new GameDto
            {
                Id = 4,
                Key = "somekey",
                Name = "LOL"
            });
            _mockOfUow.Verify(m => m.GameRepository.Insert(It.IsAny<Game>()), Times.Once);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Create_ThrowsArgumentNullException_GameWithAlreadyTakenKeyPassed()
        {
            _target.Create(new GameDto
            {
                Id = 4,
                Key = "Quakeiii",
                Name = "LOL"
            });
        }

        [TestMethod]
        public void Edit_CallsUpdatedOnce_WhenValidGamePassed()
        {
            _target.Edit(new GameDto
            {
                Id = 1,
                Key = "COD",
                Name = "COD123"
            });
            _mockOfUow.Verify(m => m.GameRepository.Update(It.IsAny<Game>()), Times.Once);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Edit_ThrowsArgumentNullException_WhenGameWithInvalidIdPassed()
        {
            _target.Edit(new GameDto
            {
                Id = 1516
            });
        }

        [TestMethod]
        public void Delete_CallsDeleteOnce_WhenValidIdPassed()
        {
            _target.Delete(1);
            _mockOfUow.Verify(m => m.GameRepository.Delete(1), Times.Once);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Delete_ThrowsArgumentNullException_InvalidIdPassed()
        {
            _target.Delete(51561);
        }

        [TestMethod]
        public void Get_ReturnsGame_WhenValidIdPassed()
        {
            GameDto game = _target.Get(1);
            Assert.IsNotNull(game);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Get_ThrowsArgumentNullException_WhenInvalidIdPassed()
        {
            GameDto game = _target.Get(default(Int32));
        }

        [TestMethod]
        public void Get_KeyOfExistingGame_ReturnsGame()
        {
            GameDto game = _target.GetSingleBy("Quakeiii");
            Assert.IsNotNull(game);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void Get_KeyOfNonExistingGame_ThrowsArgumentException()
        {
            GameDto game = _target.GetSingleBy(string.Empty);
        }

        [TestMethod]
        public void GetAll_ThreeGames_ReturnsAllGames()
        {
            List<GameDto> games = _target.GetAll().ToList();
            Assert.IsTrue(games.Count == 3);
        }

        [TestMethod]
        public void GetGamesByGenre_ExistingGenre_ReturnsMatches()
        {
            List<GameDto> games = _target.GetBy("Action").ToList();
            Assert.IsTrue(games.Count == 2);
        }

        [TestMethod]
        public void GetGamesByGenre_NonExistingGenre_ReturnsNoMatches()
        {
            List<GameDto> games = _target.GetBy("gdasgdas").ToList();
            Assert.IsTrue(games.Count == 0);
        }
    }
}
