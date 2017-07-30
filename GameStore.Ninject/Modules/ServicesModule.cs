using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Concrete;
using GameStore.DAL.Concrete.EntityFramework;
using GameStore.DAL.Concrete.MongoDb;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Linq;

namespace GameStore.Ninject.Modules
{
	public class ServicesModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IEfGameRepository>().To<ProxyGameRepository>().WhenInjectedInto(typeof(IService));
			Bind<IEfGenreRepository>().To<ProxyGenreRepository>().WhenInjectedInto(typeof(IService));
			Bind<IEfPublisherRepository>().To<ProxyPublisherRepository>().WhenInjectedInto(typeof(IService));
			Bind<IEfOrderRepository>().To<ProxyOrderRepository>().WhenInjectedInto(typeof(IService));
			Bind<IUnitOfWork>().To<UnitOfWork>();
		}
	}
}