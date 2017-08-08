using GameStore.Authentification.Abstract;
using GameStore.Common.Entities;

namespace GameStore.Web.Controllers
{
	public class BaseController
	{
		public IAuthentication Auth { get; set; }

		public User CurrentUser => ((IUserProvider) Auth.CurrentUser.Identity).User;
	}
}