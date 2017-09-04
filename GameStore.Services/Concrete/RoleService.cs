using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Abstract.Localization;

namespace GameStore.Services.Concrete
{
	public class RoleService : IRoleService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IInputLocalizer<Role> _inputLocalizer;
		private readonly IOutputLocalizer<Role> _outputLocalizer;
		private readonly IRoleLocaleRepository _localeRepository;
		private readonly IRoleRepository _roleRepository;

		public RoleService(IUnitOfWork unitOfWork,
			IMapper mapper,
			IInputLocalizer<Role> inputLocalizer,
			IOutputLocalizer<Role> outputLocalizer,
			IRoleLocaleRepository localeRepository,
			IRoleRepository roleRepository
			)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_inputLocalizer = inputLocalizer;
			_outputLocalizer = outputLocalizer;
			_localeRepository = localeRepository;
			_roleRepository = roleRepository;
		}

		public void Create(string language, RoleDto roleDto)
		{
			var role = _mapper.Map<RoleDto, Role>(roleDto);
			_inputLocalizer.Localize(language, role);
			_roleRepository.Create(role);
			_unitOfWork.Save();
		}

		public RoleDto GetSingle(string language, string name)
		{
			var role = _roleRepository.GetSingle(r => r.RoleLocales.Any(l => l.Name == name));
			_outputLocalizer.Localize(language, role);
			var roleDto = _mapper.Map<Role, RoleDto>(role);

			return roleDto;
		}

		public IEnumerable<RoleDto> GetAll(string language)
		{
			var roles = _roleRepository.GetAll().ToList();

			foreach (var role in roles)
			{
				_outputLocalizer.Localize(language, role);
			}

			return _mapper.Map<IEnumerable<Role>, List<RoleDto>>(roles);
		}

		public void Update(string language, RoleDto roleDto)
		{
			var role = _roleRepository.GetSingle(r => r.Id == roleDto.Id);
			role = _mapper.Map(roleDto, role);
			role.RoleLocales = _localeRepository.GetAllBy(role.Id).ToList();
			_inputLocalizer.Localize(language, role);
			_roleRepository.Update(role);
			_unitOfWork.Save();
		}

		public void Delete(string name)
		{
			var role = _roleRepository.GetSingle(r => r.Name == name);
			role.IsDeleted = true;
			_roleRepository.Update(role);
			_unitOfWork.Save();
		}

		public bool Contains(string language, string name)
		{
			return _roleRepository.Contains(r => r.RoleLocales.Any(l => l.Language.Name == language && l.Name == name));
		}
	}
}