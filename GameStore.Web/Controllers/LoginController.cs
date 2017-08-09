using System.Web.Mvc;
using GameStore.Web.Models;

namespace GameStore.Web.Controllers
{
	public class LoginController : BaseController
	{
		[HttpGet]
		public ActionResult Login()
		{
			return View(new LoginViewModel());
		}

		[HttpPost]
		public ActionResult Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = Auth.Login(model.UserName, model.Password, model.IsPersistent);

			if (user != null)
			{
				return RedirectToAction("ListAll", "Games");
			}

			ModelState.AddModelError("Password", "Passwords do not match");

			return View(model);
		}

		public ActionResult Logout()
		{
			Auth.LogOut();
			return RedirectToAction("ListAll", "Games");
		}
	}
}