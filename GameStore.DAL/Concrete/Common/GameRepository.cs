using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;

namespace GameStore.DAL.Concrete.Common
{
	public class GameRepository : IGameRepository
	{
		private readonly IEfGameRepository _efRepository;
		private readonly IMongoGameRepository _mongoRepository;
		private readonly IPipeline<IQueryable<Game>> _pipeline;
		private readonly IFilterMapper _filterMapper;
		private readonly ISynchronizer<Game> _synchronizer;
		private readonly ICloner<Game> _cloner;

		public GameRepository(IPipeline<IQueryable<Game>> pipeline,
			IFilterMapper filterMapper,
			IEfGameRepository efRepository,
			IMongoGameRepository mongoRepository,
			ISynchronizer<Game> synchronizer,
			ICloner<Game> cloner)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
			_pipeline = pipeline;
			_filterMapper = filterMapper;
			_synchronizer = synchronizer;
			_cloner = cloner;
		}

		public IEnumerable<Game> Get(GameFilter filter = null)
		{
			var efQuery = _efRepository.Get();
			var mongoQuery = _mongoRepository.Get();

			if (filter != null)
			{
				_filterMapper.Map(filter).ForEach(f => _pipeline.Register(f));
				efQuery = _pipeline.Process(efQuery);
				mongoQuery = _pipeline.Process(mongoQuery);
			}

			var efList = efQuery.ToList();

			for (var i = 0; i < efList.Count; i++)
			{
				efList[i] = _synchronizer.Synchronize(efList[i]);
			}

			var northwindIds = efList.Select(p => p.NorthwindId);
			var mongoList = mongoQuery.Where(g => !northwindIds.Contains(g.NorthwindId));

			return efList.Union(mongoList);
		}

		public Game Get(string gameKey)
		{
			return !_efRepository.Contains(gameKey) ? _cloner.Clone(_mongoRepository.Get(gameKey)) : _synchronizer.Synchronize(_efRepository.Get(gameKey));
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