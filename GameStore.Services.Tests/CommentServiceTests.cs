using AutoMapper;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Entities;
using GameStore.Services.Concrete;
using GameStore.Services.DTOs;
using GameStore.Services.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Tests
{
	[TestClass]
	public class CommentServiceTests
	{
		private const int TestInt = 10;
		private const string ValidString = "test";
		private const string InValidString = "testtest";
		private readonly IMapper _mapper = new Mapper(
			new MapperConfiguration(cfg => cfg.AddProfile(new ServiceProfile())));
		private Mock<ICommentRepository> _mockOfCommentRepository;
		private Mock<IGameRepository> _mockOfGameRepository;
		private Mock<IUnitOfWork> _mockOfUow;
		private CommentService _target;
		private List<Comment> _comments;
		private List<Game> _games;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfCommentRepository = new Mock<ICommentRepository>();
			_mockOfGameRepository = new Mock<IGameRepository>();
			_mockOfUow = new Mock<IUnitOfWork>();
			_target = new CommentService(_mockOfUow.Object, _mapper, _mockOfCommentRepository.Object, _mockOfGameRepository.Object);
			_mockOfCommentRepository.Setup(m => m.Insert(It.IsAny<Comment>())).Callback<Comment>(c => _comments.Add(c));
		}

		[TestMethod]
		public void Create_UpdatesParentComment_WhenCommentWithParentCommentIdIsPassed()
		{
			var newComment = new CommentDto { ParentCommentId = TestInt, GameKey = ValidString };
			var parentComment = new Comment { Id = TestInt, ChildComments = new List<Comment>() };
			_comments = new List<Comment> { new Comment() };
			_mockOfCommentRepository.Setup(m => m.GetSingle(It.IsAny<int>())).Returns(parentComment);
			_mockOfCommentRepository.Setup(m => m.Update(It.IsAny<Comment>())).Callback<Comment>(c => _comments[0] = c);

			_target.Create(newComment);

			Assert.AreEqual(ValidString, _comments[0].ChildComments.First().GameKey);
		}

		[TestMethod]
		public void Create_UpdatesGame_WhenCommentWithoutParentCommentIdIsPassed()
		{
			var newComment = new CommentDto { GameKey = ValidString };
			var game = new Game { Key = ValidString };
			_games = new List<Game> { new Game() };
			_mockOfGameRepository.Setup(m => m.GetSingle(ValidString)).Returns(game);
			_mockOfGameRepository.Setup(m => m.Update(It.IsAny<Game>())).Callback<Game>(g => _games[0] = g);

			_target.Create(newComment);

			Assert.AreEqual(ValidString, _games[0].Key);
		}

		[TestMethod]
		public void Create_CallsSaveOnce_WhenCommentWithParentCommentIdIsPassed()
		{
			var newComment = new CommentDto { ParentCommentId = TestInt, GameKey = ValidString };
			var parentComment = new Comment { Id = TestInt, ChildComments = new List<Comment>() };
			_comments = new List<Comment> { new Comment() };
			_mockOfCommentRepository.Setup(m => m.GetSingle(It.IsAny<int>())).Returns(parentComment);
			_mockOfCommentRepository.Setup(m => m.Update(It.IsAny<Comment>())).Callback<Comment>(c => _comments[0] = c);

			_target.Create(newComment);

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public void Create_CallsSaveOnce_WhenCommentWithoutParentCommentIdIsPassed()
		{
			var newComment = new CommentDto { GameKey = ValidString };
			var game = new Game { Key = ValidString };
			_games = new List<Game> { new Game() };
			_mockOfGameRepository.Setup(m => m.GetSingle(ValidString)).Returns(game);
			_mockOfGameRepository.Setup(m => m.Update(It.IsAny<Game>())).Callback<Game>(g => _games[0] = g);

			_target.Create(newComment);

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public void GetAll_ReturnsAllComments()
		{
			_comments = new List<Comment>
			{
				new Comment(),
				new Comment(),
				new Comment()
			};

			_mockOfCommentRepository.Setup(m => m.GetAll()).Returns(_comments);

			var result = _target.GetAll().ToList().Count;

			Assert.AreEqual(result, 3);
		}

		[TestMethod]
		public void GetBy_ReturnsAllComments_WhenValidGameKeyIsPassed()
		{
			_comments = new List<Comment>
			{
				new Comment
				{
					GameKey = ValidString
				},

				new Comment
				{
					GameKey = ValidString
				},

				new Comment
				{
					GameKey = ValidString
				}
			};

			_mockOfCommentRepository.Setup(m => m.GetAll()).Returns(_comments);

			var result = _target.GetAll(ValidString).ToList().Count;

			Assert.AreEqual(result, 3);
		}

		[TestMethod]
		public void GetBy_ReturnsNoComments_WhenInValidGameKeyIsPassed()
		{
			_comments = new List<Comment>
			{
				new Comment
				{
					GameKey = ValidString
				},

				new Comment
				{
					GameKey = ValidString
				},

				new Comment
				{
					GameKey = ValidString
				}
			};

			_mockOfCommentRepository.Setup(m => m.GetAll()).Returns(_comments);

			var result = _target.GetAll(InValidString).ToList().Count;

			Assert.AreEqual(result, 0);
		}
	}
}
