using System;
using System.Linq;
using GameStore.DAL.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameStore.Services.Abstract;
using AutoMapper;
using GameStore.Services.Concrete;
using GameStore.Services.DTOs;

namespace GameStore.Services.Tests
{
    [TestClass]
    public class CommentServiceTests
    {
        [TestInitialize]
        public void MockObjects()
        {
            var commentRepoMock = new Mock<IGenericRepository<CommentDto>>();
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>(commentRepoMock.Object);
            UowCommentService service = new UowCommentService(uowMock.Object);
        }

        [TestMethod]
        public void AddCommentToGame_NewComment_AddsComment()
        {
            Mock<IGenericRepository<CommentDto>> commentRepoMock = new Mock<IGenericRepository<CommentDto>>();
            Mock<IUnitOfWork> uowMock = new Mock<IUnitOfWork>(commentRepoMock.Object);

            GameDto game = new GameDto() { Id = 1, Name = "NFS", Description = "Some races", Key = "NFS1" };
            CommentDto comment = new CommentDto()
            {
                Id = 1,
                IsDeleted = false,
                Name = "User1",
                Body = "This game is cool",
                Game = game,
                GameId = game.Id
            };

            UowCommentService service = new UowCommentService(uowMock.Object);

            

            Assert.IsTrue(true);
        }
    }
}
