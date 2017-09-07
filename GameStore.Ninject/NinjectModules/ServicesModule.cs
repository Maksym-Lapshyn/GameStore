using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.Localization;
using GameStore.DAL.Concrete.Common;
using GameStore.DAL.Concrete.Localization;
using GameStore.Services.Abstract;
using GameStore.Services.Concrete;
using Ninject.Modules;

namespace GameStore.DI.NinjectModules
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
			Bind<IUnitOfWork>().To<UnitOfWork>();

			Bind<ILanguageRepository>().To<LanguageRepository>();
			Bind<IGameLocaleRepository>().To<GameLocaleRepository>();
			Bind<IRoleLocaleRepository>().To<RoleLocaleRepository>();

			Bind<IInputLocalizer<Game>>().To<GameInputLocalizer>();
			Bind<IInputLocalizer<Genre>>().To<GenreInputLocalizer>();
			Bind<IInputLocalizer<Publisher>>().To<PublisherInputLocalizer>();
			Bind<IInputLocalizer<Role>>().To<RoleInputLocalizer>();

			Bind<IOutputLocalizer<Game>>().To<GameOutputLocalizer>();
			Bind<IOutputLocalizer<Genre>>().To<GenreOutputLocalizer>();
			Bind<IOutputLocalizer<Publisher>>().To<PublisherOutputLocalizer>();
			Bind<IOutputLocalizer<Role>>().To<RoleOutputLocalizer>();
			Bind<IOutputLocalizer<PlatformType>>().To<PlatformTypeOutputLocalizer>();
			Bind<IOutputLocalizer<User>>().To<UserOutputLocalizer>();
		}
	}
}