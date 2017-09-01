using GameStore.Authentification.Abstract;
using GameStore.Authentification.Concrete;
using GameStore.Common.Abstract;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq.Expressions;
using System.Web.Security;

namespace GameStore.Authentication.Tests
{
	[TestClass]
	public class ApiAuthenticationTests
	{
		private const string ValidString = "test";
		private const string InvalidString = "testtest";

		private Mock<IUserRepository> _mockOfUserRepository;
		private Mock<IHashGenerator<string>> _mockOfHashGenerator;
		private Mock<IUnitOfWork> _mockOfUow;
		private IApiAuthentication _target;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfUserRepository = new Mock<IUserRepository>();
			_mockOfHashGenerator = new Mock<IHashGenerator<string>>();
			_mockOfUow = new Mock<IUnitOfWork>();
			_mockOfHashGenerator.Setup(m => m.Generate(It.IsAny<string>())).Returns<string>(s => s);
			_target = new ApiAuthentication(_mockOfUserRepository.Object, _mockOfUow.Object, _mockOfHashGenerator.Object);
		}

		[TestMethod]
		public void Login_ReturnsNull_WhenInvalidUserCredentialsArePassed()
		{
			_mockOfUserRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<User, bool>>>())).Returns(false);

			var result = _target.LogIn(InvalidString, InvalidString, true);

			Assert.IsNull(result);
		}

		[TestMethod]
		public void Login_ReturnsEncryptedTicket_WhenValidUserCredentialsArePassed()
		{
			_mockOfUserRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<User, bool>>>())).Returns(true);
			_mockOfUserRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User());

			var result = _target.LogIn(InvalidString, InvalidString, true);

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Login_UpdatesUserAuthenticationTicket_WhenValidUserCredentialsArePassed()
		{
			var user = new User();
			_mockOfUserRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<User, bool>>>())).Returns(true);
			_mockOfUserRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);

			_target.LogIn(ValidString, ValidString, true);

			Assert.IsNotNull(user.AuthenticationTicket);
		}

		[TestMethod]
		public void Login_CallsUpdateWithCorrectUser_WhenValidUserCredentialsArePassed()
		{
			var user = new User();
			_mockOfUserRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<User, bool>>>())).Returns(true);
			_mockOfUserRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);

			_target.LogIn(ValidString, ValidString, true);

			_mockOfUserRepository.Verify(m => m.Update(user), Times.Once);
		}

		[TestMethod]
		public void Login_CallsSaveOnce_WhenvalidUserCredentialsArePassed()
		{
			var user = new User();
			_mockOfUserRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<User, bool>>>())).Returns(true);
			_mockOfUserRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);

			_target.LogIn(ValidString, ValidString, true);

			_mockOfUow.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public void GetUserBy_ReturnsNull_WhenUserRepositoryDoesNotContainUserWithToken()
		{
			_mockOfUserRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<User, bool>>>())).Returns(false);

			var result = _target.GetUserBy(InvalidString);

			Assert.IsNull(result);
		}

		[TestMethod]
		public void GetUserBy_ReturnsNull_WhenUserDoesNotContainAuthenticationTicket()
		{
			var user = new User();
			_mockOfUserRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<User, bool>>>())).Returns(true);
			_mockOfUserRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);

			var result = _target.GetUserBy(InvalidString);

			Assert.IsNull(result);
		}

		[TestMethod]
		public void GetUserBy_ReturnsTicket_WhenUserContainsAuthenticationTicket()
		{
			var user = new User
			{
				AuthenticationTicket = CreateTicket()
			};

			_mockOfUserRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<User, bool>>>())).Returns(true);
			_mockOfUserRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);

			var result = _target.GetUserBy(InvalidString);

			Assert.AreEqual(user, result);
		}

		private string CreateTicket()
		{
			var ticket = new FormsAuthenticationTicket(1, ValidString, DateTime.UtcNow,
				DateTime.UtcNow.Add(FormsAuthentication.Timeout), true, string.Empty, FormsAuthentication.FormsCookiePath);
			var encryptedTicket = FormsAuthentication.Encrypt(ticket);

			return encryptedTicket;
		}
	}
}