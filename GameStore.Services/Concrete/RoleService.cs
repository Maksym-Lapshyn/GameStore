using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Concrete
{
	public class RoleService : IRoleService
	{
        private const string DefaultLanguage = "en";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IInputLocalizer<Role> _localizer;
		private readonly IRoleRepository _roleRepository;
		private readonly IUserRepository _userRepository;


		public RoleService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IInputLocalizer<Role> localizer,
            IRoleRepository roleRepository,
            IUserRepository userRepository
            )
		{
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
			_roleRepository = roleRepository;
			_userRepository = userRepository;
		}

		public void Create(string language, RoleDto roleDto)
		{
			var role = _mapper.Map<RoleDto, Role>(roleDto);
            role = _localizer.Localize(language, role);
            _roleRepository.Create(role);
			_unitOfWork.Save();
		}

		public RoleDto GetSingle(string language, string name)
		{
			var role = _roleRepository.GetSingle(language, r => r.Name == name);
			var roleDto = _mapper.Map<Role, RoleDto>(role);

			return roleDto;
		}

		public IEnumerable<RoleDto> GetAll(string language)
		{
			var roles = _roleRepository.GetAll(language);
			return _mapper.Map<IEnumerable<Role>, List<RoleDto>>(roles);
		}

		public void Update(string language, RoleDto roleDto)
		{
			var role = _roleRepository.GetSingle(language, r => r.Id == roleDto.Id);
			role = _mapper.Map(roleDto, role);
            role = _localizer.Localize(language, role);
			_roleRepository.Update(role);
			_unitOfWork.Save();
		}

		public void Delete(string name)
		{
			var role = _roleRepository.GetSingle(DefaultLanguage, r => r.Name == name);
			role.IsDeleted = true;
			_roleRepository.Update(role);
			_unitOfWork.Save();
		}

		public bool Contains(string name)
		{
			return _roleRepository.Contains(r => r.Name == name);
		}
	}
}