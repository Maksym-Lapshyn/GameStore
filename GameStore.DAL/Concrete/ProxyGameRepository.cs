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
		private readonly ISynchronizer<Game> _synchronizer;

		public ProxyGameRepository(IPipeline<IQueryable<Game>> pipeline,
			IFilterMapper filterMapper,
			IEfGameRepository efRepository,
			IMongoGameRepository mongoRepository,
			ISynchronizer<Game> synchronizer)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
			_pipeline = pipeline;
			_filterMapper = filterMapper;
			_synchronizer = synchronizer;
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

			var efList = efQuery.ToList();

			for (var i = 0; i < efList.Count; i++)
			{
				efList[i] = _synchronizer.Synchronize(efList[i]);
			}

			var mongoList = mongoQuery.ToList();

			mongoList.RemoveAll(mongoGame => efList.Any(efGame => efGame.Key == mongoGame.Key));//Removes duplicates

			return efList.Union(mongoList).AsQueryable();
		}

		public Game Get(string gameKey)
		{
			if (!_efRepository.Contains(gameKey))
			{
				return _mongoRepository.Get(gameKey);
			}

			return _synchronizer.Synchronize(_efRepository.Get(gameKey));
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
			if (!_efRepository.Contains(game.Key))
			{
				_efRepository.Insert(game);
			}
			else
			{
				if (CheckIfGameWasChanged(game))
				{
					game.IsUpdated = true;
				}

				_efRepository.Update(game);
			}
		}

		public bool Contains(string gameKey)
		{
			return _efRepository.Contains(gameKey);
		}

		public bool CheckIfGameWasChanged(Game game)
		{
			var previousVersion = _efRepository.Get(game.Key);

			if (previousVersion.Description != game.Description
				|| previousVersion.Name != game.Name
				|| previousVersion.Price != game.Price
				|| previousVersion.UnitsInStock != game.UnitsInStock
				|| previousVersion.Discontinued != game.Discontinued)
			{
				return true;
			}

			return false;
		}
	}
}