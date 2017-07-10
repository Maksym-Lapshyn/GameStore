using System;
using GameStore.DAL.Abstract;
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
        private Mock<IUnitOfWork> _mockOfUow;
        private CommentService _target;
        private List<Comment> _comments;
        private List<Game> _games;
        private const int TestInt = 10;
        private const string ValidString = "test";
        private const string InValidString = "testtest";

        [TestInitialize]
        public void Initialize()
        {
            ServicesAutoMapperConfig.RegisterMappings();
            _mockOfUow = new Mock<IUnitOfWork>();
            _target = new CommentService(_mockOfUow.Object);
            _mockOfUow.Setup(m => m.CommentRepository.Insert(It.IsAny<Comment>())).Callback<Comment>(c => _comments.Add(c));
        }

        [TestMethod]
        public void Create_CreatesGame_WhenAnyCommentIsPassed()
        {
            _comments = new List<Comment>();
            _mockOfUow.Setup(m => m.CommentRepository.Get(null, null)).Returns(_comments);
            _games = new List<Game> { new Game { Id = TestInt } };
            _mockOfUow.Setup(m => m.GameRepository.Get(null, null)).Returns(_games);

            _target.Create(new CommentDto { GameId = TestInt });

            Assert.IsTrue(_comments.Count == 1);
        }

        [TestMethod]
        public void Create_CallsSaveOnce_WhenAnyCommentIsPassed()
        {
            _comments = new List<Comment>();
            _mockOfUow.Setup(m => m.CommentRepository.Get(null, null)).Returns(_comments);
            _games = new List<Game> { new Game { Id = TestInt } };
            _mockOfUow.Setup(m => m.GameRepository.Get(null, null)).Returns(_games);

            _target.Create(new CommentDto { GameId = TestInt });

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

            _mockOfUow.Setup(m => m.CommentRepository.Get(null, null)).Returns(_comments);

            var result = _target.GetAll().ToList().Count;

            Assert.IsTrue(result == 3);
        }

        [TestMethod]
        public void GetBy_ReturnsAllComments_WhenValidGameKeyIsPassed()
        {
            _games = new List<Game>
            {
                new Game
                {
                    Key = ValidString,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                    }
                },
                new Game
                {
                    Key = ValidString,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                    }
                },
                new Game
                {
                    Key = ValidString,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                    }
                }
            };

            _mockOfUow.Setup(m => m.GameRepository.Get(null, null)).Returns(_games);

            var comments = _target.GetBy(ValidString).ToList().Count;

            Assert.IsTrue(comments == 1);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void GetBy_ThrowsException_WhenInValidGameKeyIsPassed()
        {
            _games = new List<Game>
            {
                new Game
                {
                    Key = ValidString,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                    }
                },
                new Game
                {
                    Key = ValidString,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                    }
                },
                new Game
                {
                    Key = ValidString,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                    }
                }
            };

            _mockOfUow.Setup(m => m.GameRepository.Get(null, null)).Returns(_games);

            _target.GetBy(InValidString);
        }
    }
}
