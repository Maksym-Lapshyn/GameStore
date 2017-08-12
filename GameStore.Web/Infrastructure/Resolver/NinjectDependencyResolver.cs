using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Authentification.Concrete;
using GameStore.Services.Abstract;
using GameStore.Services.Concrete;
using GameStore.Services.Infrastructure;
using GameStore.Web.Infrastructure.Abstract;
using GameStore.Web.Infrastructure.Concrete;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.Web.Infrastructure.Resolver
{
	public class NinjectDependencyResolver : IDependencyResolver
	{
		private readonly IKernel _kernel;

		public NinjectDependencyResolver(IKernel kernel)
		{
			_kernel = kernel;
			AddBindings();
		}

		public object GetService(Type serviceType)
		{
			return _kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return _kernel.GetAll(serviceType);
		}

		private void AddBindings()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.AddProfile(new ServiceProfile());
				cfg.AddProfile(new WebProfile());
			});

			_kernel.Bind<IMapper>().ToConstant(Mapper.Instance);
			_kernel.Bind<IGameService>().To<GameService>();
			_kernel.Bind<ICommentService>().To<CommentService>();
			_kernel.Bind<IPublisherService>().To<PublisherService>();
			_kernel.Bind<IGenreService>().To<GenreService>();
			_kernel.Bind<IPlatformTypeService>().To<PlatformTypeService>();
			_kernel.Bind<IOrderService>().To<OrderService>();
            _kernel.Bind<IUserService>().To<UserService>();
            _kernel.Bind<IRoleService>().To<RoleService>();
            _kernel.Bind<ILogger>().To<Logger>();
			_kernel.Bind<IAuthentication>().To<Authentication>();

        }
	}
}