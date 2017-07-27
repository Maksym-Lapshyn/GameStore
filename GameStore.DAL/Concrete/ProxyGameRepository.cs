using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;
using System.Linq;

namespace GameStore.DAL.Concrete
{
	public class ProxyGameRepository : IEfGameRepository
	{
		private readonly IEfGameRepository _efRepository;
		private readonly IMongoGameRepository _mongoRepository;
		private readonly IPipeline<IQueryable<Game>> _pipeline;
		private readonly IFilterMapper _filterMapper;

		public ProxyGameRepository(IPipeline<IQueryable<Game>> pipeline,
			IFilterMapper filterMapper,
			IEfGameRepository efRepository,
			IMongoGameRepository mongoRepository)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
			_pipeline = pipeline;
			_filterMapper = filterMapper;
		}

		public IQueryable<Game> Get(GameFilter filter = null)
		{
			var efQuery = _efRepository.Get();
			var mongoQuery = _mongoRepository.Get();

			if (filter != null)
			{
				_filterMapper.Map(filter).ForEach(f => _pipeline.Register(f));
				efQuery = _pipeline.Process(efQuery);
				mongoQuery = _pipeline.Process(mongoQuery);
			}

			return efQuery.ToList().Union(mongoQuery.ToList()).AsQueryable();
		}

		public Game Get(string gameKey)
		{
			return _efRepository.Contains(gameKey) ? _efRepository.Get(gameKey) : _mongoRepository.Get(gameKey);
		}

		public void Insert(Game game)
		{
			_efRepository.Insert(game);
		}

		public void Delete(string gameKey)
		{
			_efRepository.Delete(gameKey);
		}

		public void Update(Game game)
		{
			_efRepository.Update(game);
		}

		public bool Contains(string gameKey)
		{
			return _efRepository.Contains(gameKey);
		}
	}
}