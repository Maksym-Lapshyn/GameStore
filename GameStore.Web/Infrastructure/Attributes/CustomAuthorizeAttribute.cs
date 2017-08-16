using GameStore.Authentification.Abstract;
using GameStore.Common.Enums;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Infrastructure.Attributes
{
	public class CustomAuthorizeAttribute : AuthorizeAttribute
	{
		private readonly AccessLevel[] _accessLevels;
		private readonly IAuthentication _auth;
		private readonly AuthorizationMode _mode;

		public CustomAuthorizeAttribute(AuthorizationMode mode, params AccessLevel[] accessLevels)
		{
			_accessLevels = accessLevels;
			_mode = mode;
			_auth = DependencyResolver.Current.GetService<IAuthentication>();
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			if (_mode == AuthorizationMode.Allow)
			{
				return httpContext.User.Identity.IsAuthenticated && _auth.User.Roles.Any(r => _accessLevels.Contains(r.AccessLevel));
				//attribute allows access
			}

			if (httpContext.User.Identity.IsAuthenticated)
			{
				return  !_auth.User.Roles.Any(r => _accessLevels.Contains(r.AccessLevel));
				//attribute forbids access
			}

			return true;
			//attribute grants access to unauthorized user
		}
	}
}