using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Concrete
{
	public class UserService : IUserService
	{
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
		private readonly IUnitOfWork _unitOfWork;

		public UserService(IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_userRepository = userRepository;
			_unitOfWork = unitOfWork;
		}

		public void Create(UserDto userDto)
		{
			var user = _mapper.Map<UserDto, User>(userDto);
			_userRepository.Create(user);
			_unitOfWork.Save();
		}

		public UserDto GetSingle(string name)
		{
			var user = _userRepository.GetSingle(name);
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
			var user = _userRepository.GetSingle(userDto.Name);
			user = _mapper.Map(userDto, user);
			_userRepository.Update(user);
			_unitOfWork.Save();
		}

		public void Delete(string name)
		{
			var user = _userRepository.GetSingle(name);
			user.IsDeleted = true;
			_userRepository.Update(user);
			_unitOfWork.Save();
		}
	}
}