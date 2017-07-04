using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
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
    public class CommentServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockOfUow;
        private readonly CommentService _target;

        public CommentServiceTests()
        {
            ServicesAutoMapperConfig.RegisterMappings();
            _mockOfUow = new Mock<IUnitOfWork>();
            _target = new CommentService(_mockOfUow.Object);

            _mockOfUow.Setup(m => m.CommentRepository.Get(null, null)).Returns(
                new List<Comment>
                {
                    new Comment() {Id = 1, GameId = 1, Name = "Jake", Body = "Hey, this game is good"},
                    new Comment() {Id = 2, GameId = 1, Name = "Susan", Body = "Wow, it is amazing"},
                    new Comment() {Id = 3, GameId = 2, Name = "Emmy", Body = "Fuuuu, this game is crappy"}
                });

            _mockOfUow.Setup(m => m.GameRepository.Get(null, null)).Returns(
                new List<Game>
                {
                    new Game {Id = 1, Name = "Quake", Key = "Quakeiii", Comments = new List<Comment>()
                    {
                        new Comment(){Id = 5, Name = "Emily", Body = "Check it out"}
                    }},
                    new Game() {Id = 2, Name = "Doom", Key = "Doombfg"},
                    new Game() {Id = 3, Name = "Diablo", Key = "Diabloiii"}
                });
        }

        [TestMethod]
        public void GetAll_ReturnsAllComments()
        {
            var result = _target.GetAll().ToList();

            Assert.IsTrue(result.Count == 3);
        }

        [TestMethod]
        public void Add_CallsInsertOnce_WhenValidCommentPassed()
        {
            _target.Create(new CommentDto
            {
                Id = 4,
                GameId = 1,
                Name = "AngryUser",
                Body = "This game is awesomeeeeee"
            });

            _mockOfUow.Verify(m => m.CommentRepository.Insert(It.IsAny<Comment>()), Times.Once);
        }

        [TestMethod]
        public void GetBy_ReturnsAllComments_WhenValidGameKeyPassed()
        {
            var comments = _target.GetSingleBy("Quakeiii").ToList();

            Assert.IsTrue(comments.Count == 1);
        }

        [TestMethod]
        public void GetBy_ReturnsNoComments_WhenKeyOfGameWithoutCommentsPassed()
        {
            var comments = _target.GetSingleBy("Doombfg").ToList();

            Assert.IsTrue(comments.Count == 0);
        }
    }
}
