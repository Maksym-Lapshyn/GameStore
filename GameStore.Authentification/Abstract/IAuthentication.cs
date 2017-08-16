using GameStore.Common.Entities;
using System.Security.Principal;
using System.Web;

namespace GameStore.Authentification.Abstract
{
	public interface IAuthentication
	{
		HttpContext HttpContext { get; set; }

		User User { get; }

		User Login(string login, string password, bool isPersistent);

		void LogOut();

		IPrincipal CurrentUser { get; }
	}
}