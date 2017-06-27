
using GameStore.DAL.Abstract;
using GameStore.DAL.Concrete;
using Ninject.Modules;

namespace GameStore.Services.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private readonly string _connectionString;

		//TODO: Consider: make fields readonly Fixed in ML_2

        public ServiceModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(_connectionString);
        }
    }
}
