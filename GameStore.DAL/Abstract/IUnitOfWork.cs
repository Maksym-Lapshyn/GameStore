using GameStore.DAL.Entities;

namespace GameStore.DAL.Abstract
{

	public interface IUnitOfWork
	{
		IGenericRepository<Game> GameGenericRepository { get; }

		IGenericRepository<Comment> CommentGenericRepository { get; }

		IGenericRepository<Publisher> PublisherGenericRepository { get; }

		IGenericRepository<PlatformType> PlatformTypeGenericRepository { get; }

		IGenericRepository<Genre> GenreGenericRepository { get; }

		IGenericRepository<Order> OrderGenericRepository { get; }

		void Save();
	}
}
