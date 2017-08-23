using GameStore.Authentification.Abstract;
using GameStore.Common.Entities;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	public class BaseController : Controller
	{
		public BaseController(IAuthentication authentication)
		{
			Auth = authentication;
		}

		public IAuthentication Auth { get; set; }

		public User CurrentUser => Auth.User;
	}
}