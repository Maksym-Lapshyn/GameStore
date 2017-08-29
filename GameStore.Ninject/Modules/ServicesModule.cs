using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Concrete.Common;
using GameStore.Services.Abstract;
using GameStore.Services.Concrete;
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
			Bind<IUserRepository>().To<UserRepository>();
			Bind<IRoleRepository>().To<RoleRepository>();
			Bind<ILanguageRepository>().To<LanguageRepository>();
			Bind<IUnitOfWork>().To<UnitOfWork>();

			Bind<IInputLocalizer<Game>>().To<GameInputLocalizer>();
			Bind<IInputLocalizer<Genre>>().To<GenreInputLocalizer>();
			Bind<IInputLocalizer<Publisher>>().To<PublisherInputLocalizer>();
			Bind<IInputLocalizer<Role>>().To<RoleInputLocalizer>();
		}
	}
}