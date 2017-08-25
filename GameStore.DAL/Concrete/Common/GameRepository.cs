using GameStore.Common.Entities;
using GameStore.Common.Infrastructure.Comparers;
using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete.Common
{
	public class GameRepository : IGameRepository
	{
		private readonly IEfGameRepository _efRepository;
		private readonly IMongoGameRepository _mongoRepository;
		private readonly IPipeline<IQueryable<Game>> _pipeline;
		private readonly IFilterMapper _filterMapper;
		private readonly ISynchronizer<Game> _synchronizer;
		private readonly ICopier<Game> _copier;

		public GameRepository(IPipeline<IQueryable<Game>> pipeline,
			IFilterMapper filterMapper,
			IEfGameRepository efRepository,
			IMongoGameRepository mongoRepository,
			ISynchronizer<Game> synchronizer,
			ICopier<Game> copier)
		{
			_efRepository = efRepository;
			_mongoRepository = mongoRepository;
			_pipeline = pipeline;
			_filterMapper = filterMapper;
			_synchronizer = synchronizer;
			_copier = copier;
		}

		public IEnumerable<Game> GetAll(string language, GameFilter filter = null, int? itemsToSkip = null, int? itemsToTake = null, Expression<Func<Game, bool>> predicate = null)
		{
			var efQuery = _efRepository.GetAll(predicate, language);
			var mongoQuery = _mongoRepository.GetAll(predicate);

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

		public Game GetSingle(Expression<Func<Game, bool>> predicate, string language)
		{
			return !_efRepository.Contains(predicate) ? _copier.Copy(_mongoRepository.GetSingle(predicate)) : _synchronizer.Synchronize(_efRepository.GetSingle(predicate, language));
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

		public bool Contains(Expression<Func<Game, bool>> predicate)
		{
			return !_efRepository.Contains(predicate) ? _efRepository.Contains(predicate) : _mongoRepository.Contains(predicate);
		}
	}
}