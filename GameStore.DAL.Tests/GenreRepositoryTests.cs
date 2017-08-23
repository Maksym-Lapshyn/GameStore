using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Concrete.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq.Expressions;

namespace GameStore.DAL.Tests
{
	[TestClass]
	public class GenreRepositoryTests
	{
		private const string ValidString = "test";
		private const string InvalidString = "testtest";

		private Mock<ICopier<Genre>> _mockOfCloner;
		private Mock<IEfGenreRepository> _mockOfEfRepository;
		private Mock<IMongoGenreRepository> _mockOfMongoRepository;
		private IGenreRepository _target;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfCloner = new Mock<ICopier<Genre>>();
			_mockOfEfRepository = new Mock<IEfGenreRepository>();
			_mockOfMongoRepository = new Mock<IMongoGenreRepository>();
			_target = new GenreRepository(_mockOfEfRepository.Object, _mockOfMongoRepository.Object, _mockOfCloner.Object);
		}

		[TestMethod]
		public void GetSingle_ClonesGenre_WhenNonExistingGenreNameIsPassed()
		{
			_mockOfEfRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Genre, bool>>>())).Returns(false);
			_mockOfCloner.Setup(m => m.Copy(It.IsAny<Genre>())).Returns<Genre>(g => new Genre { Name = ValidString });

			var result = _target.GetSingle(g => g.Name == InvalidString);

			Assert.AreEqual(ValidString, result.Name);
		}

		[TestMethod]
		public void GetSingle_ReturnsGenreFromEfRepository_WhenNonExistingGenreNameIsPassed()
		{
			_mockOfEfRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Genre, bool>>>())).Returns(true);
			_mockOfEfRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Genre, bool>>>())).Returns(new Genre { Name = ValidString });

			var result = _target.GetSingle(g => g.Name == ValidString);

			Assert.AreEqual(ValidString, result.Name);
		}
	}
}