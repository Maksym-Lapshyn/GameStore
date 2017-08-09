using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Models;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	public class RolesController : BaseController
	{
		private readonly IRoleService _roleService;
		private readonly IMapper _mapper;

		public RolesController(IRoleService roleService,
			IMapper mapper)
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
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var roleDto = _mapper.Map<RoleViewModel, RoleDto>(model);
			_roleService.Create(roleDto);

			return RedirectToAction("ListAll", "Games");
		}

		[HttpGet]
		public ActionResult Update(string key)
		{
			var roleDto = _roleService.GetSingle(key);
			var model = _mapper.Map<RoleDto, RoleViewModel>(roleDto);

			return View(model);
		}

		[HttpPost]
		public ActionResult Update(RoleViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var roleDto = _mapper.Map<RoleViewModel, RoleDto>(model);
			_roleService.Update(roleDto);

			return RedirectToAction("ListAll", "Games");
		}

		public ActionResult Show(string key)
		{
			var roleDto = _roleService.GetSingle(key);
			var model = _mapper.Map<RoleDto, RoleViewModel>(roleDto);

			return View(model);
		}

		public ActionResult Delete(string key)
		{
			_roleService.Delete(key);

			return RedirectToAction("ListAll", "Games");
		}
	}
}