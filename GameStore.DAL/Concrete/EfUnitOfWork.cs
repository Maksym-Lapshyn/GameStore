using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.DAL.Context;

namespace GameStore.DAL.Concrete
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private GameStoreContext _context;

        private IGenericRepository<Game> _gameRepository;
        private IGenericRepository<Comment> _commentRepository;

        public EfUnitOfWork(string connectionString)
        {
            _context = new GameStoreContext(connectionString);
        }

        public IGenericRepository<Game> GameRepository
        {
            get
            {
                if (_gameRepository == null)
                {
                    _gameRepository = new EFGenericRepository<Game>(_context);
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
                    _commentRepository = new EFGenericRepository<Comment>(_context);
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
