using GameStore.Common.Entities.Localization;

namespace GameStore.DAL.Abstract.Common
{
    public interface ILanguageRepository
    {
        Language GetSingleBy(string name);
    }
}