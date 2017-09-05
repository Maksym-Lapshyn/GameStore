using GameStore.Common.Entities;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfGenreRepository : IEfGenreRepository
	{
		private readonly GameStoreContext _context;

		public EfGenreRepository(GameStoreContext context)
		{
			_context = context;
		}

		public IQueryable<Genre> GetAll(Expression<Func<Genre, bool>> predicate = null)
		{
			return predicate != null ? _context.Genres.Where(predicate) : _context.Genres;
		}

		public Genre GetSingle(Expression<Func<Genre, bool>> predicate)
		{
			return _context.Genres.First(predicate);
		}

		public bool Contains(Expression<Func<Genre, bool>> predicate)
		{
			return _context.Genres.Any(predicate);
		}

		public void Insert(Genre genre)
		{
			_context.Genres.Add(genre);
		}

		public void Update(Genre genre)
		{
			_context.Entry(genre).State = EntityState.Modified;
		}

		public void Delete(string name)
		{
			var genre = _context.Genres.First(g => g.Name == name);
			genre.IsDeleted = true;
			_context.Entry(genre).State = EntityState.Modified;
		}

		public Genre GetSingleOrDefault(Expression<Func<Genre, bool>> predicate)
		{
			return _context.Genres.FirstOrDefault(predicate);
		}
	}
}