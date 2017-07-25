using System;
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
	public class CommentControllerTests
	{
		private Mock<ICommentService> _mockOfCommentService;
		private CommentController _target;
		private List<CommentDto> _comments;
		private readonly IMapper _mapper = new Mapper(
			new MapperConfiguration(cfg => cfg.AddProfile(new WebProfile())));
		private const string ValidString = "test";
		private const string InvalidString = "testtest";

		[TestInitialize]
		public void Initialize()
		{
			Mapper.Initialize(cfg => cfg.CreateMap<IEnumerable<CommentDto>, List<CommentViewModel>>());
			_mockOfCommentService = new Mock<ICommentService>();
			_target = new CommentController(_mockOfCommentService.Object, _mapper);
		}

		[TestMethod]
		public void New_SendsCommentToView_WhenModelStateIsInvalid()
		{
			_target.ModelState.AddModelError(InvalidString, InvalidString);

			var result = ((PartialViewResult)_target.New(new CommentViewModel())).Model;

			Assert.IsInstanceOfType(result, typeof(CommentViewModel));
		}

		[TestMethod]
		public void New_ReturnsRedirectToRouteResult_WhenModelStateIsValid()
		{
			var result = _target.New(new CommentViewModel());

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
		}

		[ExpectedException(typeof(InvalidOperationException))]
		[TestMethod]
		public void ListAll_ThrowsInvalidOperationException_WhenInvalidGameKeyIsPassed()
		{
			_comments = new List<CommentDto>
			{
				new CommentDto
				{
					Game = new GameDto
					{
						Key = ValidString
					}
				},

				new CommentDto
				{
					Game = new GameDto
					{
						Key = ValidString
					}
				},

				new CommentDto
				{
					Game = new GameDto
					{
						Key = ValidString
					}
				}
			};

			_mockOfCommentService.Setup(m => m.GetBy(ValidString)).Returns(_comments.Where(c => c.Game.Key == ValidString));

			_target.ListAll(InvalidString);
		}

		[TestMethod]
		public void ListAll_SendsAllCommentsToView_WhenValidGameKeyIsPassed()
		{
			_comments = new List<CommentDto>
			{
				new CommentDto
				{
					Game = new GameDto
					{
						Key = ValidString
					}
				},

				new CommentDto
				{
					Game = new GameDto
					{
						Key = ValidString
					}
				},

				new CommentDto
				{
					Game = new GameDto
					{
						Key = ValidString
					}
				}
			};

			_mockOfCommentService.Setup(m => m.GetBy(ValidString)).Returns(_comments.Where(c => c.Game.Key == ValidString));

			var model = ((ViewResult)_target.ListAll(ValidString)).Model;
			var result = ((AllCommentsViewModel)model).Comments.Count;

			Assert.IsTrue(result == 3);
		}
	}
}
