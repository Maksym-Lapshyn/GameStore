using GameStore.Common.Entities;
using GameStore.Common.Infrastructure.Comparers;
using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.Common
{
	public class GenreRepository : IGenreRepository
	{
		private readonly IEfGenreRepository _efRepository;
		private readonly IMongoGenreRepository _mongoRepository;
		private readonly ICopier<Genre> _copier;

		public GenreRepository(IEfGenreRepository efRepository,
			IMongoGenreRepository mongoRepository,
			ICopier<Genre> copier)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
			_copier = copier;
		}

		public IEnumerable<Genre> GetAll(Expression<Func<Genre, bool>> predicate = null)
		{
			var efList = _efRepository.GetAll(predicate).ToList();
			var mongoList = _mongoRepository.GetAll(predicate).ToList();
			return efList.Union(mongoList, new GenreEqualityComparer());
		}

		public Genre GetSingle(Expression<Func<Genre, bool>> predicate)
		{
			return !_efRepository.Contains(predicate) ? _copier.Copy(_mongoRepository.GetSingle(predicate)) : _efRepository.GetSingle(predicate);
		}

		public bool Contains(Expression<Func<Genre, bool>> predicate)
		{
			return _efRepository.Contains(predicate);
		}

		public void Insert(Genre genre)
		{
			_efRepository.Insert(genre);
		}

		public void Update(Genre genre)
		{
			_efRepository.Update(genre);
		}

		public void Delete(string name)
		{
			_efRepository.Delete(name);
		}
	}
}