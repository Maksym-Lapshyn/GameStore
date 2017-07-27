using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class ProxyGenreRepository : IEfGenreRepository
	{
		private readonly IEfGenreRepository _efRepository;
		private readonly IMongoGenreRepository _mongoRepository;

		public ProxyGenreRepository(IEfGenreRepository efRepository, IMongoGenreRepository mongoRepository)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
		}

		public IQueryable<Genre> Get()
		{
			var efQuery = _efRepository.Get().ToList();
			var mongoQuery = _mongoRepository.Get().ToList();

			return efQuery.Union(mongoQuery).AsQueryable();
		}

		public Genre Get(string name)
		{
			return _efRepository.Contains(name) ? _efRepository.Get(name) : _mongoRepository.Get(name);
		}

		public bool Contains(string name)
		{
			return _efRepository.Contains(name);
		}
	}
}