using GameStore.Authentification.Infrastructure;
using GameStore.Common.Enums;
using GameStore.DAL.Abstract.Common;
using System;
using System.Linq;
using System.Security.Principal;

namespace GameStore.Authentification.Concrete
{
	public class UserProvider : IPrincipal
	{
		private readonly UserIdentity _userIdentity;

		public IIdentity Identity => _userIdentity;

		public UserProvider()
		{
			_userIdentity = new UserIdentity();
		}

		public UserProvider(string name, IUserRepository repository)
		{
			_userIdentity.User = repository.GetSingle(name);
		}

		public bool IsInRole(string roles)
		{
			if (_userIdentity.User?.Name != null)
			{
				var rolesArray = roles.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
				return rolesArray.Any(
					r =>
						string.Equals(Enum.GetName(typeof(AccessLevel), _userIdentity.User.Role.AccessLevel), r,
							StringComparison.CurrentCultureIgnoreCase));
			}

			return false;
		}
	}
}