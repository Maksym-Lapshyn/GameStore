using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Common.Enums;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.App_LocalResources;
using GameStore.Web.Infrastructure.Attributes;
using GameStore.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.Administrator)]
	public class UsersController : BaseController
	{
		private readonly IUserService _userService;
		private readonly IRoleService _roleService;
		private readonly IMapper _mapper;

		public UsersController(IUserService userService,
			IRoleService roleService,
			IMapper mapper,
			IAuthentication authentication)
			: base(authentication)
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
			CheckIfLoginIsUnique(model);

			if (!ModelState.IsValid)
			{
				model.RolesData = GetRoles();
				return View(model);
			}

			var userDto = _mapper.Map<UserViewModel, UserDto>(model);
			_userService.Create(userDto);

			return RedirectToAction("ShowAll", "Users");
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
			CheckIfLoginIsUnique(model);

			if (!ModelState.IsValid)
			{
				model.RolesData = GetRoles();
				return View(model);
			}

			var roleDto = _mapper.Map<UserViewModel, UserDto>(model);
			_userService.Update(roleDto);

			return RedirectToAction("ShowAll", "Users");
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

			return RedirectToAction("ShowAll", "Users");
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

		private void CheckIfLoginIsUnique(UserViewModel user)
		{
			if (!_userService.Contains(user.Login))
			{
				return;
			}

			var existingUser = _userService.GetSingle(user.Login);

			if (existingUser.Id != user.Id)
			{
				ModelState.AddModelError("Login", GlobalResource.UserWithSuchLoginAlreadyExists);
			}
		}
	}
}