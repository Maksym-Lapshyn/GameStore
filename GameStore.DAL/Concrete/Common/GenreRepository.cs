using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure.Comparers;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Concrete.Common
{
	public class GenreRepository : IGenreRepository
	{
		private readonly IEfGenreRepository _efRepository;
		private readonly IMongoGenreRepository _mongoRepository;
		private readonly ICloner<Genre> _cloner;

		public GenreRepository(IEfGenreRepository efRepository,
			IMongoGenreRepository mongoRepository,
			ICloner<Genre> cloner)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
			_cloner = cloner;
		}

		public IEnumerable<Genre> GetAll()
		{
			var efList = _efRepository.GetAll().ToList();
			var mongoList = _mongoRepository.GetAll().ToList();

			return efList.Union(mongoList, new GenreEqualityComparer());
		}

		public Genre GetSingle(string name)
		{
			return !_efRepository.Contains(name) ? _cloner.Clone(_mongoRepository.GetSingle(name)) : _efRepository.GetSingle(name);
		}

		public bool Contains(string name)
		{
			return _efRepository.Contains(name);
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