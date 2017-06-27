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
	//TODO: Required: Remove 'Ef' prefix
	public class EfUnitOfWork : IUnitOfWork
    {
		//TODO: Consider: make fields readonly
		private GameStoreContext _context;

        private IGenericRepository<Game> _gameRepository;
        private IGenericRepository<Comment> _commentRepository;

        public EfUnitOfWork(string connectionString)
        {
            _context = new GameStoreContext(connectionString);
        }

		//TODO: Suggestion: Use Lazy<T> in getter
		public IGenericRepository<Game> GameRepository
        {
            get
            {
                if (_gameRepository == null) //TODO: Required: Convert to ?? expression
                {
                    _gameRepository = new EFGenericRepository<Game>(_context);
                }

                return _gameRepository;
            }
        }

		//TODO: Suggestion: Use Lazy<T> in getter
		public IGenericRepository<Comment> CommentRepository
        {
            get
            {
                if (_commentRepository == null) //TODO: Required: Convert to ?? expression
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
		//TODO: Required: Remove all code bleow
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
