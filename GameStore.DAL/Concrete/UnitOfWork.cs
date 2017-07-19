using GameStore.DAL.Abstract;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using System;

namespace GameStore.DAL.Concrete
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly GameStoreContext _context;
		private readonly Lazy<IRepository<Game>> _gameRepository;
		private readonly Lazy<IRepository<Comment>> _commentRepository;
		private readonly Lazy<IRepository<Publisher>> _publisherRepository;
		private readonly Lazy<IRepository<PlatformType>> _platformTypeRepository;
		private readonly Lazy<IRepository<Genre>> _genreRepository;
		private readonly Lazy<IRepository<Order>> _orderRepository;

		public UnitOfWork(GameStoreContext context) //TODO Required: Move to top
		{
			_context = context;
			_gameRepository = new Lazy<IRepository<Game>>(() => new GenericRepository<Game>(_context));
			_commentRepository = new Lazy<IRepository<Comment>>(() => new GenericRepository<Comment>(_context));
			_publisherRepository = new Lazy<IRepository<Publisher>>(() => new GenericRepository<Publisher>(_context));
			_platformTypeRepository = new Lazy<IRepository<PlatformType>>(() => new GenericRepository<PlatformType>(_context));
			_genreRepository = new Lazy<IRepository<Genre>>(() => new GenericRepository<Genre>(_context));
			_orderRepository = new Lazy<IRepository<Order>>(() => new GenericRepository<Order>(_context));
		}

		public IRepository<Game> GameRepository => _gameRepository.Value;

		public IRepository<Comment> CommentRepository => _commentRepository.Value;

		public IRepository<Publisher> PublisherRepository => _publisherRepository.Value;

		public IRepository<Genre> GenreRepository => _genreRepository.Value;

		public IRepository<PlatformType> PlatformTypeRepository => _platformTypeRepository.Value;

		public IRepository<Order> OrderRepository => _orderRepository.Value;

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
