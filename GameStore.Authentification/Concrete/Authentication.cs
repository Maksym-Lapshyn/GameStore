using GameStore.Authentification.Abstract;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace GameStore.Authentification.Concrete
{
	public class Authentication : IAuthentication
	{
		private const string CookieName = "GameStoreAuthentication";
		private readonly IUserRepository _repository;
		private IPrincipal _currentUser;

		public HttpContext HttpContext { get; set; }

		public Authentication(IUserRepository repository)
		{
			_repository = repository;
		}

		public User Login(string userName, string password, bool isPersistent)
		{
			var user = _repository.Contains(userName, password) ? _repository.GetSingle(userName, password) : null;

			if (user != null)
			{
				CreateCookie(userName, isPersistent);
			}

			return user;
		}

		public User Login(string userName)
		{
			var user = _repository.GetAll().FirstOrDefault(u => string.Compare(u.Name, userName, StringComparison.OrdinalIgnoreCase) == 0);

			if (user != null)
			{
				CreateCookie(userName);
			}

			return user;
		}

		public void LogOut()
		{
			var cookie = HttpContext.Response.Cookies[CookieName];

			if (cookie != null)
			{
				cookie.Value = string.Empty;
			}
		}

		public IPrincipal CurrentUser
		{
			get
			{
				if (_currentUser != null)
				{
					return _currentUser;
				}

				var cookie = HttpContext.Request.Cookies.Get(CookieName);

				if (!string.IsNullOrEmpty(cookie?.Value))
				{
					var ticket = FormsAuthentication.Decrypt(cookie.Value);
					_currentUser = new UserProvider(ticket.Name, _repository);
				}
				else
				{
					_currentUser = new UserProvider();
				}

				return _currentUser;
			}
		}

		private void CreateCookie(string login, bool isPersistent = false)
		{
			var ticket = new FormsAuthenticationTicket(1, login, DateTime.UtcNow,
				DateTime.UtcNow.Add(FormsAuthentication.Timeout), isPersistent, string.Empty, FormsAuthentication.FormsCookiePath);
			var encryptedTicket = FormsAuthentication.Encrypt(ticket);
			var cookie = new HttpCookie(CookieName)
			{
				Value = encryptedTicket,
				Expires = DateTime.UtcNow.Add(FormsAuthentication.Timeout)
			};

			HttpContext.Response.Cookies.Set(cookie);
		}
	}
}