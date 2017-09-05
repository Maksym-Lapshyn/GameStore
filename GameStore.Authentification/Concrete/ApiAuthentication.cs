using GameStore.Authentification.Abstract;
using GameStore.Common.Abstract;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using System;
using System.Web.Security;

namespace GameStore.Authentification.Concrete
{
	public class ApiAuthentication : IApiAuthentication
	{
		private readonly IUserRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHashGenerator<string> _hashGenerator;

		private User _currentUser;

		public ApiAuthentication(IUserRepository repository,
			IUnitOfWork unitOfWork,
			IHashGenerator<string> hashGenerator)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
			_hashGenerator = hashGenerator;
		}

		public User CurrentUser => _currentUser;

		public string LogIn(string login, string password, bool isPersistent)
		{
			var hashedPassword = _hashGenerator.Generate(password);
			var user = _repository.GetSingleOrDefault(u => u.Login == login && u.Password == hashedPassword && u.IsDeleted == false);

			if (user == null)
			{
				return null;
			}

			var encryptedTicket = CreateTicket(login, isPersistent);
			user.AuthenticationTicket = encryptedTicket;

			_repository.Update(user);

			_unitOfWork.Save();

			return encryptedTicket;
		}

		public User GetUserBy(string token)
		{
			var user = _repository.GetSingleOrDefault(u => u.AuthenticationTicket == token);

			if (user == null)
			{
				return null;
			}

			_currentUser = user;

			if (user.AuthenticationTicket == null)
			{
				return null;
			}

			var ticket = FormsAuthentication.Decrypt(user.AuthenticationTicket);

			if (ticket != null && !ticket.Expired)
			{
				return user;
			}

			return null;
		}

		private string CreateTicket(string login, bool isPersistent = false)
		{
			var ticket = new FormsAuthenticationTicket(1, login, DateTime.UtcNow,
				DateTime.UtcNow.Add(FormsAuthentication.Timeout), isPersistent, string.Empty, FormsAuthentication.FormsCookiePath);
			var encryptedTicket = FormsAuthentication.Encrypt(ticket);

			return encryptedTicket;
		}
	}
}