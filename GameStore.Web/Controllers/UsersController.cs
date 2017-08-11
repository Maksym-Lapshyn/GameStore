using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Common.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Models;

namespace GameStore.Web.Controllers
{
	public class UsersController : Controller
	{
		private readonly IUserService _userService;
		private readonly IRoleService _roleService;
		private readonly IMapper _mapper;

		public UsersController(IUserService userService,
			IRoleService roleService,
			IMapper mapper)
		{
			_userService = userService;
			_roleService = roleService;
			_mapper = mapper;
		}

		[HttpGet]
		public ActionResult New()
		{
			var model = new UserViewModel
			{
				RolesData = GetRoles()
			};

			return View(model);
		}

		[HttpPost]
		public ActionResult New(UserViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.RolesData = GetRoles();
				return View(model);
			}

			var userDto = _mapper.Map<UserViewModel, UserDto>(model);
			_userService.Create(userDto);

			return RedirectToAction("ShowAll", "Games");
		}

		[HttpGet]
		public ActionResult Update(string key)
		{
			var userDto = _userService.GetSingle(key);
			var model = _mapper.Map<UserDto, UserViewModel>(userDto);
			model.RolesData = GetRoles();

			return View(model);
		}

		[HttpPost]
		public ActionResult Update(UserViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.RolesData = GetRoles();
				return View(model);
			}

			var roleDto = _mapper.Map<UserViewModel, UserDto>(model);
			_userService.Update(roleDto);

			return RedirectToAction("ShowAll", "Games");
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

			return RedirectToAction("ShowAll", "Games");
		}

		public ActionResult ShowAll()
		{
			var userDtos = _userService.GetAll();
			var userViewModels = _mapper.Map<IEnumerable<UserDto>, List<UserViewModel>>(userDtos);

			return View(userViewModels);
		}

		private List<RoleViewModel> GetRoles()
		{
			return _mapper.Map<IEnumerable<RoleDto>, IEnumerable<RoleViewModel>>(_roleService.GetAll()).ToList();
		}
	}
}