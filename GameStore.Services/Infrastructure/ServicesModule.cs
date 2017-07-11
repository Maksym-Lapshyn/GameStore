using GameStore.DAL.Abstract;
using GameStore.DAL.Concrete;
using GameStore.DAL.Context;
using Ninject.Modules;

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
		}
	}
}