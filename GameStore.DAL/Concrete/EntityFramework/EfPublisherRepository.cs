using System.Data.Entity;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfPublisherRepository : IEfPublisherRepository
	{
		private readonly GameStoreContext _context;

		public EfPublisherRepository(GameStoreContext context)
		{
			_context = context;
		}

		public Publisher GetSingle(string companyName)
		{
			return _context.Publishers.First(p => p.CompanyName == companyName);
		}

		public IQueryable<Publisher> GetAll()
		{
			return _context.Publishers;
		}

		public bool Contains(string companyName)
		{
			return _context.Publishers.Any(p => p.CompanyName == companyName);
		}

		public void Insert(Publisher publisher)
		{
			_context.Publishers.Add(publisher);
		}

		public void Update(Publisher publisher)
		{
			_context.Entry(publisher).State = EntityState.Modified;
		}
	}
}