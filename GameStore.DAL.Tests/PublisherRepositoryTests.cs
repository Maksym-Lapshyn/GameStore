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
	public class PublisherRepositoryTests
	{
		private const string ValidString = "test";
		private const string InvalidString = "testtest";
		private Mock<ICopier<Publisher>> _mockOfCloner;
		private Mock<IEfPublisherRepository> _mockOfEfRepository;
		private Mock<IMongoPublisherRepository> _mockOfMongoRepository;
		private IPublisherRepository _target;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfCloner = new Mock<ICopier<Publisher>>();
			_mockOfEfRepository = new Mock<IEfPublisherRepository>();
			_mockOfMongoRepository = new Mock<IMongoPublisherRepository>();
			_target = new PublisherRepository(_mockOfEfRepository.Object, _mockOfMongoRepository.Object, _mockOfCloner.Object);
		}

		[TestMethod]
		public void GetSingle_ClonesGenre_WhenNonExistingCompanyNameIsPassed()
		{
			_mockOfEfRepository.Setup(m => m.Contains(InvalidString)).Returns(false);
			_mockOfCloner.Setup(m => m.Copy(It.IsAny<Publisher>())).Returns<Publisher>(g => new Publisher { CompanyName = ValidString });

			var result = _target.GetSingle(InvalidString);

			Assert.AreEqual(ValidString, result.CompanyName);
		}

		[TestMethod]
		public void GetSingle_ReturnsGenreFromEfRepository_WhenNonExistingGenreNameIsPassed()
		{
			_mockOfEfRepository.Setup(m => m.Contains(ValidString)).Returns(true);
			_mockOfEfRepository.Setup(m => m.GetSingle(ValidString)).Returns(new Publisher() { CompanyName = ValidString });

			var result = _target.GetSingle(ValidString);

			Assert.AreEqual(ValidString, result.CompanyName);
		}
	}
}