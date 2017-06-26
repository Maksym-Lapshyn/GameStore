using System;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.DAL.Context;

namespace GameStore.DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreContext _context;

        private IGenericRepository<Game> _gameRepository;
        private IGenericRepository<Comment> _commentRepository;

        public UnitOfWork(string connectionString)
        {
            _context = new GameStoreContext(connectionString);
        }

        public IGenericRepository<Game> GameRepository
        {
            get
            {
                if (_gameRepository == null)
                {
                    _gameRepository = new GenericRepository<Game>(_context);
                }

                return _gameRepository;
            }
        }

        public IGenericRepository<Comment> CommentRepository
        {
            get
            {
                if (_commentRepository == null)
                {
                    _commentRepository = new GenericRepository<Comment>(_context);
                }

                return _commentRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
