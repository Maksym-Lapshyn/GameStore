using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfGenreRepository : IEfGenreRepository
	{
		private readonly GameStoreContext _context;

		public EfGenreRepository(GameStoreContext context)
		{
			_context = context;
		}

		public IQueryable<Genre> GetAll()
		{
			return _context.Genres;
		}

		public Genre GetSingle(string name)
		{
			return _context.Genres.First(g => g.Name == name);
		}

		public bool Contains(string name)
		{
			return _context.Genres.Any(g => g.Name == name);
		}

		public void Insert(Genre genre)
		{
			_context.Genres.Add(genre);
		}
	}
}