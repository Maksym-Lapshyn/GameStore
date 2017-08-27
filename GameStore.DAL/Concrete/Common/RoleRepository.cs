using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
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
        private readonly IOutputLocalizer<Role> _localizer;

		public RoleRepository(IEfRoleRepository efRepository,
            IOutputLocalizer<Role> localizer)
		{
			_efRepository = efRepository;
            _localizer = localizer;
		}

		public Role GetSingle(string language, Expression<Func<Role, bool>> predicate)
		{
			var role = _efRepository.GetSingle(predicate);

            return _localizer.Localize(language, role);
		}

		public IEnumerable<Role> GetAll(string language, Expression<Func<Role, bool>> predicate = null)
		{
			var totalList = _efRepository.GetAll(predicate).ToList();

            for (var i = 0; i < totalList.Count; i++)
            {
                totalList[i] = _localizer.Localize(language, totalList[i]);
            }

            return totalList;
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