﻿using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IUserService
	{
		void Create(UserDto userDto);

		UserDto GetSingle(string language, string name);

		UserDto GetSingleOrDefault(string language, string name);

		IEnumerable<UserDto> GetAll();

		void Update(UserDto userDto);

		void Delete(string name);

		bool Contains(string name);
	}
}