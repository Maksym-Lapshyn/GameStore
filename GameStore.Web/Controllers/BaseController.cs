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
		private string CurrentLanguageCode { get; set; }

		public BaseController(IAuthentication authentication)
		{
			Auth = authentication;
		}

		public IAuthentication Auth { get; set; }

		public User CurrentUser => Auth.User;

		protected override void Initialize(RequestContext requestContext)
		{
			if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
			{
				CurrentLanguageCode = (string)requestContext.RouteData.Values["lang"];

				if (CurrentLanguageCode != null)
				{
					Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(CurrentLanguageCode);
				}
			}

			base.Initialize(requestContext);
		}
	}
}