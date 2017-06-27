using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.Controllers;

namespace GameStore.Web.Tests
{
    [TestClass]
    public class CommentControllerTests
    {
		//TODO: Consider: make fields readonly
		private Mock<ICommentService> _mockOfCommentService;
        private CommentController _target;

        public CommentControllerTests()
        {
            _mockOfCommentService = new Mock<ICommentService>();
			_mockOfCommentService.Setup(m => m.GetBy("COD123")).Returns(new List<CommentDto>
            {
                new CommentDto {Name = "Elma", Body = "Wow, it is amazing"},
                new CommentDto {Name = "Supra", Body = "This game is so so"}
            });
            _target = new CommentController(_mockOfCommentService.Object);
        }

        [TestMethod]
        public void NewComment_CommentWithParentCommentId_AddsCommentToComment()
        {
            ActionResult result = _target.NewComment(new CommentDto());
			_mockOfCommentService.Verify(m => m.Add(It.IsAny<CommentDto>()), Times.Once);
        }

        [TestMethod]
        public void NewComment_CommentWithoutParentCommentId_AddsCommentToGame()
        {
            ActionResult result = _target.NewComment(new CommentDto());
			_mockOfCommentService.Verify(m => m.Add(It.IsAny<CommentDto>()), Times.Once);
        }

        [TestMethod]
        public void ListAllComments_ValidGameKey_ReturnsJson()
        {
            ActionResult result = _target.ListAllComments("COD123");
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
    }
}
