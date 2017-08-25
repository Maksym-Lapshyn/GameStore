﻿using GameStore.Common.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfGenreRepository
	{
		IQueryable<Genre> GetAll(string language, Expression<Func<Genre, bool>> predicate = null);

		Genre GetSingle(string language, Expression<Func<Genre, bool>> predicate);

		bool Contains(Expression<Func<Genre, bool>> predicate);

		void Insert(Genre genre);

		void Update(Genre genre);

		void Delete(string name);
	}
}