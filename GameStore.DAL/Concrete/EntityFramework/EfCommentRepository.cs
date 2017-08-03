using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using System.Data.Entity;
using System.Linq;

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

		public Comment Get(int commentId)
		{
			return _context.Comments.First(c => c.Id == commentId);
		}

		public IQueryable<Comment> Get()
		{
			return _context.Comments;
		}

		public void Update(Comment comment)
		{
			_context.Entry(comment).State = EntityState.Modified;
		}
	}
}