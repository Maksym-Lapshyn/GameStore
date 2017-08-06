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
        private CommentsController _target;
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
            var comment = new CommentViewModel { Name = ValidString, Body = ValidString };
            var model = new AllCommentsViewModel { Comments = new List<CommentViewModel> { comment } };

            _mockOfCommentService.Setup(m => m.Create(_mapper.Map<CommentViewModel, CommentDto>(comment))).Callback<CommentDto>(c => _comments.Add(c));

            _target.NewComment(model);

            Assert.AreEqual(_comments.First().Name, ValidString);
            Assert.AreEqual(_comments.First().Body, ValidString);
        }

        [TestMethod]
        public void NewComment_ReturnsViewResult_WhenModelStateIsValid()
        {
            var result = _target.NewComment(new AllCommentsViewModel());

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }


        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void NewComment_ThrowsInvalidOperationException_WhenInvalidGameKeyIsPassed()
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

            _target.NewComment(InvalidString);
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
