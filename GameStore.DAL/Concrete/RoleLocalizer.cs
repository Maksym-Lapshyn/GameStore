using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class RoleLocalizer : ILocalizer<Role>
	{
		private const string DefaultLanguage = "en";

		public Role Localize(Role entity, string language)
		{
			var genreLocale = entity.RoleLocales.FirstOrDefault(l => l.Language.Name == language);
			entity.Name = genreLocale != null ? genreLocale.Name : entity.RoleLocales.First(l => l.Language.Name == DefaultLanguage).Name;

			return entity;
		}
	}
}
