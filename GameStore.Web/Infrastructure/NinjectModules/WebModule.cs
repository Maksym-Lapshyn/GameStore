using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Authentification.Concrete;
using GameStore.Common.Abstract;
using GameStore.Common.Concrete;
using GameStore.Services.Abstract;
using GameStore.Services.Concrete;
using GameStore.Services.Infrastructure;
using GameStore.Web.Infrastructure.Abstract;
using GameStore.Web.Infrastructure.Concrete;
using Ninject.Modules;
using Ninject.Web.Common;

namespace GameStore.Web.Infrastructure.NinjectModules
{
	public class WebModule : NinjectModule
	{
		public override void Load()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.AddProfile(new ServiceProfile());
				cfg.AddProfile(new WebProfile());
			});

			Bind<IMapper>().ToConstant(Mapper.Instance);
			Bind<IGameService>().To<GameService>();
			Bind<ICommentService>().To<CommentService>();
			Bind<IPublisherService>().To<PublisherService>();
			Bind<IGenreService>().To<GenreService>();
			Bind<IPlatformTypeService>().To<PlatformTypeService>();
			Bind<IOrderService>().To<OrderService>();
			Bind<IUserService>().To<UserService>();
			Bind<IRoleService>().To<RoleService>();
			Bind<ILogger>().To<Logger>();
			Bind<IAuthentication>().To<Authentication>().InRequestScope();
			Bind<IHashGenerator<string>>().To<Md5HashGenerator>();
		}
	}
}