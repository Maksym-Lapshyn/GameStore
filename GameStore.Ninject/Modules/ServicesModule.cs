using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Concrete.Common;
using Ninject.Modules;

namespace GameStore.Ninject.Modules
{
	public class ServicesModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IGameRepository>().To<GameRepository>();
			Bind<IGenreRepository>().To<GenreRepository>();
			Bind<IPublisherRepository>().To<PublisherRepository>();
			Bind<IOrderRepository>().To<OrderRepository>();
			Bind<IPlatformTypeRepository>().To<PlatformTypeRepository>();
			Bind<ICommentRepository>().To<CommentRepository>();
			Bind<IUnitOfWork>().To<UnitOfWork>();
		}
	}
}