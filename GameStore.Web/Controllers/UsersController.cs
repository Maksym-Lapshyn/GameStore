using System.Web.Mvc;
using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Models;

namespace GameStore.Web.Controllers
{
	public class UsersController : Controller
	{
		private readonly IUserService _userService;
		private readonly IMapper _mapper;

		public UsersController(IUserService userService,
			IMapper mapper)
		{
			_userService = userService;
			_mapper = mapper;
		}

		[HttpGet]
		public ActionResult New()
		{
			var model = new UserViewModel();

			return View(model);
		}

		[HttpPost]
		public ActionResult New(UserViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userDto = _mapper.Map<UserViewModel, UserDto>(model);
			_userService.Create(userDto);

			return RedirectToAction("ListAll", "Games");
		}

		[HttpGet]
		public ActionResult Update(string key)
		{
			var userDto = _userService.GetSingle(key);
			var model = _mapper.Map<UserDto, UserViewModel>(userDto);

			return View(model);
		}

		[HttpPost]
		public ActionResult Update(UserViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var roleDto = _mapper.Map<UserViewModel, UserDto>(model);
			_userService.Update(roleDto);

			return RedirectToAction("ListAll", "Games");
		}

		public ActionResult Show(string key)
		{
			var roleDto = _userService.GetSingle(key);
			var model = _mapper.Map<UserDto, UserViewModel>(roleDto);

			return View(model);
		}

		public ActionResult Delete(string key)
		{
			_userService.Delete(key);

			return RedirectToAction("ListAll", "Games");
		}
	}
}