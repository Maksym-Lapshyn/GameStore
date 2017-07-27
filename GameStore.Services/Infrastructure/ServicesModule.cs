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
using System.Linq;

namespace GameStore.Services.Infrastructure
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
			var context = Bind<GameStoreContext>().ToSelf().WithConstructorArgument(_connectionString);
			Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(context);
			Bind<IEfGameRepository>().To<EfGameRepository>().WithConstructorArgument(context);
			Bind<IEfCommentRepository>().To<EfCommentRepository>().WithConstructorArgument(context);
			Bind<IEfGenreRepository>().To<EfGenreRepository>().WithConstructorArgument(context);
			Bind<IEfOrderRepository>().To<EfOrderRepository>().WithConstructorArgument(context);
			Bind<IEfPlatformTypeRepository>().To<EfPlatformTypeRepository>().WithConstructorArgument(context);
			Bind<IEfPublisherRepository>().To<EfPublisherRepository>().WithConstructorArgument(context);
			//Mongo
			Bind<IMongoGameRepository>().To<MongoGameRepository>();
			Bind<IMongoShipperRepository>().To<MongoShipperRepository>();
			Bind<IMongoGenreRepository>().To<MongoGenreRepository>();
			Bind<IMongoPublisherRepository>().To<MongoPublisherRepository>();
			//Common
			Bind<IPipeline<IQueryable<Game>>>().To<GamePipeline>();
			Bind<IFilterMapper>().To<GameFilterMapper>();
		}
	}
}