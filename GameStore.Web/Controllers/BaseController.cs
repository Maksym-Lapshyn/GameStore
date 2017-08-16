using GameStore.Authentification.Abstract;
using Ninject;
using System.Web.Mvc;
using GameStore.Common.Entities;

namespace GameStore.Web.Controllers
{
	public class BaseController : Controller
	{
		[Inject]
		public IAuthentication Auth { get; set; }

		public User CurrentUser => Auth.User;
	}
}