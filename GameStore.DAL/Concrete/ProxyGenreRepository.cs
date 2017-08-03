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
			var efList = _efRepository.Get().ToList();
			var mongoList = _mongoRepository.Get().ToList();
			mongoList.RemoveAll(mongoGenre => efList.Any(efGenre => efGenre.Name == mongoGenre.Name));//Removes duplicates

			return efList.Union(mongoList).AsQueryable();
		}

		public Genre Get(string name)
		{
			return _efRepository.Contains(name) ? _efRepository.Get(name) : _mongoRepository.Get(name);
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