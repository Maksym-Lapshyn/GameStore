using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameStore.Services.Abstract;
using AutoMapper;
using GameStore.DAL.Entities;
using GameStore.Services.Concrete;
using GameStore.Services.DTOs;
using GameStore.Services.Infrastructure;

namespace GameStore.Services.Tests
{
    [TestClass]
    public class CommentServiceTests
    {
        private Mock<IUnitOfWork> _mockOfUow;
        private UowCommentService _target;

        public CommentServiceTests()
        {
            AutoMapperConfig.RegisterMappings();
            _mockOfUow = new Mock<IUnitOfWork>();
            _target = new UowCommentService(_mockOfUow.Object);

            _mockOfUow.Setup(m => m.CommentRepository.Get(null, null)).Returns(
                new List<Comment>()
                {
                    new Comment() {Id = 1, GameId = 1, Name = "Jake", Body = "Hey, this game is good"},
                    new Comment() {Id = 2, GameId = 1, Name = "Susan", Body = "Wow, it is amazing"},
                    new Comment() {Id = 3, GameId = 2, Name = "Emmy", Body = "Fuuuu, this game is crappy"}
                });

            _mockOfUow.Setup(m => m.GameRepository.Get(null, null)).Returns(
                new List<Game>()
                {
                    new Game() {Id = 1, Name = "Quake", Key = "Quakeiii", Comments = new List<Comment>()
                    {
                        new Comment(){Id = 5, Name = "Emily", Body = "Check it out"}
                    }},
                    new Game() {Id = 2, Name = "Doom", Key = "Doombfg"},
                    new Game() {Id = 3, Name = "Diablo", Key = "Diabloiii"}
                });
        }

        [TestMethod]
        public void GetAll_Nothing_ReturnsAllComments()
        {
            List<CommentDto> result = _target.GetAll().ToList();
            Assert.IsTrue(result.Count == 3);
        }

        [TestMethod]
        public void AddCommentToGame_CommentDto_AddsCommentToGame()
        {
            _target.AddCommentToGame(new CommentDto()
            {
                Id = 4,
                GameId = 1,
                Name = "AngryUser",
                Body = "This game is awesomeeeeee"
            });
            _mockOfUow.Verify(m => m.CommentRepository.Insert(It.IsAny<Comment>()), Times.Once);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AddCommentToGame_CommentDtoWithNonExistingGame_ThrowsArgumentException()
        {
            _target.AddCommentToGame(new CommentDto()
            {
                Id = 4,
                GameId = 4125,
                Name = "AngryUser",
                Body = "This game is awesomeeeeee"
            });
        }

        [TestMethod]
        public void AddCommentToComment_CommentDto_AddsCommentToComment()
        {
            _target.AddCommentToComment(new CommentDto()
            {
                Id = 4,
                ParentCommentId = 1,
                Name = "AngryUser",
                Body = "Agree"
            });
            _mockOfUow.Verify(m => m.CommentRepository.Insert(It.IsAny<Comment>()), Times.Once);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AddCommentToComment_CommentDtoWithNonExistingParentComment_ThrowsArgumentException()
        {
            _target.AddCommentToComment(new CommentDto()
            {
                Id = 4,
                ParentCommentId = 213,
                Name = "AngryUser",
                Body = "Agree"
            });
        }

        [TestMethod]
        public void GetAllCommentsByGameKey_KeyOfExistingGameWithComments_ReturnsAllComments()
        {
            List<CommentDto> comments = _target.GetAllCommentsByGameKey("Quakeiii").ToList();
            Assert.IsTrue(comments.Count == 1);
        }

        [TestMethod]
        public void GetAllCommentsByGameKey_KeyOfExistingGameWithoutComments_ReturnsNoComments()
        {
            List<CommentDto> comments = _target.GetAllCommentsByGameKey("Doombfg").ToList();
            Assert.IsTrue(comments.Count == 0);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void GetAllCommentsByGameKey_KeyOfNonExistingGame_ThrowsArgumentException()
        {
            List<CommentDto> comments = _target.GetAllCommentsByGameKey("gdashdash").ToList();
        }
    }
}
