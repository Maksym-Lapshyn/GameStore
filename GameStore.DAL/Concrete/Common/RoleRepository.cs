using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.Common
{
	public class RoleRepository : IRoleRepository
	{
		private readonly IEfRoleRepository _efRepository;

		public RoleRepository(IEfRoleRepository efRepository)
		{
			_efRepository = efRepository;
		}

		public Role GetSingle(Expression<Func<Role, bool>> predicate)
		{
			return _efRepository.GetSingle(predicate);
		}

		public IEnumerable<Role> GetAll(Expression<Func<Role, bool>> predicate = null)
		{
			return _efRepository.GetAll(predicate).ToList();
		}

		public void Update(Role role)
		{
			_efRepository.Update(role);
		}

		public void Create(Role role)
		{
			_efRepository.Create(role);
		}

		public void Delete(string name)
		{
			_efRepository.Delete(name);
		}

		public bool Contains(Expression<Func<Role, bool>> predicate)
		{
			return _efRepository.Contains(predicate);
		}
	}
}