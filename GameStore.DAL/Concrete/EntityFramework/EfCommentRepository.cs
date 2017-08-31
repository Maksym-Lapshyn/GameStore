using GameStore.Common.Entities;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfCommentRepository : IEfCommentRepository
	{
		private readonly GameStoreContext _context;

		public EfCommentRepository(GameStoreContext context)
		{
			_context = context;
		}

		public void Insert(Comment comment)
		{
			_context.Comments.Add(comment);
		}

		public Comment GetSingle(Expression<Func<Comment, bool>> predicate)
		{
			return _context.Comments.First(predicate);
		}

		public IQueryable<Comment> GetAll(Expression<Func<Comment, bool>> predicate = null)
		{
			return predicate != null ? _context.Comments.Where(predicate) : _context.Comments;
		}

		public void Update(Comment comment)
		{
			_context.Entry(comment).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			var comment = _context.Comments.First(c => c.Id == id);
			comment.IsDeleted = true;
			_context.Entry(comment).State = EntityState.Modified;
		}

		public bool Contains(Expression<Func<Comment, bool>> predicate)
		{
			return _context.Comments.Any(predicate);
		}
	}
}