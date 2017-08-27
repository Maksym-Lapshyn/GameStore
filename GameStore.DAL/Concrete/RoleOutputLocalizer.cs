using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class RoleOutputLocalizer : IOutputLocalizer<Role>
	{
		private const string DefaultLanguage = "en";

		public Role Localize(string language, Role entity)
		{
            if (entity.RoleLocales.Count != 0)
            {
                var roleLocale = entity.RoleLocales.FirstOrDefault(l => l.Language.Name == language);
                entity.Name = roleLocale != null ? roleLocale.Name : entity.RoleLocales.First(l => l.Language.Name == DefaultLanguage).Name;
            }

			return entity;
		}
	}
}
