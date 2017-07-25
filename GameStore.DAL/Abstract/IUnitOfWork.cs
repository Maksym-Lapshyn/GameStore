using GameStore.DAL.Entities;

namespace GameStore.DAL.Abstract
{

	public interface IUnitOfWork
	{
		IRepository<Game> GameRepository { get; }

		IRepository<Comment> CommentRepository { get; }

		IRepository<Publisher> PublisherRepository { get; }

		IRepository<PlatformType> PlatformTypeRepository { get; }

		IRepository<Genre> GenreRepository { get; }

		IRepository<Order> OrderRepository { get; }

		void Save();
	}
}
