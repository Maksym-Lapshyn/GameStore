using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Concrete.Common
{
	public class CommentRepository : ICommentRepository
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

		public Comment Get(int commentId)
		{
			return _efRepository.Get(commentId);
		}

		public IEnumerable<Comment> Get()
		{
			return _efRepository.Get();
		}

		public void Update(Comment comment)
		{
			_efRepository.Update(comment);
		}
	}
}