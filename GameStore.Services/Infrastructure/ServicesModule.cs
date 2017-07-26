using GameStore.DAL.Abstract;
using GameStore.DAL.Concrete;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
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
			Bind<GameStoreContext>().ToSelf().WithConstructorArgument(_connectionString);
			Bind<IUnitOfWork>().To<UnitOfWork>();
			Bind<IPipeline<IQueryable<Game>>>().To<GamePipeline>();
			Bind<IFilterMapper>().To<GameFilterMapper>();
		}
	}
}