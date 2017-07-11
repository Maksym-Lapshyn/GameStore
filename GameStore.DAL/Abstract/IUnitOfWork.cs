using GameStore.DAL.Entities;

namespace GameStore.DAL.Abstract
{

	public interface IUnitOfWork
	{
		IGenericRepository<Game> GameRepository { get; }

		IGenericRepository<Comment> CommentRepository { get; }

		IGenericRepository<Publisher> PublisherRepository { get; }

		IGenericRepository<PlatformType> PlatformTypeRepository { get; }

		IGenericRepository<Genre> GenreRepository { get; }

		IGenericRepository<Order> OrderRepository { get; }

		void Save();
	}
}
