using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Concrete;
using GameStore.Services.Abstract;
using Ninject.Modules;

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