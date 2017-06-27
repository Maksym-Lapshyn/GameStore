using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Abstract
{
	//TODO: Required: Blank line after each method/property
	public interface IUnitOfWork
    {
        IGenericRepository<Game> GameRepository { get; }
        IGenericRepository<Comment> CommentRepository { get; }
        void Save();

		//TODO: Required: Remove this method or inherit IDisposable
		void Dispose();
    }
}
