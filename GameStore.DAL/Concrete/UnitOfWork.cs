using GameStore.DAL.Abstract;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
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

        public IGenericRepository<Game> GameRepository => _gameRepository.Value;

        public IGenericRepository<Comment> CommentRepository => _commentRepository.Value;

        public IGenericRepository<Publisher> PublisherRepository => _publisherRepository.Value;

        public IGenericRepository<Genre> GenreRepository => _genreRepository.Value;

        public IGenericRepository<PlatformType> PlatformTypeRepository => _platformTypeRepository.Value;

        public UnitOfWork(string connectionString)
        {
            _context = new GameStoreContext(connectionString);
            _gameRepository = new Lazy<IGenericRepository<Game>>(() => new GenericRepository<Game>(_context));
            _commentRepository = new Lazy<IGenericRepository<Comment>>(() => new GenericRepository<Comment>(_context));
            _publisherRepository = new Lazy<IGenericRepository<Publisher>>(() => new GenericRepository<Publisher>(_context));
            _platformTypeRepository = new Lazy<IGenericRepository<PlatformType>>(() => new GenericRepository<PlatformType>(_context));
            _genreRepository = new Lazy<IGenericRepository<Genre>>(() => new GenericRepository<Genre>(_context));
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
