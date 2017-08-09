using GameStore.Authentification.Abstract;
using GameStore.Common.Entities;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	public class BaseController : Controller
	{
		public IAuthentication Auth { get; set; }

		public User CurrentUser => ((IUserProvider) Auth.CurrentUser.Identity).User;
	}
}