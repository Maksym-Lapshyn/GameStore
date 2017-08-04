using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;

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

		public IEnumerable<Genre> Get()
		{
			var efList = _efRepository.Get().ToList();
			var northwindIds = efList.Select(p => p.NorthwindId);
			var mongoList = _mongoRepository.Get().Where(g => !northwindIds.Contains(g.NorthwindId));

			return efList.Union(mongoList);
		}

		public Genre Get(string name)
		{
			return !_efRepository.Contains(name) ? _cloner.Clone(_mongoRepository.Get(name)) : _efRepository.Get(name);
		}

		public bool Contains(string name)
		{
			return _efRepository.Contains(name);
		}

		public void Insert(Genre genre)
		{
			_efRepository.Insert(genre);
		}
	}
}