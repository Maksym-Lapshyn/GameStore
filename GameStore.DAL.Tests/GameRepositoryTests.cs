using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Concrete.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Tests
{
	[TestClass]
	public class GameRepositoryTests
	{
		private const string ValidString = "test";
		private const string InvalidString = "testtest";
		private Mock<IPipeline<IQueryable<Game>>> _mockOfPipeline;
		private Mock<IFilterMapper> _mockOfMapper;
		private Mock<ISynchronizer<Game>> _mockOfSynchronizer;
		private Mock<ICopier<Game>> _mockOfCloner;
		private Mock<IEfGameRepository> _mockOfEfRepository;
		private Mock<IMongoGameRepository> _mockOfMongoRepository;
		private IGameRepository _target;
		private List<Game> _efGames;
		private List<Game> _mongoGames;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfPipeline = new Mock<IPipeline<IQueryable<Game>>>();
			_mockOfMapper = new Mock<IFilterMapper>();
			_mockOfSynchronizer = new Mock<ISynchronizer<Game>>();
			_mockOfCloner = new Mock<ICopier<Game>>();
			_mockOfEfRepository = new Mock<IEfGameRepository>();
			_mockOfMongoRepository = new Mock<IMongoGameRepository>();
			_target = new GameRepository(_mockOfPipeline.Object, _mockOfMapper.Object, _mockOfEfRepository.Object, _mockOfMongoRepository.Object, _mockOfSynchronizer.Object, _mockOfCloner.Object);
		}

		[TestMethod]
		public void GetAll_ReturnsAllGamesFromBothRepositories()
		{
			_efGames = new List<Game>
			{
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = ValidString}
			};

			_mongoGames = new List<Game>
			{
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = ValidString}
			};

			_mockOfEfRepository.Setup(m => m.GetAll(null)).Returns(_efGames.AsQueryable());
			_mockOfMongoRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Game,bool>>>())).Returns(_mongoGames.AsQueryable);
			_mockOfSynchronizer.Setup(m => m.Synchronize(It.IsAny<Game>())).Returns<Game>(g => g);

			var result = _target.GetAll();

			Assert.AreEqual(6, result.Count());
		}

		[TestMethod]
		public void GetAll_ReturnsDistinctGamesFromBothRepositories()
		{
			_efGames = new List<Game>
			{
				new Game{Key = ValidString, NorthwindId = ValidString},
				new Game{Key = ValidString, NorthwindId = ValidString},
				new Game{Key = ValidString}
			};

			_mongoGames = new List<Game>
			{
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = ValidString}
			};

			_mockOfEfRepository.Setup(m => m.GetAll(null)).Returns(_efGames.AsQueryable());
			_mockOfMongoRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_mongoGames.AsQueryable);
			_mockOfSynchronizer.Setup(m => m.Synchronize(It.IsAny<Game>())).Returns<Game>(g => g);

			var result = _target.GetAll();

			Assert.AreEqual(5, result.Count());
		}

		[TestMethod]
		public void GetAll_SkipsGames()
		{
			_efGames = new List<Game>
			{
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = InvalidString}
			};

			_mongoGames = new List<Game>
			{
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = ValidString}
			};

			_mockOfEfRepository.Setup(m => m.GetAll(null)).Returns(_efGames.AsQueryable());
			_mockOfMongoRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_mongoGames.AsQueryable);
			_mockOfSynchronizer.Setup(m => m.Synchronize(It.IsAny<Game>())).Returns<Game>(g => g);

			var result = _target.GetAll(null, 4, 4).ToList();

			Assert.AreEqual(InvalidString, result[0].Key);
		}

		[TestMethod]
		public void GetAll_TakesGames()
		{
			_efGames = new List<Game>
			{
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = InvalidString}
			};

			_mongoGames = new List<Game>
			{
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = ValidString}
			};

			_mockOfEfRepository.Setup(m => m.GetAll(null)).Returns(_efGames.AsQueryable());
			_mockOfMongoRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_mongoGames.AsQueryable);
			_mockOfSynchronizer.Setup(m => m.Synchronize(It.IsAny<Game>())).Returns<Game>(g => g);

			var result = _target.GetAll(null, 0, 4);

			Assert.AreEqual(4, result.Count());
		}

		[TestMethod]
		public void GetAll_SkipsAndTakesGames()
		{
			_efGames = new List<Game>
			{
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = ValidString},
				new Game{Key = InvalidString}
			};

			_mongoGames = new List<Game>
			{
				new Game{Key = InvalidString},
				new Game{Key = InvalidString},
				new Game{Key = InvalidString},
				new Game{Key = ValidString},
				new Game{Key = ValidString}
			};

			_mockOfEfRepository.Setup(m => m.GetAll(null)).Returns(_efGames.AsQueryable());
			_mockOfMongoRepository.Setup(m => m.GetAll(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_mongoGames.AsQueryable);
			_mockOfSynchronizer.Setup(m => m.Synchronize(It.IsAny<Game>())).Returns<Game>(g => g);

			var result = _target.GetAll(null, 4, 4).ToList();

			Assert.AreEqual(true, result.All(g => g.Key == InvalidString));
			Assert.AreEqual(4, result.Count);
		}

		[TestMethod]
		public void GetSingle_ClonesGame_WhenNonExistingGameKeyIsPassed()
		{
			_mockOfEfRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Game, bool>>>())).Returns(false);
			_mockOfCloner.Setup(m => m.Copy(It.IsAny<Game>())).Returns<Game>(g => new Game{Key = ValidString});

			var result = _target.GetSingle(g => g.Key == InvalidString);

			Assert.AreEqual(ValidString, result.Key);
		}

		[TestMethod]
		public void GetSingle_SynchronizesGame_WhenNonExistingGameKeyIsPassed()
		{
			_mockOfEfRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Game, bool>>>())).Returns(true);
			_mockOfSynchronizer.Setup(m => m.Synchronize(It.IsAny<Game>())).Returns<Game>(g => new Game { Key = ValidString });
			_mockOfEfRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Game, bool>>>())).Returns(new Game());

			var result = _target.GetSingle(g => g.Key == InvalidString);

			Assert.AreEqual(ValidString, result.Key);
		}
	}
}