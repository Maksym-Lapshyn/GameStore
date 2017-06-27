using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
