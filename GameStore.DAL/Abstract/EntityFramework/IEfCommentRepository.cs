using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfCommentRepository
	{
		void Insert(Comment comment);

		Comment GetSingle(int commentId);

		IQueryable<Comment> GetAll();

		void Update(Comment comment);
	}
}