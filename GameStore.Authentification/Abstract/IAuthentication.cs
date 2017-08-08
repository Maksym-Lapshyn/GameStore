using GameStore.Common.Entities;
using System.Security.Principal;
using System.Web;

namespace GameStore.Authentification.Abstract
{
	public interface IAuthentication
	{
		HttpContext HttpContext { get; set; }

		User Login(string userName, string password, bool isPersistent);

		User Login(string userName);

		void LogOut();

		IPrincipal CurrentUser { get; }
	}
}