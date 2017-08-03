using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfCommentRepository
	{
		void Insert(Comment comment);

		Comment Get(int commentId);

		IQueryable<Comment> Get();

		void Update(Comment comment);
	}
}