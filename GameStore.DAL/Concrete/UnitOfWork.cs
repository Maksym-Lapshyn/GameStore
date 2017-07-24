using GameStore.DAL.Abstract;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using GameStore.DAL.Concrete.EntityFramework;
using System;

namespace GameStore.DAL.Concrete
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly GameStoreContext _context;
		private readonly Lazy<IGenericRepository<Game>> _gameRepository;
		private readonly Lazy<IGenericRepository<Comment>> _commentRepository;
		private readonly Lazy<IGenericRepository<Publisher>> _publisherRepository;
		private readonly Lazy<IGenericRepository<PlatformType>> _platformTypeRepository;
		private readonly Lazy<IGenericRepository<Genre>> _genreRepository;
		private readonly Lazy<IGenericRepository<Order>> _orderRepository;

		public UnitOfWork(GameStoreContext context)
		{
			_context = context;
			_gameRepository = new Lazy<IGenericRepository<Game>>(() => new GenericRepository<Game>(_context));
			_commentRepository = new Lazy<IGenericRepository<Comment>>(() => new GenericRepository<Comment>(_context));
			_publisherRepository = new Lazy<IGenericRepository<Publisher>>(() => new GenericRepository<Publisher>(_context));
			_platformTypeRepository = new Lazy<IGenericRepository<PlatformType>>(() => new GenericRepository<PlatformType>(_context));
			_genreRepository = new Lazy<IGenericRepository<Genre>>(() => new GenericRepository<Genre>(_context));
			_orderRepository = new Lazy<IGenericRepository<Order>>(() => new GenericRepository<Order>(_context));
		}

		public IGenericRepository<Game> GameGenericRepository => _gameRepository.Value;

		public IGenericRepository<Comment> CommentGenericRepository => _commentRepository.Value;

		public IGenericRepository<Publisher> PublisherGenericRepository => _publisherRepository.Value;

		public IGenericRepository<Genre> GenreGenericRepository => _genreRepository.Value;

		public IGenericRepository<PlatformType> PlatformTypeGenericRepository => _platformTypeRepository.Value;

		public IGenericRepository<Order> OrderGenericRepository => _orderRepository.Value;

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}