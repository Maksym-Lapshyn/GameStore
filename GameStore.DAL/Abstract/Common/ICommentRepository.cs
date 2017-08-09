using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Common
{
	public interface ICommentRepository
	{
		void Insert(Comment comment);

		Comment GetSingle(int commentId);

		IEnumerable<Comment> GetAll();

		void Update(Comment comment);
	}
}