using GameStore.Common.Entities;
using GameStore.Services.Abstract;

namespace GameStore.Services.Concrete
{
	public class UserOutputLocalizer : IOutputLocalizer<User>
	{
		private readonly IOutputLocalizer<Role> _roleLocalizer;

		public UserOutputLocalizer(IOutputLocalizer<Role> roleLocalizer)
		{
			_roleLocalizer = roleLocalizer;
		}

		public User Localize(string language, User entity)
		{
			if (entity.Roles.Count == 0)
			{
				return entity;
			}

			foreach (var role in entity.Roles)
			{
				_roleLocalizer.Localize(language, role);
			}

			return entity;;
		}
	}
}