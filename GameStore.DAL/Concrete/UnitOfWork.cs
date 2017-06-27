using System;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.DAL.Context;

namespace GameStore.DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreContext _context;
    
        private readonly Lazy<IGenericRepository<Game>> _gameRepository = new Lazy<IGenericRepository<Game>>();
        private readonly Lazy<IGenericRepository<Comment>> _commentRepository = new Lazy<IGenericRepository<Comment>>();

        public UnitOfWork(string connectionString)
        {
            _context = new GameStoreContext(connectionString);
        }

		//TODO: Suggestion: Use Lazy<T> in getter
		public IGenericRepository<Game> GameRepository
        {
            get { return _gameRepository.Value; }
        }

		//TODO: Suggestion: Use Lazy<T> in getter
		public IGenericRepository<Comment> CommentRepository
        {
            get { return _commentRepository.Value; }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
