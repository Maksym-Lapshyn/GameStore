using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Models;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	public class AccountsController : BaseController
	{
		private readonly IUserService _userService;
		private readonly IMapper _mapper;

		public AccountsController(IUserService userService,
			IMapper mapper,
			IAuthentication authentication)
			: base(authentication)
		{
			_userService = userService;
			_mapper = mapper;
		}

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

			var user = Auth.LogIn(model.Login, model.Password, model.IsPersistent);

			if (user != null)
			{
				return RedirectToAction("ShowAll", "Games");
			}

			ModelState.AddModelError("Password", "Passwords do not match");

			return View(model);
		}

		[HttpGet]
		public ActionResult Register()
		{
			return View(new RegisterViewModel());
		}

		[HttpPost]
		public ActionResult Register(RegisterViewModel model)
		{
			if (_userService.Contains(model.Login))
			{
				ModelState.AddModelError("Login", "User with such login already exists");
			}
			
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userDto = new UserDto {Login = model.Login, Password = model.Login};
			_userService.Create(userDto);
			Auth.LogIn(model.Login, model.Password, true);

			return RedirectToAction("ShowAll", "Games");
		}

		public ActionResult Logout()
		{
			Auth.LogOut();
			return RedirectToAction("ShowAll", "Games");
		}
	}
}