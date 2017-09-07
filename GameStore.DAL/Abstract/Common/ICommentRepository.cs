﻿using GameStore.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.Common
{
	public interface ICommentRepository
	{
		void Insert(Comment comment);

		Comment GetSingleOrDefault(Expression<Func<Comment, bool>> predicate);

		Comment GetSingle(Expression<Func<Comment, bool>> predicate);

		IEnumerable<Comment> GetAll(Expression<Func<Comment, bool>> predicate = null);

		bool Contains(Expression<Func<Comment, bool>> predicate);

		void Update(Comment comment);

		void Delete(int id);
	}
}