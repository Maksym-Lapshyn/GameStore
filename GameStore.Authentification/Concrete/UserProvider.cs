using GameStore.Authentification.Abstract;
using GameStore.Authentification.Infrastructure;
using GameStore.DAL.Abstract.Common;
using System.Linq;
using System.Security.Principal;

namespace GameStore.Authentification.Concrete
{
	public class UserProvider : IPrincipal
	{
		private readonly IUserIdentityProvider _userIdentity;

		public IIdentity Identity => _userIdentity;

		public UserProvider()
		{
			_userIdentity = new UserIdentityProvider();
		}

		public UserProvider(string login, IUserRepository repository)
		{
			_userIdentity = new UserIdentityProvider {User = repository.GetSingle(u => u.Login == login)};
		}

		public bool IsInRole(string role)
		{
			return _userIdentity.User.Roles.Any(r => r.AccessLevel.ToString() == role);
		}
	}
}