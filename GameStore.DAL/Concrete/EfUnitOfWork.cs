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
    //TODO: Required: Remove 'Ef' prefix Fixed in ML_2
    public class EfUnitOfWork : IUnitOfWork
    {
        //TODO: Consider: make fields readonly Fixed in ML_2
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
            get { return _gameRepository ?? (_gameRepository = new EFGenericRepository<Game>(_context)); }
        }

		//TODO: Suggestion: Use Lazy<T> in getter
		public IGenericRepository<Comment> CommentRepository
        {
            get { return _commentRepository ?? (_commentRepository = new EFGenericRepository<Comment>(_context)); }
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
