using GameStore.Common.Enums;
using System.Web.Mvc;

namespace GameStore.Web.Infrastructure.Attributes
{
	public class CustomAuthorizeAttribute : AuthorizeAttribute
	{
		public AccessLevel AccessLevel { get; set; }

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if (AccessLevel != 0)
			{
				Roles = AccessLevel.ToString();
			}

			base.OnAuthorization(filterContext);
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			if (filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				filterContext.HttpContext.Response.StatusCode = 401;
			}

			filterContext.HttpContext.Response.StatusCode = 403;
			base.HandleUnauthorizedRequest(filterContext);
		}
	}
}