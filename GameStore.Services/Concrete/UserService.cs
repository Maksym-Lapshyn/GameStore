using AutoMapper;
using GameStore.Common.Abstract;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Concrete
{
	public class UserService : IUserService
	{
		private const string DefaultRoleName = "User";

		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
		private readonly IRoleRepository _roleRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly IHasher<string> _hasher;
		private readonly IUnitOfWork _unitOfWork;

		public UserService(IMapper mapper,
			IUserRepository userRepository,
			IRoleRepository roleRepository,
			IOrderRepository orderRepository,
			IHasher<string> hasher,
			IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_userRepository = userRepository;
			_roleRepository = roleRepository;
			_orderRepository = orderRepository;
			_hasher = hasher;
			_unitOfWork = unitOfWork;
		}

		public void Create(UserDto userDto)
		{
			AddDefaultRoleInput(userDto);
			var user = _mapper.Map<UserDto, User>(userDto);
			user = MapEmbeddedEntities(userDto, user);
			user.Password = _hasher.GenerateHash(user.Password);
			_userRepository.Create(user);
			_unitOfWork.Save();
		}

		public UserDto GetSingle(string name)
		{
			var user = _userRepository.GetSingle(u => u.Login == name);
			var userDto = _mapper.Map<User, UserDto>(user);

			return userDto;
		}

		public IEnumerable<UserDto> GetAll()
		{
			var users = _userRepository.GetAll();
			return _mapper.Map<IEnumerable<User>, List<UserDto>>(users);
		}

		public void Update(UserDto userDto)
		{
			AddDefaultRoleInput(userDto);
			var user = _mapper.Map<UserDto, User>(userDto);
			user = MapEmbeddedEntities(userDto, user);
			user.Password = _userRepository.GetSingle(u => u.Id == userDto.Id).Password;
			_userRepository.Update(user);
			_unitOfWork.Save();
		}

		public void Delete(string name)
		{
			var user = _userRepository.GetSingle(u => u.Login == name);
			user.IsDeleted = true;
			_userRepository.Update(user);
			_unitOfWork.Save();
		}

		public bool Contains(string name)
		{
			return _userRepository.Contains(u => u.Login == name);
		}

		private User MapEmbeddedEntities(UserDto input, User result)
		{
			if (input.RolesInput.Count != 0)
			{
				result.Roles = new List<Role>();
				input.RolesInput.ForEach(n => result.Roles.Add(_roleRepository.GetSingle(r => r.Name == n)));
			}

			return result;
		}

		private void AddDefaultRoleInput(UserDto user)
		{
			if (user.RolesInput.Count == 0)
			{
				user.RolesInput.Add(DefaultRoleName);
			}
		}
	}
}