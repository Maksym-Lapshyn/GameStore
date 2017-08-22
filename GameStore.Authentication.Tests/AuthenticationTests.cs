using GameStore.Common.Abstract;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Security;

namespace GameStore.Authentication.Tests
{
	[TestClass]
	public class AuthenticationTests
	{
		private const string CookieName = "__GAMESTORE_AUTH";
		private const string TestString = "test";
		private const string DefaultUserName = "Guest";
		private Mock<IUserRepository> _mockOfUserRepository;
		private Mock<IHasher<string>> _mockOfHasher;
		private Mock<HttpContextBase> _mockOfContext;
		private Mock<HttpResponseBase> _mockOfResponse;
		private Mock<HttpRequestBase> _mockOfRequest;
		private Authentification.Concrete.Authentication _target;
		private HttpCookieCollection _cookieCollection;
			
		[TestInitialize]
		public void Initialize()
		{
			_cookieCollection = new HttpCookieCollection();
			_mockOfContext = new Mock<HttpContextBase>();
			_mockOfResponse = new Mock<HttpResponseBase>();
			_mockOfRequest = new Mock<HttpRequestBase>();
			_mockOfResponse.Setup(m => m.Cookies).Returns(_cookieCollection);
			_mockOfRequest.Setup(m => m.Cookies).Returns(_cookieCollection);
			_mockOfContext.Setup(m => m.Response).Returns(_mockOfResponse.Object);
			_mockOfContext.Setup(m => m.Request).Returns(_mockOfRequest.Object);
			_mockOfUserRepository = new Mock<IUserRepository>();
			_mockOfHasher = new Mock<IHasher<string>>();
			_mockOfHasher.Setup(m => m.GenerateHash(It.IsAny<string>())).Returns<string>(s => s);
			_target = new Authentification.Concrete.Authentication(_mockOfUserRepository.Object, _mockOfHasher.Object)
			{
				HttpContext = _mockOfContext.Object
			};
		}

		[TestMethod]
		public void Login_ReturnsUser_WhenRepositoryContainsOne()
		{
			var user = new User
			{
				Login = TestString,
				Password = TestString
			};

			_mockOfUserRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<User, bool>>>())).Returns(true);
			_mockOfUserRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);

			var result = _target.Login(TestString, TestString, true);

			Assert.AreEqual(TestString, result.Login);
			Assert.AreEqual(TestString, result.Password);
		}

		[TestMethod]
		public void Login_AddsCookie_WhenRepositoryContainsUser()
		{
			var user = new User
			{
				Login = TestString,
				Password = TestString
			};

			_mockOfUserRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<User, bool>>>())).Returns(true);
			_mockOfUserRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);

			_target.Login(TestString, TestString, true);

			Assert.AreEqual(1, _cookieCollection.Count);
		}

		[TestMethod]
		public void Login_DoesNotCreateCookie_WhenRepositoryDoesNotContainUser()
		{
			_mockOfUserRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<User, bool>>>())).Returns(false);

			_target.Login(TestString, TestString, true);

			Assert.AreEqual(0, _cookieCollection.Count);
		}

		[TestMethod]
		public void Logout_CleansCookie_IfCookieExists()
		{
			_cookieCollection.Add(new HttpCookie(CookieName));

			_target.LogOut();

			Assert.AreEqual(string.Empty, _cookieCollection.Get(CookieName).Value);
		}

		[TestMethod]
		public void CurrentUser_ReturnsUserFromCookie_IfCookieValueIsNotNull()
		{
			var user = new User
			{
				Login = TestString,
				Password = TestString
			};

			_mockOfUserRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
			_cookieCollection.Add(CreateCookie());

			var result = _target.CurrentUser;

			Assert.AreEqual(TestString, result.Identity.Name);
		}

		[TestMethod]
		public void CurrentUser_ReturnsDefaultUser_IfCookieValueIsNull()
		{
			_cookieCollection.Add(new HttpCookie(CookieName));

			var result = _target.CurrentUser;

			Assert.AreEqual(DefaultUserName, result.Identity.Name);
		}

		private HttpCookie CreateCookie()
		{
			var ticket = new FormsAuthenticationTicket(1, TestString, DateTime.UtcNow,
				DateTime.UtcNow.Add(FormsAuthentication.Timeout), true, string.Empty, FormsAuthentication.FormsCookiePath);
			var encryptedTicket = FormsAuthentication.Encrypt(ticket);
			var cookie = new HttpCookie(CookieName)
			{
				Value = encryptedTicket,
				Expires = DateTime.UtcNow.Add(FormsAuthentication.Timeout)
			};

			return cookie;
		}
	}
}