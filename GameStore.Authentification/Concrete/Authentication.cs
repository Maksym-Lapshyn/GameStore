using GameStore.Authentification.Abstract;
using GameStore.DAL.Entities;
using System;
using System.Security.Principal;
using System.Web;
using GameStore.DAL.Abstract.EntityFramework;

namespace GameStore.Authentification.Concrete
{
	public class Authentication : IAuthentication
	{
		private readonly IEfUserRepository _repository;
		public HttpContext HttpContext { get; set; }

		public Authentication(IEfUserRepository repository)
		{
			_repository = repository;
		}

		public User Login(string login, string password, bool isPersistent)
		{
			var user = _repository.Contains(login, password) ? _repository
			var user = _repository.GetSingle(login, password);

			return user;
		}

		public User Login(string login)
		{
			throw new NotImplementedException();
		}

		public void LogOut()
		{
			throw new NotImplementedException();
		}

		public IPrincipal CurrentUser { get; set; }
	}
}