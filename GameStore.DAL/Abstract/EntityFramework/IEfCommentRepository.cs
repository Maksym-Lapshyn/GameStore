using GameStore.Common.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfCommentRepository
	{
		void Insert(Comment comment);

		Comment GetSingle(Expression<Func<Comment, bool>> predicate);

		IQueryable<Comment> GetAll(Expression<Func<Comment, bool>> predicate = null);

		void Update(Comment comment);
	}
}