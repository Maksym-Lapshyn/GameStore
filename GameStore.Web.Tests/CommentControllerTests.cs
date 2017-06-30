using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Components.DictionaryAdapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.App_Start;
using GameStore.Web.Controllers;
using GameStore.Web.Models;

namespace GameStore.Web.Tests
{
    [TestClass]
    public class CommentControllerTests
    {
        private readonly Mock<ICommentService> _mockOfCommentService;
        private readonly CommentController _target;

        public CommentControllerTests()
        {
            WebAutoMapperConfig.RegisterMappings();
            _mockOfCommentService = new Mock<ICommentService>();
			_mockOfCommentService.Setup(m => m.GetBy("COD123")).Returns(new List<CommentDto>
            {
                new CommentDto {Name = "Elma", Body = "Wow, it is amazing"},
                new CommentDto {Name = "Supra", Body = "This game is so so"}
            });

            _target = new CommentController(_mockOfCommentService.Object);
        }

        [TestMethod]
        public void NewComment_CallsAddOnce_WhenNewCommentPassed()
        {
            var result = _target.NewComment(new CommentViewModel());

			_mockOfCommentService.Verify(m => m.Create(It.IsAny<CommentDto>()), Times.Once);
        }

        [TestMethod]
        public void ListAllComments_ReturnsViewResult_WhenValidGameKeyPassed()
        {
            var result = _target.ListAllComments("COD123");

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ListAllComments_ReturnsAllComments_WhenValidGameKeyPassed()
        {
            var result = _target.ListAllComments("COD123");
            var count = ((List<CommentViewModel>) result.Model).Count;

            Assert.IsTrue(count == 2);
        }
    }
}
