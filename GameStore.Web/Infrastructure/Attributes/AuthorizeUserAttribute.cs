using GameStore.Authentification.Abstract;
using GameStore.Common.Enums;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Infrastructure.Attributes
{
	public class AuthorizeUserAttribute : AuthorizeAttribute
	{
		private readonly AccessLevel[] _accessLevels;
		private readonly AuthorizationMode _mode;

		private IAuthentication _authentication;

		public AuthorizeUserAttribute(AuthorizationMode mode, params AccessLevel[] accessLevels)
		{
			_accessLevels = accessLevels;
			_mode = mode;
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			_authentication = DependencyResolver.Current.GetService<IAuthentication>();

			if (_mode == AuthorizationMode.Allow)
			{
				return httpContext.User.Identity.IsAuthenticated && _authentication.User.Roles.Any(r => _accessLevels.Contains(r.AccessLevel));
				//attribute allows access
			}

			if (httpContext.User.Identity.IsAuthenticated)
			{
				return  !_authentication.User.Roles.Any(r => _accessLevels.Contains(r.AccessLevel));
				//attribute forbids access
			}

			return true;
			//attribute grants access to unauthorized user
		}
	}
}