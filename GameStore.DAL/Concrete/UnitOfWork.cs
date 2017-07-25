using GameStore.DAL.Abstract;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using System;
using GameStore.DAL.Concrete.EntityFramework;
using GameStore.DAL.Concrete.MongoDb;
using MongoDB.Driver;

namespace GameStore.DAL.Concrete
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly GameStoreContext _context;
		private readonly IMongoDatabase _database;
		private readonly Lazy<IGenericRepository<Game>> _gameRepository;
		private readonly Lazy<IGenericRepository<Comment>> _commentRepository;
		private readonly Lazy<IGenericRepository<Publisher>> _publisherRepository;
		private readonly Lazy<IGenericRepository<PlatformType>> _platformTypeRepository;
		private readonly Lazy<IGenericRepository<Genre>> _genreRepository;
		private readonly Lazy<IGenericRepository<Order>> _orderRepository;
		private readonly Lazy<IGenericRepository<Shipper>> _shipperRepository;

		public UnitOfWork(GameStoreContext context, IMongoDatabase database)
		{
			_context = context;
			_database = database;
			_gameRepository = new Lazy<IGenericRepository<Game>>(() => new GameDecorator(_context, _database));
			_commentRepository = new Lazy<IGenericRepository<Comment>>(() => new EfGenericRepository<Comment>(_context));
			_publisherRepository = new Lazy<IGenericRepository<Publisher>>(() => new PublisherDecorator(_context, _database));
			_platformTypeRepository = new Lazy<IGenericRepository<PlatformType>>(() => new EfGenericRepository<PlatformType>(_context));
			_genreRepository = new Lazy<IGenericRepository<Genre>>(() => new GenreDecorator(_context, _database));
			_orderRepository = new Lazy<IGenericRepository<Order>>(() => new EfGenericRepository<Order>(_context));
			_shipperRepository = new Lazy<IGenericRepository<Shipper>>(() => new MongoShipperRepository(_database));
		}

		public IGenericRepository<Game> GameRepository => _gameRepository.Value;

		public IGenericRepository<Comment> CommentRepository => _commentRepository.Value;

		public IGenericRepository<Publisher> PublisherRepository => _publisherRepository.Value;

		public IGenericRepository<Genre> GenreRepository => _genreRepository.Value;

		public IGenericRepository<PlatformType> PlatformTypeRepository => _platformTypeRepository.Value;

		public IGenericRepository<Order> OrderRepository => _orderRepository.Value;

		public IGenericRepository<Shipper> ShipperRepository => _shipperRepository.Value;

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}