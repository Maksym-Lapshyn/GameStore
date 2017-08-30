using GameStore.Common.Entities.Localization;
using GameStore.DAL.Abstract.Localization;
using GameStore.DAL.Context;
using System.Linq;

namespace GameStore.DAL.Concrete.Localization
{
	public class LanguageRepository : ILanguageRepository
	{
		private readonly GameStoreContext _context;

		public LanguageRepository(GameStoreContext context)
		{
			_context = context;
		}

		public Language GetSingleBy(string name)
		{
			return _context.Languages.First(l => l.Name == name);
		}
	}
}