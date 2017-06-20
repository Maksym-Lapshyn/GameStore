using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Abstract;
using GameStore.Domain.Entities;
using GameStore.DAL.Context;

namespace GameStore.DAL.Concrete
{
    public class EfUnitOfWork
    {
        private GameStoreContext _context = new GameStoreContext();

        private IGenericRepository<Game> _gameRepository;
        private IGenericRepository<Comment> _commentRepository;
        private IGenericRepository<Genre> _genreRepository;

        public EfUnitOfWork(IGenericRepository<Game> gameRepository, IGenericRepository<Comment> commentRepository,
            IGenericRepository<Genre> genreRepository)
        {
            _gameRepository = gameRepository;
            _commentRepository = commentRepository;
            _genreRepository = genreRepository;
        }

        public IGenericRepository<Game> GameRepository
        {
            get { return _gameRepository; }
        }

        public IGenericRepository<Comment> CommentRepository
        {
            get { return _commentRepository; }
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
