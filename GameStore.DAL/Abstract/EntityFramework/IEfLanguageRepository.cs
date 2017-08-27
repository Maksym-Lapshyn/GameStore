using GameStore.Common.Entities.Localization;

namespace GameStore.DAL.Abstract.EntityFramework
{
    public interface IEfLanguageRepository
    {
        Language GetSingleBy(string name);
    }
}