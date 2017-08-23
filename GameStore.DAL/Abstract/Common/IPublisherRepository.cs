using GameStore.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.Common
{
	public interface IPublisherRepository
	{
		Publisher GetSingle(Expression<Func<Publisher, bool>> predicate);

		IEnumerable<Publisher> GetAll(Expression<Func<Publisher, bool>> predicate = null);

		bool Contains(Expression<Func<Publisher, bool>> predicate);

		void Insert(Publisher publisher);

		void Update(Publisher publisher);

		void Delete(string companyName);
	}
}