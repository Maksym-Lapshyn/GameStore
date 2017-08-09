using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.Controllers;
using GameStore.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.Web.Tests
{
	[TestClass]
	public class CommentsControllerTests
	{
		private const string ValidString = "test";
		private const string InvalidString = "testtest";
		private readonly IMapper _mapper = new Mapper(
			new MapperConfiguration(cfg => cfg.AddProfile(new WebProfile())));
		private Mock<ICommentService> _mockOfCommentService;
		private CommentsController _target;
		private List<CommentDto> _comments;

		[TestInitialize]
		public void Initialize()
		{
			Mapper.Initialize(cfg => cfg.CreateMap<IEnumerable<CommentDto>, List<CommentViewModel>>());
			_mockOfCommentService = new Mock<ICommentService>();
			_target = new CommentsController(_mockOfCommentService.Object, _mapper);
		}

		[TestMethod]
		public void NewComment_SendsAllCommentsViewModelToView()
		{
			_target.ModelState.AddModelError(InvalidString, InvalidString);

			var result = ((ViewResult)_target.NewComment(ValidString)).Model;

			Assert.IsInstanceOfType(result, typeof(AllCommentsViewModel));
		}

		[TestMethod]
		public void NewComment_ReturnsViewResult_WhenModelStateIsInvalid()
		{
			_target.ModelState.AddModelError(InvalidString, InvalidString);

			var result = _target.NewComment(new AllCommentsViewModel());

			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public void NewComment_CreatesComment_WhenModelStateIsValid()
		{
			_comments = new List<CommentDto>();
			var newComment = new CommentViewModel { GameKey = ValidString };
			var model = new AllCommentsViewModel { NewComment = newComment };
			_mockOfCommentService.Setup(m => m.Create(It.IsAny<CommentDto>())).Callback<CommentDto>(c => _comments.Add(c));
			_mockOfCommentService.Setup(m => m.GetBy(ValidString)).Returns(_comments.Where(c => c.GameKey == ValidString));

			_target.NewComment(model);

			Assert.AreEqual(1, _comments.Count);
		}

		[TestMethod]
		public void NewComment_ReturnsViewResult_WhenModelStateIsValid()
		{
			var result = _target.NewComment(new AllCommentsViewModel());

			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public void NewComemnt_ReturnsModelWithAllComments_WhenValidGameKeyIsPassed()
		{
			_comments = new List<CommentDto>
			{
				new CommentDto
				{
					GameKey = ValidString
				},

				new CommentDto
				{
					GameKey = ValidString
				},

				new CommentDto
				{
					GameKey = ValidString
				}
			};

			_mockOfCommentService.Setup(m => m.GetBy(ValidString)).Returns(_comments.Where(c => c.GameKey == ValidString));

			var model = ((ViewResult)_target.NewComment(ValidString)).Model;
			var result = ((AllCommentsViewModel)model).Comments.Count;

			Assert.AreEqual(result, 3);
		}
	}
}
