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
		private readonly string _connectionString;

		public ServicesModule(string connectionString)
		{
			_connectionString = connectionString;
		}

		public override void Load()
		{
			//Ef
			Bind<IEfGameRepository>().To<ProxyGameRepository>().WhenInjectedInto(typeof(IService));
			Bind<IEfGenreRepository>().To<ProxyGenreRepository>().WhenInjectedInto(typeof(IService));
			Bind<IEfPublisherRepository>().To<ProxyPublisherRepository>().WhenInjectedInto(typeof(IService));
			Bind<IEfOrderRepository>().To<ProxyOrderRepository>().WhenInjectedInto(typeof(IService));
			Bind<GameStoreContext>().ToSelf().InRequestScope().WithConstructorArgument(_connectionString);
			Bind<IUnitOfWork>().To<UnitOfWork>();
			Bind<IEfGameRepository>().To<EfGameRepository>();
			Bind<IEfCommentRepository>().To<EfCommentRepository>();
			Bind<IEfGenreRepository>().To<EfGenreRepository>();
			Bind<IEfOrderRepository>().To<EfOrderRepository>();
			Bind<IEfPlatformTypeRepository>().To<EfPlatformTypeRepository>();
			Bind<IEfPublisherRepository>().To<EfPublisherRepository>();
			//Bind<IEfOrderRepository>().To<EfOrderRepository>().WithConstructorArgument(context);
			//Mongo
			Bind<IMongoGameRepository>().To<MongoGameRepository>();
			Bind<IMongoShipperRepository>().To<MongoShipperRepository>();
			Bind<IMongoGenreRepository>().To<MongoGenreRepository>();
			Bind<IMongoPublisherRepository>().To<MongoPublisherRepository>();
			Bind<IMongoOrderRepository>().To<MongoOrderRepository>();
			//Common
			Bind<IPipeline<IQueryable<Game>>>().To<GamePipeline>();
			Bind<IFilterMapper>().To<GameFilterMapper>();
		}
	}
}