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
        private readonly IOutputLocalizer<Genre> _localizer;

		public GenreRepository(IEfGenreRepository efRepository,
			IMongoGenreRepository mongoRepository,
			ICopier<Genre> copier,
            IOutputLocalizer<Genre> localizer)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
			_copier = copier;
            _localizer = localizer;
		}

		public IEnumerable<Genre> GetAll(string language, Expression<Func<Genre, bool>> predicate = null)
		{
			var efList = _efRepository.GetAll(predicate).ToList();
			var mongoList = _mongoRepository.GetAll(predicate).ToList();
			var totalList = efList.Union(mongoList, new GenreEqualityComparer()).ToList();

            for (var i = 0; i < totalList.Count; i++)
            {
                totalList[i] = _localizer.Localize(language, totalList[i]);
            }

            return totalList;
        }

		public Genre GetSingle(string language, Expression<Func<Genre, bool>> predicate)
		{
			var genre = !_efRepository.Contains(predicate) ? _copier.Copy(_mongoRepository.GetSingle(predicate)) : _efRepository.GetSingle(predicate);

            return _localizer.Localize(language, genre);
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