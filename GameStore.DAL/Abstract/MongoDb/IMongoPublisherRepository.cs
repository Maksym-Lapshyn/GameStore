using GameStore.Common.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.MongoDb
{
	public interface IMongoPublisherRepository
	{
		IQueryable<Publisher> GetAll(Expression<Func<Publisher, bool>> predicate = null);

		Publisher GetSingle(Expression<Func<Publisher, bool>> predicate);

		bool Contains(Expression<Func<Publisher, bool>> predicate);
	}
}