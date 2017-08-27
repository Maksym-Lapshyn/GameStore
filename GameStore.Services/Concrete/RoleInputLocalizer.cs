using GameStore.Services.Abstract;
using GameStore.Common.Entities;
using GameStore.Common.Entities.Localization;
using GameStore.DAL.Abstract.Common;
using System.Linq;

namespace GameStore.Services.Concrete
{
    public class RoleInputLocalizer : IInputLocalizer<Role>
    {
        private readonly ILanguageRepository _languageRepository;

        public RoleInputLocalizer(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public Role Localize(string language, Role entity)
        {
            var roleLocale = entity.RoleLocales.FirstOrDefault(l => l.Language.Name == language);

            if (roleLocale != null)
            {
                roleLocale.Name = entity.Name;
            }
            else
            {
                entity.RoleLocales.Add(new RoleLocale
                {
                    Name = entity.Name,
                    Language = _languageRepository.GetSingleBy(language)
                });
            }

            return entity;
        }
    }
}
