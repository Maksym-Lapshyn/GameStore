using GameStore.Common.Entities;
using System;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IService<T> where T : BaseEntity
	{
		void Create(T dto);

		T GetSingle(Func<T, bool> predicate = null);

		IEnumerable<T> GetAll(Func<T, bool> predicate = null);

		void Update(T userDto);

		void Delete(string name);
	}
}