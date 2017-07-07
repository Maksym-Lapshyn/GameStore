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
		private readonly Mock<IGameService> _mockOfGameService;
		private readonly CommentController _target;

		public CommentControllerTests()
		{
			WebAutoMapperConfig.RegisterMappings();
			_mockOfCommentService = new Mock<ICommentService>();
			_mockOfGameService = new Mock<IGameService>();
			_mockOfCommentService.Setup(m => m.GetBy("COD123")).Returns(new List<CommentDto>
			{
				new CommentDto {Name = "Elma", Body = "Wow, it is amazing"},
				new CommentDto {Name = "Supra", Body = "This game is so so"}
			});

			_target = new CommentController(_mockOfCommentService.Object, _mockOfGameService.Object);
		}

		[TestMethod]
		public void New_SendsCommentViewModelToView_WhenModelStateIsInvalid()
		{
			_target.ModelState.AddModelError("test", "test");
			var result = ((PartialViewResult)_target.New(new CommentViewModel())).Model;

			Assert.IsInstanceOfType(result, typeof(CommentViewModel));
		}

		[TestMethod]
		public void New_ReturnsRedirect_WhenModelStateIsValid()
		{
			var result = _target.New(new CommentViewModel());

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
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
			var count = ((AllCommentsViewModel) result.Model).Comments.Count;

			Assert.IsTrue(count == 2);
		}
	}
}
