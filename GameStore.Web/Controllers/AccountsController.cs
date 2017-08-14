using AutoMapper;
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

		public AccountsController(IUserService userService, IMapper mapper)
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

			var user = Auth.Login(model.Login, model.Password, model.IsPersistent);

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
			return View(new UserViewModel());
		}

		[HttpPost]
		public ActionResult Register(UserViewModel model)
		{
			if (_userService.Contains(model.Login))
			{
				ModelState.AddModelError("Login", "User with such login already exists");
			}
			
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userDto = _mapper.Map<UserViewModel, UserDto>(model);
			_userService.Create(userDto);
			Auth.Login(model.Login, model.Password, true);

			return RedirectToAction("ShowAll", "Games");
		}

		public ActionResult Logout()
		{
			Auth.LogOut();
			return RedirectToAction("ShowAll", "Games");
		}
	}
}