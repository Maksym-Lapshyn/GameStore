using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Concrete.Common;
using GameStore.DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
			_mockOfEfRepository.Setup(m => m.Contains(InvalidString)).Returns(false);
			_mockOfCloner.Setup(m => m.Copy(It.IsAny<Genre>())).Returns<Genre>(g => new Genre { Name = ValidString });

			var result = _target.GetSingle(InvalidString);

			Assert.AreEqual(ValidString, result.Name);
		}

		[TestMethod]
		public void GetSingle_ReturnsGenreFromEfRepository_WhenNonExistingGenreNameIsPassed()
		{
			_mockOfEfRepository.Setup(m => m.Contains(ValidString)).Returns(true);
			_mockOfEfRepository.Setup(m => m.GetSingle(ValidString)).Returns(new Genre { Name = ValidString });

			var result = _target.GetSingle(ValidString);

			Assert.AreEqual(ValidString, result.Name);
		}
	}
}