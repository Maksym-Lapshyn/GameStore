using GameStore.Common.Entities;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfPublisherRepository : IEfPublisherRepository
	{
		private readonly GameStoreContext _context;

		public EfPublisherRepository(GameStoreContext context)
		{
			_context = context;
		}

		public Publisher GetSingle(Expression<Func<Publisher, bool>> predicate)
		{
			return _context.Publishers.First(predicate);
		}

		public IQueryable<Publisher> GetAll(Expression<Func<Publisher, bool>> predicate = null)
		{
			return predicate != null ? _context.Publishers.Where(predicate) : _context.Publishers;
		}

		public bool Contains(Expression<Func<Publisher, bool>> predicate)
		{
			return _context.Publishers.Any(predicate);
		}

		public void Insert(Publisher publisher)
		{
			_context.Publishers.Add(publisher);
		}

		public void Update(Publisher publisher)
		{
			_context.Entry(publisher).State = EntityState.Modified;
		}

		public void Delete(string companyName)
		{
			var publisher = _context.Publishers.First(p => p.CompanyName == companyName);
			publisher.IsDeleted = true;

			_context.Entry(publisher).State = EntityState.Modified;
		}

		public Publisher GetSingleOrDefault(Expression<Func<Publisher, bool>> predicate)
		{
			return _context.Publishers.FirstOrDefault(predicate);
		}
	}
}