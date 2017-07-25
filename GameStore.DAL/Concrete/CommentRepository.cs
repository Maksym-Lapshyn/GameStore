using GameStore.DAL.Abstract;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using System;
using System.Data.Entity;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class CommentRepository : IGenericRepository<Comment>
	{
		private readonly GameStoreContext _context;
		private readonly DbSet<Comment> _dbSet;

		public CommentRepository(GameStoreContext context)
		{
			_context = context;
			_dbSet = _context.Comments;
		}

		public IQueryable<Comment> GetAll()
		{
			return _dbSet;
		}

		public Comment Get(string id)
		{
			return _dbSet.Find(id);
		}

		public void Insert(Comment entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(string id)
		{
			throw new NotImplementedException();
		}

		public void Update(Comment entityToUpdate)
		{
			throw new NotImplementedException();
		}
	}
}