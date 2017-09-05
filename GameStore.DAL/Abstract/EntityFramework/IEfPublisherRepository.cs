using GameStore.Common.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfPublisherRepository
	{
		Publisher GetSingle(Expression<Func<Publisher, bool>> predicate);

		Publisher GetSingleOrDefault(Expression<Func<Publisher, bool>> predicate);

		IQueryable<Publisher> GetAll(Expression<Func<Publisher, bool>> predicate = null);

		bool Contains(Expression<Func<Publisher, bool>> predicate);

		void Insert(Publisher publisher);

		void Update(Publisher publisher);

		void Delete(string companyName);
	}
}