using AutoMapper;
using GameStore.Common.Abstract;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class UserService : IUserService
	{
		private const string DefaultRoleName = "User";

		private readonly IMapper _mapper;
		private readonly IOutputLocalizer<User> _userOutputLocalizer;
		private readonly IUserRepository _userRepository;
		private readonly IRoleRepository _roleRepository;
		private readonly IHashGenerator<string> _hashGenerator;
		private readonly IUnitOfWork _unitOfWork;

		public UserService(IMapper mapper,
			IOutputLocalizer<User> userOutputLocalizer,
			IUserRepository userRepository,
			IRoleRepository roleRepository,
			IHashGenerator<string> hashGenerator,
			IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_userOutputLocalizer = userOutputLocalizer;
			_userRepository = userRepository;
			_roleRepository = roleRepository;
			_hashGenerator = hashGenerator;
			_unitOfWork = unitOfWork;
		}

		public void Create(UserDto userDto)
		{
			AddDefaultRoleInput(userDto);
			var user = _mapper.Map<UserDto, User>(userDto);
			user = MapEmbeddedEntities(userDto, user);
			user.Password = _hashGenerator.Generate(user.Password);
			_userRepository.Create(user);
			_unitOfWork.Save();
		}

		public UserDto GetSingle(string language, string name)
		{
			var user = _userRepository.GetSingle(u => u.Login == name);
			user = _userOutputLocalizer.Localize(language, user);
			var userDto = _mapper.Map<User, UserDto>(user);

			return userDto;
		}

		public IEnumerable<UserDto> GetAll()
		{
			var users = _userRepository.GetAll().ToList();

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
				input.RolesInput.ForEach(n => result.Roles.Add(_roleRepository.GetSingle(r => r.RoleLocales.Any(l => l.Name == n))));
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