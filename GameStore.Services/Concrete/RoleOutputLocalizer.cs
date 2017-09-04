using GameStore.Common.Entities;
using GameStore.Services.Abstract;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class RoleOutputLocalizer : IOutputLocalizer<Role>
	{
		public void Localize(string language, Role entity)
		{
			if (entity.RoleLocales.Count == 0)
			{
				return;
			}

			var roleLocale = entity.RoleLocales.FirstOrDefault(l => l.Language.Name == language) ?? entity.RoleLocales.First();
			entity.Name = roleLocale.Name;
		}
	}
}