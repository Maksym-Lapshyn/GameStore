using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Concrete.Common
{
	public class RoleRepository : IRoleRepository
	{
		private readonly IEfRoleRepository _efRepository;

		public RoleRepository(IEfRoleRepository efRepository)
		{
			_efRepository = efRepository;
		}

		public Role GetSingle(string name)
		{
			return _efRepository.GetSingle(name);
		}

		public IEnumerable<Role> GetAll()
		{
			return _efRepository.GetAll().ToList();
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
	}
}