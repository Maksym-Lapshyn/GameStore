using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Concrete.Common
{
	public class CommentRepository : ICommentRepository //TODO Consider: Remove useless wrapper
	{
		private readonly IEfCommentRepository _efRepository;

		public CommentRepository(IEfCommentRepository efRepository)
		{
			_efRepository = efRepository;
		}

		public void Insert(Comment comment)
		{
			_efRepository.Insert(comment);
		}

		public Comment GetSingle(int commentId)
		{
			return _efRepository.GetSingle(commentId);
		}

		public IEnumerable<Comment> GetAll()
		{
			return _efRepository.GetAll();
		}

		public void Update(Comment comment)
		{
			_efRepository.Update(comment);
		}
	}
}