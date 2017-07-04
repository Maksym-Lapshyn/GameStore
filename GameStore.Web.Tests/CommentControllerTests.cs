using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.App_Start;
using GameStore.Web.Controllers;
using GameStore.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;

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
			_mockOfCommentService.Setup(m => m.GetSingleBy("COD123")).Returns(new List<CommentDto>
			{
				new CommentDto {Name = "Elma", Body = "Wow, it is amazing"},
				new CommentDto {Name = "Supra", Body = "This game is so so"}
			});

			_target = new CommentController(_mockOfCommentService.Object);
		}

		[TestMethod]
		public void New_CallsAddOnce_WhenNewCommentPassed()
		{
			_target.New(new CommentViewModel());

			_mockOfCommentService.Verify(m => m.Create(It.IsAny<CommentDto>()), Times.Once);
		}

		[TestMethod]
		public void ListAll_ReturnsViewResult_WhenValidGameKeyPassed()
		{
			var result = _target.ListAll("COD123");

			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public void ListAll_ReturnsAllComments_WhenValidGameKeyPassed()
		{
			var result = _target.ListAll("COD123");
			var count = ((List<CommentViewModel>) result.Model).Count;

			Assert.IsTrue(count == 2);
		}
	}
}
