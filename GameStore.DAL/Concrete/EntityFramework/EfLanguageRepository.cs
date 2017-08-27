using System.Linq;
using GameStore.Common.Entities.Localization;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;

namespace GameStore.DAL.Concrete.EntityFramework
{
    public class EfLanguageRepository : IEfLanguageRepository
    {
        private readonly GameStoreContext _context;

        public EfLanguageRepository(GameStoreContext context)
        {
            _context = context;
        }

        public Language GetSingleBy(string name)
        {
            return _context.Languages.First(l => l.Name == name);
        }
    }
}