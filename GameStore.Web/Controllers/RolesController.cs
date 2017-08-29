using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Common.Enums;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.App_LocalResources;
using GameStore.Web.Infrastructure.Attributes;
using GameStore.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.Administrator)]
	public class RolesController : BaseController
	{
		private readonly IRoleService _roleService;
		private readonly IMapper _mapper;

		public RolesController(IRoleService roleService,
			IMapper mapper,
			IAuthentication authentication)
			: base(authentication)
		{
			_roleService = roleService;
			_mapper = mapper;
		}

		[HttpGet]
		public ActionResult New()
		{
			var model = new RoleViewModel();

			return View(model);
		}

		[HttpPost]
		public ActionResult New(RoleViewModel model)
		{
			CheckIfNameIsUnique(model);

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var roleDto = _mapper.Map<RoleViewModel, RoleDto>(model);
			_roleService.Create(CurrentLanguage, roleDto);

			return RedirectToAction("ShowAll", "Roles");
		}

		[HttpGet]
		public ActionResult Update(string key)
		{
			var roleDto = _roleService.GetSingle(CurrentLanguage, key);
			var model = _mapper.Map<RoleDto, RoleViewModel>(roleDto);

			return View(model);
		}

		[HttpPost]
		public ActionResult Update(RoleViewModel model)
		{
			CheckIfNameIsUnique(model);

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var roleDto = _mapper.Map<RoleViewModel, RoleDto>(model);
			_roleService.Update(CurrentLanguage, roleDto);

			return RedirectToAction("ShowAll", "Roles");
		}

		public ActionResult Show(string key)
		{
			var roleDto = _roleService.GetSingle(CurrentLanguage, key);
			var model = _mapper.Map<RoleDto, RoleViewModel>(roleDto);

			return View(model);
		}

		public ActionResult Delete(string key)
		{
			_roleService.Delete(key);

			return RedirectToAction("ShowAll", "Roles");
		}

		public ActionResult ShowAll()
		{
			var roleDtos = _roleService.GetAll(CurrentLanguage);
			var roleViewModels = _mapper.Map<IEnumerable<RoleDto>, List<RoleViewModel>>(roleDtos);

			return View(roleViewModels);
		}

		private void CheckIfNameIsUnique(RoleViewModel model)
		{
			if (!_roleService.Contains(CurrentLanguage, model.Name))
			{
				return;
			}

			var existingRole = _roleService.GetSingle(CurrentLanguage, model.Name);

			if (existingRole.Id != model.Id)
			{
				ModelState.AddModelError("Name", GlobalResource.RoleWithSuchNameAlreadyExists);
			}
		}
	}
}