using System;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Abstract
{

	public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Game> GameRepository { get; }

        IGenericRepository<Comment> CommentRepository { get; }

        void Save();
    }
}
