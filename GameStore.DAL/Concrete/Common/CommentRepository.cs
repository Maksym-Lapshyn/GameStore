using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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

		public Comment GetSingle(Expression<Func<Comment, bool>> predicate)
		{
			return _efRepository.GetSingle(predicate);
		}

		public IEnumerable<Comment> GetAll(Expression<Func<Comment, bool>> predicate = null)
		{
			return _efRepository.GetAll(predicate);
		}

		public void Update(Comment comment)
		{
			_efRepository.Update(comment);
		}

		public void Delete(int id)
		{
			_efRepository.Delete(id);
		}

		public bool Contains(Expression<Func<Comment, bool>> predicate)
		{
			return _efRepository.Contains(predicate);
		}
	}
}