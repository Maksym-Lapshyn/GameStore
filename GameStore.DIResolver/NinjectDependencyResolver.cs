using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using GameStore.DAL.Abstract;
using GameStore.DAL.Concrete;
using GameStore.Domain.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.Concrete;

namespace GameStore.DIResolver
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
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
            _kernel.Bind<IUnitOfWork>().To<EfUnitOfWork>();
            _kernel.Bind<IGenericRepository<Game>>().To<EFGenericRepository<Game>>();
            _kernel.Bind<IGenericRepository<Genre>>().To<EFGenericRepository<Genre>>();
            _kernel.Bind<IGenericRepository<Comment>>().To<EFGenericRepository<Comment>>();
            _kernel.Bind<IGameService>().To<UOWGameService>();
        }
    }
}