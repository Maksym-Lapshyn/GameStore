using System.Linq;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Abstract.MongoDb;
using GameStore.DAL.Concrete;
using GameStore.DAL.Concrete.EntityFramework;
using GameStore.DAL.Concrete.MongoDb;
using GameStore.DAL.Context;
using MongoDB.Driver;
using Ninject.Modules;
using Ninject.Web.Common;

namespace GameStore.DI.NinjectModules
{
	public class DalModule : NinjectModule
	{
		private readonly string _mongoConnectionString;
		private readonly string _mongoDatabaseName;
		private readonly string _efConnectionString;

		public DalModule(string mongoConnectionString, string mongoDatabaseName, string efConnectionString)
		{
			_mongoConnectionString = mongoConnectionString;
			_mongoDatabaseName = mongoDatabaseName;
			_efConnectionString = efConnectionString;
		}

		public override void Load()
		{
			Bind<GameStoreContext>().ToSelf().InRequestScope().WithConstructorArgument(_efConnectionString);
			Bind<IMongoDatabase>().ToMethod(ctx => new MongoClient(_mongoConnectionString).GetDatabase(_mongoDatabaseName));

			Bind<IEfGameRepository>().To<EfGameRepository>();
			Bind<IEfCommentRepository>().To<EfCommentRepository>();
			Bind<IEfGenreRepository>().To<EfGenreRepository>();
			Bind<IEfOrderRepository>().To<EfOrderRepository>();
			Bind<IEfPlatformTypeRepository>().To<EfPlatformTypeRepository>();
			Bind<IEfPublisherRepository>().To<EfPublisherRepository>();
			Bind<IEfUserRepository>().To<EfUserRepository>();
			Bind<IEfRoleRepository>().To<EfRoleRepository>();

			Bind<IMongoGameRepository>().To<MongoGameRepository>();
			Bind<IMongoShipperRepository>().To<MongoShipperRepository>();
			Bind<IMongoGenreRepository>().To<MongoGenreRepository>();
			Bind<IMongoPublisherRepository>().To<MongoPublisherRepository>();
			Bind<IMongoOrderRepository>().To<MongoOrderRepository>();

			Bind<IPipeline<IQueryable<Game>>>().To<GamePipeline>();
			Bind<IFilterMapper>().To<GameFilterMapper>();
			Bind<ILogger<Game>>().To<MongoGameLogger>();

			Bind<ISynchronizer<Game>>().To<MongoGameSynchronizer>();
			Bind<ICopier<Game>>().To<GameCopier>();
			Bind<ICopier<Genre>>().To<GenreCopier>();
			Bind<ICopier<Publisher>>().To<PublisherCopier>();

            Bind<IAsyncEfGameRepository>().To<AsyncEfGameRepository>();
            Bind<IAsyncMongoGameRepository>().To<AsyncMongoGameRepository>();
            Bind<IAsyncLogger<Game>>().To<AsyncMongoGameLogger>();
		}
	}
}