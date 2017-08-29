using GameStore.Common.Entities.Localization;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.Common;

namespace GameStore.DAL.Concrete.Common
{
	public class LanguageRepository : ILanguageRepository
	{
		private readonly IEfLanguageRepository _efRepository;

		public LanguageRepository(IEfLanguageRepository efRepository)
		{
			_efRepository = efRepository;
		}

		public Language GetSingleBy(string name)
		{
			return _efRepository.GetSingleBy(name);
		}
	}
}