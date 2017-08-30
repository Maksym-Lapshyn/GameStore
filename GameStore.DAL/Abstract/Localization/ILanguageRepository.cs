using GameStore.Common.Entities.Localization;

namespace GameStore.DAL.Abstract.Localization
{
	public interface ILanguageRepository
	{
		Language GetSingleBy(string name);
	}
}