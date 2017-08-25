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
		private readonly IMapper _mapper;
		private readonly IRoleRepository _roleRepository;
		private readonly IUserRepository _userRepository;
		private readonly IUnitOfWork _unitOfWork;

		public RoleService(IMapper mapper, IRoleRepository roleRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_roleRepository = roleRepository;
			_userRepository = userRepository;
			_unitOfWork = unitOfWork;
		}

		public void Create(RoleDto roleDto)
		{
			var role = _mapper.Map<RoleDto, Role>(roleDto);
			_roleRepository.Create(role);
			_unitOfWork.Save();
		}

		public RoleDto GetSingle(string name, string language)
		{
			var role = _roleRepository.GetSingle(r => r.Name == name, language);
			var roleDto = _mapper.Map<Role, RoleDto>(role);

			return roleDto;
		}

		public IEnumerable<RoleDto> GetAll(string language)
		{
			var roles = _roleRepository.GetAll(language);
			return _mapper.Map<IEnumerable<Role>, List<RoleDto>>(roles);
		}

		public void Update(RoleDto roleDto)
		{
			var role = _roleRepository.GetSingle(r => r.Id == roleDto.Id);
			role = _mapper.Map(roleDto, role);
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

		public bool Contains(string name)
		{
			return _roleRepository.Contains(r => r.Name == name);
		}
	}
}