using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using GameStore.Services.Abstract;
using GameStore.Services.Concrete;
using GameStore.Web.Infrastructure.Abstract;
using GameStore.Web.Infrastructure.Concrete;
using Ninject.Web.Common;

namespace GameStore.Web.Infrastructure.Resolver
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
		//TODO: Consider: make fields readonly
		private IKernel _kernel;

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
            _kernel.Bind<IGameService>().To<UowGameService>();
            _kernel.Bind<ICommentService>().To<UowCommentService>();
            _kernel.Bind<ILogger>().To<NLogger>();
        }
    }
}