using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Controllers;
using GameStore.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
		private Mock<IGameService> _mockOfGameService;
		private Mock<IAuthentication> _mockOfAuthentication;
		private Mock<ControllerContext> _mockOfControllerContext;
		private Mock<IPrincipal> _mockOfPrincipal;
		private CommentsController _target;
		private List<CommentDto> _comments;

		[TestInitialize]
		public void Initialize()
		{
			_comments = new List<CommentDto>();
			Mapper.Initialize(cfg => cfg.CreateMap<IEnumerable<CommentDto>, List<CommentViewModel>>());
			_mockOfCommentService = new Mock<ICommentService>();
			_mockOfGameService = new Mock<IGameService>();
			_mockOfAuthentication = new Mock<IAuthentication>();
			_mockOfControllerContext = new Mock<ControllerContext>();
			_mockOfPrincipal = new Mock<IPrincipal>();
			_mockOfPrincipal.Setup(m => m.Identity.Name).Returns(ValidString);
			_mockOfControllerContext.Setup(m => m.HttpContext.User).Returns(_mockOfPrincipal.Object);
			_mockOfGameService.Setup(m => m.GetSingle(It.IsAny<string>(), ValidString)).Returns(new GameDto {IsDeleted = true});
			_mockOfCommentService.Setup(m => m.Create(It.IsAny<CommentDto>())).Callback<CommentDto>(c => _comments.Add(c));
			_mockOfCommentService.Setup(m => m.GetAll(ValidString)).Returns(_comments.Where(c => c.GameKey == ValidString));
			_target = new CommentsController(_mockOfCommentService.Object, _mockOfGameService.Object, _mapper, _mockOfAuthentication.Object)
			{
				ControllerContext = _mockOfControllerContext.Object
			};
		}

		[TestMethod]
		public void New_SendsAllCommentsViewModelToView()
		{
			var result = ((ViewResult)_target.New(ValidString)).Model;

			Assert.IsInstanceOfType(result, typeof(CompositeCommentsViewModel));
		}

		[TestMethod]
		public void New_ReturnsHttpStatusCodeResult_WhenGameIsDeleted()
		{
			_target.ModelState.AddModelError(InvalidString, InvalidString);

			var result = _target.New(new CompositeCommentsViewModel{GameIsDeleted = true});

			Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
		}

		[TestMethod]
		public void New_ReturnsViewResult_WhenModelStateIsInvalid()
		{
			_target.ModelState.AddModelError(InvalidString, InvalidString);

			var result = _target.New(new CompositeCommentsViewModel
			{
				NewComment = new CommentViewModel
				{
					GameKey = ValidString
				}
			});

			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public void New_CreatesComment_WhenModelStateIsValid()
		{
			_comments = new List<CommentDto>();
			var newComment = new CommentViewModel { GameKey = ValidString };
			var model = new CompositeCommentsViewModel { NewComment = newComment };

			_target.New(model);

			Assert.AreEqual(1, _comments.Count);
		}

		[TestMethod]
		public void New_ReturnsRedirectToRouteResult_WhenModelStateIsValid()
		{
			var result = _target.New(new CompositeCommentsViewModel
			{
				GameIsDeleted = false,

				NewComment = new CommentViewModel
				{
					GameKey = ValidString
				}
			});

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
		}

		[TestMethod]
		public void New_ReturnsModelWithAllComments_WhenValidGameKeyIsPassed()
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

			
			_mockOfCommentService.Setup(m => m.GetAll(ValidString)).Returns(_comments.Where(c => c.GameKey == ValidString));

			var model = ((ViewResult)_target.New(ValidString)).Model;
			var result = ((CompositeCommentsViewModel)model).Comments.Count;

			Assert.AreEqual(result, 3);
		}
	}
}
