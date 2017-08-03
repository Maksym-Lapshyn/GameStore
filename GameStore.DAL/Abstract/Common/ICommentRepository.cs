using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Common
{
	public interface ICommentRepository
	{
		void Insert(Comment comment);

		Comment Get(int commentId);

		IEnumerable<Comment> Get();

		void Update(Comment comment);
	}
}
