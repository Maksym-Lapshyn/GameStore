using GameStore.Authentification.Abstract;
using GameStore.Common.Entities;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.Web.Controllers
{
	public class BaseController : Controller
	{
		public BaseController(IAuthentication authentication)
		{
			Auth = authentication;
		}

		public string CurrentLanguage { get; set; }

		public IAuthentication Auth { get; set; }

		public User CurrentUser => Auth.User;

		protected override void Initialize(RequestContext requestContext)
		{
			if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
			{
				CurrentLanguage = (string)requestContext.RouteData.Values["lang"];

				if (CurrentLanguage != null)
				{
					Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(CurrentLanguage);
				}
			}

			base.Initialize(requestContext);
		}
	}
}