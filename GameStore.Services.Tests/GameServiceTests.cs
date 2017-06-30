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
            ServicesAutoMapperConfig.RegisterMappings();
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

        [TestMethod]
        public void Delete_CallsDeleteOnce_WhenValidIdPassed()
        {
            _target.Delete(1);

            _mockOfUow.Verify(m => m.GameRepository.Delete(1), Times.Once);
        }

        [TestMethod]
        public void Get_ReturnsGame_WhenValidIdPassed()
        {
            var game = _target.Get(1);

            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void ReturnsGame_WhenValidGameKeyPassed()
        {
            var game = _target.GetSingleBy("Quakeiii");

            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void GetAll_ReturnsAllGames()
        {
            var games = _target.GetAll().ToList();

            Assert.IsTrue(games.Count == 3);
        }

        [TestMethod]
        public void GetBy_ReturnsAllGames_WhenValidGenrePassed()
        {
            var games = _target.GetBy("Action").ToList();

            Assert.IsTrue(games.Count == 2);
        }

        [TestMethod]
        public void GetBy_ReturnsNoGames_WhenEmptyStringPassed()
        {
            var games = _target.GetBy(string.Empty).ToList();

            Assert.IsTrue(games.Count == 0);
        }
    }
}