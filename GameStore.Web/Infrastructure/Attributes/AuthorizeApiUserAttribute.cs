using GameStore.Authentification.Abstract;
using GameStore.Common.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace GameStore.Web.Infrastructure.Attributes
{
	public class AuthorizeApiUserAttribute : AuthorizeAttribute
	{
		private const string TokenName = "__GAMESTORE_TOKEN";

		private readonly AccessLevel[] _accessLevels;
		private readonly AuthorizationMode _mode;

		private IApiAuthentication _authentication;

		public AuthorizeApiUserAttribute(AuthorizationMode mode, params AccessLevel[] accessLevels)
		{
			_accessLevels = accessLevels;
			_mode = mode;
		}
		protected override bool IsAuthorized(HttpActionContext actionContext)
		{
			_authentication = DependencyResolver.Current.GetService<IApiAuthentication>();
			actionContext.Request.Headers.TryGetValues(TokenName, out IEnumerable<string> tokens);
			var token = tokens?.FirstOrDefault();

			if (token == null)
			{
				return false;
			}

			var user = _authentication.GetUserBy(token);

			if (user == null)
			{
				return false;
			}

			if (_mode == AuthorizationMode.Allow)
			{
				return user.Roles.Any(r => _accessLevels.Contains(r.AccessLevel));
			}

			return !user.Roles.Any(r => _accessLevels.Contains(r.AccessLevel));
		}
	}
}