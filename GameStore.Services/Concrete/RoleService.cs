using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using System.Collections.Generic;
using System.Linq;

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

		public RoleDto GetSingle(string name)
		{
			var role = _roleRepository.GetSingle(name);
			var roleDto = _mapper.Map<Role, RoleDto>(role);

			return roleDto;
		}

		public IEnumerable<RoleDto> GetAll()
		{
			var roles = _roleRepository.GetAll();
			return _mapper.Map<IEnumerable<Role>, List<RoleDto>>(roles);
		}

		public void Update(RoleDto roleDto)
		{
			var role = _roleRepository.GetSingle(roleDto.Name);
			role = _mapper.Map(roleDto, role);
			role.Users = _userRepository.GetAll().Where(u => u.Roles.Any(r => r.Name == roleDto.Name)).ToList();
			_roleRepository.Update(role);
			_unitOfWork.Save();
		}

		public void Delete(string name)
		{
			var role = _roleRepository.GetSingle(name);
			role.IsDeleted = true;
			_roleRepository.Update(role);
			_unitOfWork.Save();
		}
	}
}