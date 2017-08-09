using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Entities;
using GameStore.DAL.Infrastructure;
using GameStore.DAL.Infrastructure.Comparers;
using System.Collections.Generic;
using System.Linq;

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

		public IEnumerable<Game> GetAll(GameFilter filter = null, int? itemsToSkip = null, int? itemsToTake = null)
		{
			var efQuery = _efRepository.GetAll();
			var mongoQuery = _mongoRepository.GetAll();

			if (filter != null)
			{
				_filterMapper.Map(filter).ForEach(f => _pipeline.Register(f));
				efQuery = _pipeline.Process(efQuery);
				mongoQuery = _pipeline.Process(mongoQuery);
			}

			var totalQuery = efQuery.ToList().Union(mongoQuery.ToList(), new GameEqualityComparer());

			if (itemsToSkip != null && itemsToTake != null)
			{
				totalQuery = totalQuery.Skip(itemsToSkip.Value).Take(itemsToTake.Value);
			}

			var totalList = totalQuery.ToList();

			for (var i = 0; i < totalList.Count; i++)
			{
				totalList[i] = _synchronizer.Synchronize(totalList[i]);
			}

			return totalList;
		}

		public Game GetSingle(string gameKey)
		{
			return !_efRepository.Contains(gameKey) ? _cloner.Clone(_mongoRepository.GetSingle(gameKey)) : _synchronizer.Synchronize(_efRepository.GetSingle(gameKey));
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