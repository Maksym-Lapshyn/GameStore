using GameStore.Common.Entities;
using System.Security.Principal;
using System.Web;

namespace GameStore.Authentification.Abstract
{
	public interface IAuthentication
	{
		HttpContextBase HttpContext { get; set; }

		User User { get; }

		User LogIn(string login, string password, bool isPersistent);

		void LogOut();

		IPrincipal CurrentUser { get; }
	}
}